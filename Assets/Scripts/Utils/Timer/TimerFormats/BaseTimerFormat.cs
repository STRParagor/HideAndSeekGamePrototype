using System;

namespace ParagorGames.Utils.TimerFormats
{
    public abstract class BaseTimerFormat : IDisposable
    {
        public abstract string GetTimeFormat(long milliseconds);
        public abstract string GetTimeFormat(TimeSpan timeSpan);

        public abstract void Dispose();
    }
}