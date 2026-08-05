namespace PulseMonitor
{
    public class MetricSource
    {
        public event Action<Reading>? ReadingTaken;
        public void Emit(Reading reading) => ReadingTaken?.Invoke(reading);
    }
}
