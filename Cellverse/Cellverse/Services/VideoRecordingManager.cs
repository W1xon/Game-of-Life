using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Cellverse.View;
using Microsoft.Win32;
using Xabe.FFmpeg.Downloader;

namespace Cellverse.Services;

public class VideoRecordingManager
{
    private VideoRecorder _videoRecorder;
    private bool _isRecording;
    private bool _isFFmpegReady;

    public bool IsRecording => _isRecording;
    public bool IsFFmpegReady => _isFFmpegReady;
    public int FrameCount => _videoRecorder?.FrameCount ?? 0;

    public async Task<bool> InitializeFFmpegAsync(Window owner)
    {
        try
        {
            // Проверяем, установлен ли уже FFmpeg
            string currentDir = AppContext.BaseDirectory;
            string ffmpegPath = Path.Combine(currentDir, "ffmpeg.exe");
            
            if (!File.Exists(ffmpegPath))
            {
                var progressWindow = new FFmpegDownloadWindow { Owner = owner };
                progressWindow.Show();
                
                try
                {
                    await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official);
                    progressWindow.Close();
                    
                    // ✅ Используем новый API
                    CustomMessageBox.ShowSuccess(
                        "Готово",
                        "FFmpeg успешно загружен!\nТеперь вы можете записывать видео."
                    );
                }
                catch (Exception)
                {
                    progressWindow.Close();
                    throw;
                }
            }
            
            _isFFmpegReady = true;
            return true;
        }
        catch (Exception ex)
        {
            // ✅ Используем новый API
            CustomMessageBox.ShowError(
                "Ошибка загрузки FFmpeg",
                $"Ошибка: {ex.Message}\n\nВидеозапись будет недоступна.\n\nВы можете скачать FFmpeg вручную с https://ffmpeg.org/"
            );
            
            _isFFmpegReady = false;
            return false;
        }
    }

    public void StartRecording(int fps = 30)
    {
        if (!_isFFmpegReady)
            throw new InvalidOperationException("FFmpeg не готов");
        
        _isRecording = true;
        _videoRecorder = new VideoRecorder(fps);
    }

    public void CaptureFrame(WriteableBitmap bitmap)
    {
        if (_isRecording)
        {
            _videoRecorder?.CaptureFrame(bitmap);
        }
    }

    public async Task<bool> StopRecordingAsync(Window owner)
    {
        if (!_isRecording)
            return false;
        
        _isRecording = false;
        
        var saveDialog = new SaveFileDialog
        {
            Filter = "MP4 Video|*.mp4",
            Title = "Сохранить видео",
            FileName = $"GameOfLife_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.mp4"
        };
        
        if (saveDialog.ShowDialog() != true)
        {
            _videoRecorder?.Clear();
            _videoRecorder = null;
            return false;
        }
        
        try
        {
            var progressWindow = new VideoSaveProgressWindow { Owner = owner };
            
            // Если есть метод SetFrameCount, вызываем его
            if (_videoRecorder?.FrameCount > 0)
            {
                // progressWindow.SetFrameCount(_videoRecorder.FrameCount);
                // Или обновляем текст напрямую:
                progressWindow.StatusText.Text = $"Сохранение {_videoRecorder.FrameCount} кадров...\nПожалуйста, подождите.";
            }
            
            progressWindow.Show();
            
            await _videoRecorder.SaveVideoAsync(saveDialog.FileName);
            
            progressWindow.Close();
            
            // ✅ Используем новый API
            CustomMessageBox.ShowSuccess(
                "Видео сохранено!",
                $"Видео успешно сохранено!\n\n" +
                $"📊 Кадров: {_videoRecorder.FrameCount}\n" +
                $"📁 Файл: {Path.GetFileName(saveDialog.FileName)}"
            );
            
            _videoRecorder?.Clear();
            _videoRecorder = null;
            return true;
        }
        catch (Exception ex)
        {
            // ✅ Используем новый API
            CustomMessageBox.ShowError(
                "Ошибка сохранения",
                $"Не удалось сохранить видео:\n\n{ex.Message}"
            );
            
            _videoRecorder?.Clear();
            _videoRecorder = null;
            return false;
        }
    }

    public void CancelRecording()
    {
        _isRecording = false;
        _videoRecorder?.Clear();
        _videoRecorder = null;
    }
}