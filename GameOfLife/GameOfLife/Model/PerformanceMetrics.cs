using System.Diagnostics;

namespace GameOfLife.Model;

 public class PerformanceMetrics
    {
        private readonly Stopwatch _gpsTimer = new();
        private readonly Stopwatch _cycleTimer = new();
        private int _generationCount;
        private Stopwatch _executionTimer = new();

        public double GPS { get; private set; }
        public double LastUpdateTimeMs { get; private set; } // Время выполнения кода
        public double LastCycleTimeMs { get; private set; }  // Полное время между генерациями

        public void StartCycle()
        {
            if (!_gpsTimer.IsRunning) _gpsTimer.Start();
            if (!_cycleTimer.IsRunning) _cycleTimer.Start();
            _generationCount = 0;
        }

        public void BeginUpdate()
        {
            _executionTimer.Restart();
            LastCycleTimeMs = _cycleTimer.Elapsed.TotalMilliseconds;
            _cycleTimer.Restart();
        }

        public void EndUpdate()
        {
            _executionTimer.Stop();
            LastUpdateTimeMs = _executionTimer.Elapsed.TotalMilliseconds;

            _generationCount++;
            if (_gpsTimer.ElapsedMilliseconds >= 1000)
            {
                GPS = _generationCount / (_gpsTimer.ElapsedMilliseconds / 1000.0);
                _generationCount = 0;
                _gpsTimer.Restart();
            }
        }
    }