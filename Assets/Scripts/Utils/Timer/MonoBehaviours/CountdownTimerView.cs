using UnityEngine;
using TMPro;
using ParagorGames.Utils.TimerFormats;

namespace ParagorGames.UI.SubViews
{
    public class CountdownTimerView : BaseCountdownTimerView
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Color _warningColor = Color.red;
        
        private BaseTimerFormat _timerFormat;
        private long _warningThresholdMilliseconds = -1;
        private Color _initialTextColor;
        
        public void Initialize(long initialMilliseconds, BaseTimerFormat timerFormat, long warningThresholdMilliseconds = -1)
        {
            StopTimer();
            Initialize(initialMilliseconds);
            _initialTextColor = _timerText.color;
            _timerFormat = timerFormat;
            _warningThresholdMilliseconds = warningThresholdMilliseconds;
            InitialMilliseconds = initialMilliseconds;
            OnTimerInterval(initialMilliseconds);
            SetFormattedText(InitialMilliseconds);
        }

        protected override void OnRunTimer()
        {
            if (InitialMilliseconds > _warningThresholdMilliseconds) return;
            _timerText.color = _initialTextColor;
        }
        
        protected override void OnTimerInterval(long milliseconds)
        {
            if (milliseconds <= _warningThresholdMilliseconds)
            {
                OnTimerWarning();
            }
            
            SetFormattedText(milliseconds);
        }
        
        protected override void OnTimerElapsed()
        {
            SetFormattedText(0);
        }
        
        private void OnTimerWarning()
        {
            _timerText.color = _warningColor;
        }
        
        private void SetFormattedText(long milliseconds)
        {
            _timerText.text = _timerFormat.GetTimeFormat(milliseconds);
        }
    }
}