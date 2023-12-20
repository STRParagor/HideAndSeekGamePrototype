using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ParagorGames.Utils
{
    public class Timer : IDisposable
    {
        public event Action Elapsed = delegate { };
        public event Action Began = delegate { };
        public event Action Paused = delegate { };
        public event Action Stopped = delegate { };
        public event Action Resumed = delegate { };
        public event Action<long> Interval = delegate {  };
        
        public bool IsRunning { get; private set; }
        public bool IsPlaying { get; private set; }
        public bool IsPaused => IsRunning && !IsPlaying;
        public UniTask TimerTask => _timerTask;
        
        private long _millisecondsInterval;
        private CancellationTokenSource _cts;
        private UniTask _timerTask = UniTask.CompletedTask;

        private long _targetMilliseconds;
        private long _currentMilliseconds;
        private long _nextIntervalEvent;
        private double _nextTime;
        private double _currentTime;

        public Timer(long millisecondsIntervalEvent = 1000)
        {
            _millisecondsInterval = millisecondsIntervalEvent;
        }

        public void SetIntervalCallback(long milliseconds) => _millisecondsInterval = milliseconds;

        public void Run(long targetMilliseconds = -1L)
        {
            Stop();
            _targetMilliseconds = targetMilliseconds;
            _currentMilliseconds = 0;
            _nextIntervalEvent = _currentMilliseconds + _millisecondsInterval;
            _currentTime = targetMilliseconds * 0.001d;
            _nextTime = _currentTime + _millisecondsInterval * 0.001d;
            Began();
            _cts = new CancellationTokenSource();
            RunTimerAsync(_cts.Token);
        }

        public void Stop()
        {
            if (!IsRunning) return;
            
            IsRunning = false;
            IsPlaying = false;
            Stopped();
            Dispose();
        }
        
        public void Resume()
        {
            if (!IsPaused) return;

            Resumed();
            _cts = new CancellationTokenSource();
            RunTimerAsync(_cts.Token);
        }
        
        public void Pause()
        {
            if (!IsRunning || !IsPlaying) return;
            
            IsPlaying = false;
            Paused();
            Dispose();
        }
        
        private async void RunTimerAsync(CancellationToken token)
        {
            IsRunning = true;
            IsPlaying = true;

            _timerTask = UniTask.WaitWhile(() => IsRunning, cancellationToken: token);
            
            while (!token.IsCancellationRequested)
            {
                if (Time.unscaledDeltaTime < 0.2f)
                {
                    _currentTime += Time.unscaledDeltaTime;
                    _currentMilliseconds = (long)_currentTime * 1000;
                    
                    if (_currentTime > _nextTime)
                    {
                        Interval(_nextIntervalEvent);
                        
                        _nextIntervalEvent += _millisecondsInterval;
                        _nextTime += _millisecondsInterval * 0.001d;
                        if (_targetMilliseconds > 0 && _currentTime > _targetMilliseconds)
                        {
                            OnElapsed();
                            return;
                        }
                    }
                }
                
                await UniTask.Yield(token).SuppressCancellationThrow();
            }
        }

        private void OnElapsed()
        {
            Elapsed.Invoke();
            Stop();
        }
        
        public void Dispose()
        {
            if (_cts != null)
            {
                if (_cts.IsCancellationRequested == false)
                {
                    _cts.Cancel();
                }
                
                _cts.Dispose();
                _cts = null;
            };
        }
    }
}