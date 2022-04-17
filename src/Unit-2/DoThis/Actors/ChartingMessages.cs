using Akka.Actor;

namespace ChartApp.Actors
{
    #region Reporting

    /// <summary>
    /// Signal used to indicate that it's time to sample all counters
    /// </summary>
    public class GatherMetrics
    {
    }

    /// <summary>
    /// Metric data at the time of sample
    /// </summary>
    public class Metric
    {
        public string Series { get; }

        public float CounterValue { get; }

        public Metric(string series, float counterValue)
        {
            CounterValue = counterValue;
            Series = series;
        }
    }

    #endregion

    #region Performance Counter Management

    /// <summary>
    /// All types of counters supported by this example
    /// </summary>
    public enum CounterType
    {
        Cpu,
        Memory,
        Disk
    }

    /// <summary>
    /// Enables a counter and begins publishing values to <see cref="Subscriber"/>.
    /// </summary>
    public class SubscribeCounter
    {
        public CounterType Counter { get; }

        public IActorRef Subscriber { get; }

        public SubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Subscriber = subscriber;
            Counter = counter;
        }
    }

    /// <summary>
    /// Unsubscribes <see cref="Subscriber"/> from receiving updates 
    /// for a given counter
    /// </summary>
    public class UnsubscribeCounter
    {
        public CounterType Counter { get; }

        public IActorRef Subscriber { get; }

        public UnsubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Subscriber = subscriber;
            Counter = counter;
        }
    }

    #endregion
}
