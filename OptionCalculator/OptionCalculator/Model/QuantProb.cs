namespace OptionCalculator.Model
{
    public struct QuantProb
    {
        public double val;

        public double p;

        public QuantProb(double v, double p)
        {
            val = v;
            this.p = p;
        }

        public override string ToString()
        {
            return $"{val}: {p}";
        }
    }
}
