namespace OptionCalculator.Model
{
    /// <summary>
    /// a cortege of value and the corresponded probability
    /// an array of QuantProb comprises a cumulative distribution function
    /// </summary>
    public struct QuantProb
    {
        /// <summary>
        /// value
        /// </summary>
        public double val;

        /// <summary>
        /// probability
        /// </summary>
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
