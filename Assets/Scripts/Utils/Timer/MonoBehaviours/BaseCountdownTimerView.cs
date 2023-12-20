using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ParagorGames.Utils;
using UnityEngine;

namespace ParagorGames.UI.SubViews
{
    public abstract class BaseCountdownTimerView : MonoBehaviour
    {
        public event Action TimerElapsed = delegate { };
        public event Action<long> TimerInterval = delegate { };
        
        [SerializeField, Min(10)] private long _intervalCallback = 1000;
        
        private readonly CountdownTimer _countdownTimer = new ();
        
        protected long InitialMilliseconds = -1;

        private void OnEnable()
        {
            _countdownTimer.Interval += OnBaseTimerInterval;
            _countdownTimer.Elapsed += OnBaseTimerElapsed;
        }
        
        private void OnDisable()
        {
            _countdownTimer.Interval -= OnBaseTimerInterval;
            _countdownTimer.Elapsed -= OnBaseTimerElapsed;
        }
        
        private void OnDestroy()
        {
            StopTimer();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            switch (hasFocus)
            {
                case true when _countdownTimer.IsRunning: ResumeTimer(); break;
                case false when _countdownTimer.IsRunning: PauseTimer(); break;
            }
        }
        
        protected void Initialize(long initialMilliseconds)
        {
            StopTimer();

            _countdownTimer.SetIntervalCallback(_intervalCallback);
            InitialMilliseconds = initialMilliseconds;
        }

        public UniTask RunTimerAsync()
        {
            if (InitialMilliseconds < 0)
            {
                throw new Exception($"[{nameof(CountdownTimerView)}] Timer view don't initialized. Call Initialize() method before run timer");
            }

            OnRunTimer();
            _countdownTimer.Run(InitialMilliseconds);
            return _countdownTimer.CountdownTask;
        }

        private void OnBaseTimerInterval(long milliseconds)
        {
            OnTimerInterval(milliseconds);
            TimerInterval(milliseconds);
        }

        private void OnBaseTimerElapsed()
        {
            OnTimerElapsed();
            TimerElapsed();
        }

        public void PauseTimer() => _countdownTimer.Pause();
        
        public void ResumeTimer() => _countdownTimer.Resume();
        
        public void StopTimer() => _countdownTimer.Stop();
        
        protected abstract void OnRunTimer();
        
        protected abstract void OnTimerInterval(long milliseconds);

        protected abstract void OnTimerElapsed();
    }
}