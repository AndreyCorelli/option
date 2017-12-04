namespace OptionCalculator.Model
{
    public class OptionContract
    {
        public Side Side { get; set; } = Side.Call;

        public double Price { get; set; }

        public double Strike { get; set; }

        public double Term { get; set; }

        public double Volume { get; set; } = 1;

        public int YearTradeDays { get; set; } = 365;
    }
}
