using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using GameOfLife.View;
using Microsoft.Win32;
using Xabe.FFmpeg.Downloader;

namespace GameOfLife.Services;

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

                    var successDialog = new CustomMessageBox
                    {
                        Owner = owner,
                        MessageTitle = "Готово",
                        MessageText = "FFmpeg успешно загружен!\nТеперь вы можете записывать видео.",
                        MessageType = MessageBoxType.Success
                    };
                    successDialog.ShowDialog();
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
            var errorDialog = new CustomMessageBox
            {
                Owner = owner,
                MessageTitle = "Ошибка",
                MessageText = $"Ошибка загрузки FFmpeg: {ex.Message}\n\nВидеозапись будет недоступна.\n\nВы можете скачать FFmpeg вручную с https://ffmpeg.org/",
                MessageType = MessageBoxType.Error
            };
            errorDialog.ShowDialog();

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
            progressWindow.SetFrameCount(_videoRecorder.FrameCount);
            progressWindow.Show();

            await _videoRecorder.SaveVideoAsync(saveDialog.FileName);

            progressWindow.Close();

            var successDialog = new CustomMessageBox
            {
                Owner = owner,
                MessageTitle = "Успех",
                MessageText = $"Видео успешно сохранено!\n\nКадров: {_videoRecorder.FrameCount}\nФайл: {Path.GetFileName(saveDialog.FileName)}",
                MessageType = MessageBoxType.Success
            };
            successDialog.ShowDialog();

            _videoRecorder?.Clear();
            _videoRecorder = null;
            return true;
        }
        catch (Exception ex)
        {
            var errorDialog = new CustomMessageBox
            {
                Owner = owner,
                MessageTitle = "Ошибка",
                MessageText = $"Ошибка при сохранении видео:\n{ex.Message}",
                MessageType = MessageBoxType.Error
            };
            errorDialog.ShowDialog();

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