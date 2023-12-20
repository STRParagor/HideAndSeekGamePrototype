using UnityEngine;

namespace ParagorGames.UI.SubViews
{
    public class CountdownBarTimerView : BaseCountdownTimerView
    {
        [SerializeField] private BaseProgressFillerProxy _progressFiller;
        
        public new void Initialize(long initialMilliseconds)
        {
            base.Initialize(initialMilliseconds);
        }
        
        protected override void OnRunTimer()
        {
            _progressFiller.SetProgress(1f);
        }

        protected override void OnTimerInterval(long milliseconds)
        {
            _progressFiller.SetProgress((float) milliseconds / InitialMilliseconds);
        }

        protected override void OnTimerElapsed()
        {
            _progressFiller.SetProgress(0f);
        }
    }
}