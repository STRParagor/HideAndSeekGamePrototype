using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ParagorGames.Utils
{
    public class CountdownTimer : IDisposable
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
        public UniTask CountdownTask => _countdownTask;

        private long _millisecondsInterval;
        private CancellationTokenSource _cts;
        private UniTask _countdownTask = UniTask.CompletedTask;

        private long _currentMilliseconds;
        private long _nextIntervalEvent;
        private double _nextTime;
        private double _currentTime;

        public CountdownTimer(long millisecondsIntervalEvent = 1000)
        {
            _millisecondsInterval = millisecondsIntervalEvent;
        }

        public void SetIntervalCallback(long milliseconds) => _millisecondsInterval = milliseconds;

        public void Run(long millisecondsCountdown)
        {
            Stop();

            _currentMilliseconds = millisecondsCountdown;
            _nextIntervalEvent = _currentMilliseconds - _millisecondsInterval;
            _currentTime = (double)millisecondsCountdown / 1000;
            _nextTime = _currentTime - ((double)_millisecondsInterval /1000);
            Began();
            _cts = new CancellationTokenSource();
            RunCountdownAsync(_cts.Token);
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
            RunCountdownAsync(_cts.Token);
        }
        
        public void Pause()
        {
            if (!IsRunning || !IsPlaying) return;
            
            IsPlaying = false;
            Paused();
            Dispose();
        }
        
        private async void RunCountdownAsync(CancellationToken token)
        {
            IsRunning = true;
            IsPlaying = true;

            _countdownTask = UniTask.WaitWhile(() => IsRunning);
            
            while (!token.IsCancellationRequested)
            {
                if (Time.unscaledDeltaTime < 0.2f)
                {
                    _currentTime -= Time.unscaledDeltaTime;
                    _currentMilliseconds = (long)_currentTime * 1000;
                    
                    if (_currentTime < _nextTime)
                    {
                        Interval(_nextIntervalEvent);
                        
                        _nextIntervalEvent -= _millisecondsInterval;
                        _nextTime -= (double)_millisecondsInterval / 1000;
                        if (_currentTime < 0)
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