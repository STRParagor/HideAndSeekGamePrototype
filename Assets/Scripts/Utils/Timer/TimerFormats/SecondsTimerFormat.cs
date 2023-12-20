using System;

namespace ParagorGames.Utils.TimerFormats
{
    public class SecondsTimerFormat : BaseTimerFormat
    {
        public override string GetTimeFormat(long milliseconds)
        {
            return ((999 + milliseconds) / 1000).ToString();
        }

        public override string GetTimeFormat(TimeSpan timeSpan)
        {
            return ((9999999 + timeSpan.Ticks) / 10000000).ToString();
        }

        public override void Dispose() { }
    }
}