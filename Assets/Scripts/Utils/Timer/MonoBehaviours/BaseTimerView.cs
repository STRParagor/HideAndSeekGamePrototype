using System;
using Cysharp.Threading.Tasks;
using ParagorGames.Utils;
using UnityEngine;

namespace ParagorGames.UI.SubViews
{
    public abstract class BaseTimerView : MonoBehaviour
    {
        public event Action TimerElapsed = delegate { };
        public event Action<long> TimerInterval = delegate { };
        
        [SerializeField, Min(10)] private long _intervalCallback = 1000;
        
        protected readonly CountdownTimer Timer = new ();
        
        protected long InitialMilliseconds = -1;

        private void OnEnable()
        {
            Timer.Interval += OnBaseTimerInterval;
            Timer.Elapsed += OnBaseTimerElapsed;
        }
        
        private void OnDisable()
        {
            Timer.Interval -= OnBaseTimerInterval;
            Timer.Elapsed -= OnBaseTimerElapsed;
        }
        
        private void OnDestroy()
        {
            StopTimer();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            switch (hasFocus)
            {
                case true when Timer.IsRunning: ResumeTimer(); break;
                case false when Timer.IsRunning: PauseTimer(); break;
            }
        }
        
        protected void Initialize(long initialMilliseconds)
        {
            StopTimer();

            Timer.SetIntervalCallback(_intervalCallback);
            InitialMilliseconds = initialMilliseconds;
        }
        
        public UniTask RunTimer()
        {
            OnRunTimer();
            Timer.Run(InitialMilliseconds);
            return Timer.CountdownTask;
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

        public void PauseTimer() => Timer.Pause();
        
        public void ResumeTimer() => Timer.Resume();
        
        public void StopTimer() => Timer.Stop();
        
        protected abstract void OnRunTimer();
        
        protected abstract void OnTimerInterval(long milliseconds);

        protected abstract void OnTimerElapsed();
    }
}