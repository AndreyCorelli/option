namespace OptionCalculator.Model
{
    /// <summary>
    /// an option contract
    /// being estimated
    /// </summary>
    public class OptionContract
    {
        /// <summary>
        /// put or call
        /// </summary>
        public Side Side { get; set; } = Side.Call;

        /// <summary>
        /// current option's active price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// option's strike price
        /// </summary>
        public double Strike { get; set; }

        /// <summary>
        /// option's term in days
        /// </summary>
        public double Term { get; set; }

        /// <summary>
        /// option's volume
        /// just to multiply the premium calculated by the volume's value
        /// </summary>
        public double Volume { get; set; } = 1;

        /// <summary>
        /// count of days within the year when the active is being traded
        /// </summary>
        public int YearTradeDays { get; set; } = 365;
    }
}
