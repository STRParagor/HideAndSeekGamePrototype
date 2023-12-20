using TMPro;
using UnityEngine;
using ParagorGames.Utils.TimerFormats;

namespace ParagorGames.UI.SubViews
{
    public class TimerView : BaseTimerView
    {
        [Header("Timer View")]
        [SerializeField] private TextMeshProUGUI _timerText;
        
        private BaseTimerFormat _timerFormat = new MinutesTimerFormat();
        
        public void Initialize(long initialMilliseconds, BaseTimerFormat timerFormat)
        {
            StopTimer();
            Initialize(initialMilliseconds);
            _timerFormat = timerFormat;
            SetFormattedText(0);
        }
        
        protected override void OnRunTimer() { }

        protected override void OnTimerInterval(long milliseconds)
        {
            SetFormattedText(milliseconds);
        }

        protected override void OnTimerElapsed()
        {
            SetFormattedText(InitialMilliseconds);
        }
        
        private void SetFormattedText(long milliseconds)
        {
            _timerText.text = _timerFormat.GetTimeFormat(milliseconds);
        }
    }
}