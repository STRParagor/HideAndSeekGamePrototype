using System;
using System.Text;

namespace ParagorGames.Utils.TimerFormats
{
    public class MinutesTimerFormat : BaseTimerFormat
    {
        private readonly string _minutesFormat;
        private readonly string _secondsFormat;
        private StringBuilder _stringBuilder;

        public MinutesTimerFormat(string minutesFormat = "00:", string secondsFormat = "00")
        {
            _minutesFormat = minutesFormat;
            _secondsFormat = secondsFormat;
            _stringBuilder = new StringBuilder(minutesFormat.Length + secondsFormat.Length + 2);
        }

        public override string GetTimeFormat(long milliseconds)
        {
            var timeSpan = TimeSpan.FromMilliseconds(999 + milliseconds);
            return GetTimeFormat(timeSpan);
        }

        public override string GetTimeFormat(TimeSpan timeSpan)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(((int)timeSpan.TotalMinutes).ToString(_minutesFormat));
            _stringBuilder.Append(timeSpan.Seconds.ToString(_secondsFormat));
            return _stringBuilder.ToString();
        }

        public override void Dispose()
        {
            _stringBuilder.Clear();
            _stringBuilder = null;
        }
    }
}