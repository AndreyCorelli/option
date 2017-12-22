using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace OptionCalculator.Model
{
    /// <summary>
    /// option premium calculator
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// base active's price timeseries
        /// </summary>
        private List<double> prices;

        /// <summary>
        /// contract to calculate premium
        /// </summary>
        private OptionContract contract;

        /// <summary>
        /// leave (false) or remove (true) trend from the source timeseries
        /// </summary>
        private bool detrend;

        /// <summary>
        /// cumulative distribution function
        /// </summary>
        private List<QuantProb> distribution = new List<QuantProb>();

        /// <summary>
        /// uniformly distributed random value generator
        /// </summary>
        private readonly Random rand = new Random();

        /// <summary>
        /// the file path to store results
        /// </summary>
        private string filePath;

        /// <summary>
        /// profits got by the option's buyer in each iteration
        /// stored in a list to be saved in an image (histogram) after all the calculation completed
        /// </summary>
        private List<double> iteratedProfits = new List<double>();

        /// <summary>
        /// the result - premium calculated
        /// </summary>
        private double premium;

        /// <summary>
        /// historical volatility
        /// </summary>
        private double hv;        

        public double HV => hv;

        /// <summary>
        /// iterations to estimate the premium
        /// </summary>
        public int iterationsCount = 10000;

        /// <summary>
        /// the main 
        /// </summary>
        /// <param name="prices">source active's prices timeseries</param>
        /// <param name="detrend">remove trend from the source data when set to true</param>
        /// <param name="contract">option contract's specifications</param>
        /// <param name="folder">a path to place files: price distribution and option premium distribution chart</param>
        /// <returns></returns>
        public double CalcPremium(List<double> prices, bool detrend,
            OptionContract contract, string folder)
        {
            this.prices = prices;
            this.contract = contract;
            this.detrend = detrend;
            filePath = folder;
            // build the cumulative distribution function
            CalculateDistribution();
            // calculate the premium, store results in 2 files
            return DoCalcPremium();
        }

        /// <summary>
        /// perform N iterations
        /// in each iteration calculate and sum up the random profit obtained by the option's buyer
        /// 
        /// the premium is the sum divided by N
        /// </summary>        
        private double DoCalcPremium()
        {
            double sum = 0;
            for (var i = 0; i < iterationsCount; i++)
            {
                var p = CalcPremiumForIteration();
                sum += p;
                // each profit value is stored to be drawn on a chart
                iteratedProfits.Add(p);
            }
            premium = sum / iterationsCount;
            // store price distribution (*.csv) and option premium distribution chart (histogram)
            StoreInFile();
            return premium;
        }

        /// <summary>
        /// option buyer hypothetical profit calculation
        /// </summary>
        private double CalcPremiumForIteration()
        {
            var p = contract.Price;
            // here I floor term (days)
            var steps = (int) contract.Term;            
            for (var i = 0; i < steps; i++)
            {
                var delta = GetDelta();
                // new price = the old one * exp(delta),
                // where delta has the distribution, provided by "distribution" list
                p = p * Math.Exp(delta);
            }

            // see the next method
            p = ShiftPriceOnLastDay(p);

            // option's profit calculation
            var s = contract.Side == Side.Call ? 1 : -1;
            var pl = s * contract.Volume * (p - contract.Price);
            // this is an Option. If the profit calculated is negative
            // the option buyer refuses to take that loss
            return pl < 0 ? 0 : pl;
        }

        /// <summary>
        /// last price step is to be multiply be sqrt(f)
        /// where f is a part of the last day in term provided
        /// e.g., contract.Term = 10.66, f = 0.66
        /// </summary>
        private double ShiftPriceOnLastDay(double lastPrice)
        {
            var steps = (int)contract.Term;
            var dayFract = contract.Term - steps;
            if (dayFract > 0)
            {
                var delta = GetDelta();
                delta = lastPrice * Math.Exp(delta) - lastPrice;
                delta = delta * Math.Sqrt(dayFract);
                lastPrice += delta;
            }
            return lastPrice;
        }

        private void CalculateDistribution()
        {
            if (prices.Count < 3) return;
            // HV value will be displayed somewhere in the program just for a reference
            CalcHistVol();

            // price timeseries is reduced to price deltas' timeseries
            var deltas = new List<double>(prices.Count - 1);
            for (var i = 1; i < prices.Count; i++)
                deltas.Add(Math.Log(prices[i] / prices[i - 1]));

            // I remove trend from price deltas, not from the price itself
            // because that way I may end up with negative prices
            if (detrend)
                DetrendDeltas(deltas);
            deltas.Sort();

            for (var i = 0; i < deltas.Count; i++)
            {
                // probability of price delta being lower or equal to deltas[i]
                var prob = (i + 1.0) / deltas.Count;
                distribution.Add(new QuantProb(deltas[i], prob));
            }
        }

        /// <summary>
        /// after the substraction sum of price deltas will be 0
        /// </summary>
        private static void DetrendDeltas(List<double> deltas)
        {
            var d = deltas.Average() / (deltas.Count - 1);
            for (var i = 0; i < deltas.Count; i++)
                deltas[i] = deltas[i] - d * i;
        }

        /// <summary>
        /// calculate HV - just for a reference
        /// </summary>
        private void CalcHistVol()
        {
            var deltas = new List<double>();
            for (var i = 1; i < prices.Count; i++)
                deltas.Add(100 * (prices[i] - prices[i - 1]) / prices[i - 1]);
            hv = CalcHvByDeltas(deltas);
        }

        /// <summary>
        /// deltas are squared, summed up, divided by N-1 and finally
        /// are applied by square root function
        /// </summary>
        private double CalcHvByDeltas(List<double> deltas)
        {
            var avg = deltas.Average();
            var sum = 0.0;
            foreach (var delta in deltas)
            {
                var d2 = delta - avg;
                sum += d2 * d2;
            }
            sum /= deltas.Count - 1;
            var sigma = Math.Sqrt(sum);
            return sigma * Math.Sqrt(365);
        }

        /// <summary>
        /// get a random price delta, d: 
        /// p[i+1] = p[i] * exp(d)
        /// 
        /// from the cumulative distribution function
        /// </summary>
        private double GetDelta()
        {
            var p = rand.NextDouble();
            var floatP = p * distribution.Count;
            var floorP = (int) floatP;
            if (floorP >= distribution.Count - 1)
                return distribution.Last().val;

            var shift = floatP - floorP;
            var a = distribution[floorP].val;
            var b = distribution[floorP + 1].val;
            return a + (b - a) * shift;            
        }

        /// <summary>
        /// store price distribution function in a file (*.csv)
        /// store option buyer profits in another file (bitmap)
        /// </summary>
        private void StoreInFile()
        {
            using (var sw = new StreamWriter(Path.Combine(filePath, "distribution.txt"), false, Encoding.ASCII))
                foreach (var d in distribution)
                    sw.WriteLine($"{d.val:F4};{d.p:F4}");
            // sim prices
            const int height = 800;
            const int width = 15;
            const int shadesMax = 4;
            var map = Enumerable.Range(0, height).Select(y =>
                Enumerable.Range(0, width).ToDictionary(x => x, x => 0)).ToList();

            var min = iteratedProfits.Min();
            var max = iteratedProfits.Max();
            var kY = height / (max - min);
            foreach (var p in iteratedProfits)
            {
                var y = (int) Math.Round((p - min) * kY);
                if (y < 0) y = 0;
                if (y >= height) y = height - 1;
                var x = rand.Next(width);

                var val = map[y][x];
                if (val < shadesMax) val++;
                map[y][x] = val;
            }

            // make a bitmap
            const int priceWd = 75;
            var bmp = new Bitmap(width + priceWd, height);
            using (var cn = Graphics.FromImage(bmp))
            {
                using (var b = new SolidBrush(Color.White))
                    cn.FillRectangle(b, 0, 0, bmp.Width, height);
                for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    var s = map[y][x];
                    var intense = s == 0 ? 0 : s == 1 ? 255 : s == 2 ? 225 : s == 3 ? 205 : 192;
                    var color = intense == 0 ? Color.White : Color.FromArgb(255, 0, 0, intense);
                    bmp.SetPixel(x, height - y - 1, color);
                }
                // text
                using (var f = new Font(FontFamily.GenericMonospace, 11))
                using (var b = new SolidBrush(Color.Black))
                {
                    cn.DrawString($"{min:F2}", f, b, width + 3, height - 30);
                    cn.DrawString($"{max:F2}", f, b, width + 3, 5);
                    var y = height - (premium - min) * kY;
                    using (var blueBrush = new SolidBrush(Color.Blue))
                        cn.DrawString($"{premium:F2}", f, blueBrush, width + 3, (float)y, new StringFormat
                    {
                        LineAlignment = StringAlignment.Center
                    });
                }
            }

            bmp.Save(Path.Combine(filePath, "profits.png"), ImageFormat.Png);
        }
    }
}
