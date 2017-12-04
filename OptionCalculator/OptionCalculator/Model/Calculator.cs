using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace OptionCalculator.Model
{
    public class Calculator
    {
        private List<double> prices;
        private OptionContract contract;
        private bool detrend;
        private List<QuantProb> distribution = new List<QuantProb>();
        private readonly Random rand = new Random();
        private string filePath;
        private List<double> simPrices = new List<double>();
        private double premium;
        private double hv;        

        public double HV => hv;
        public int iterationsCount = 10000;

        public double CalcPremium(List<double> prices, bool detrend,
            OptionContract contract, string folder)
        {
            this.prices = prices;
            this.contract = contract;
            this.detrend = detrend;
            filePath = folder;
            CalculateDistribution();
            return DoCalcPremium();
        }

        public double CalculateModelledVolatility()
        {
            const int iterations = 1000;
            double sum = 0;
            for (var i = 0; i < iterations; i++)
                sum += CalcModelledVolIteration();
            var mhv = sum / iterations;
            return mhv;
        }

        private double CalcModelledVolIteration()
        {
            var steps = (int) contract.Term;
            var deltas = new List<double>(steps);
            for (var i = 0; i < steps; i++)
                deltas.Add(100 * GetDelta());
            return CalcHvByDeltas(deltas);
        }

        private double DoCalcPremium()
        {
            double sum = 0;
            for (var i = 0; i < iterationsCount; i++)
            {
                var p = CalcPremiumForIteration();
                sum += p;
                simPrices.Add(p);
            }
            premium = sum / iterationsCount;
            StoreInFile();
            return premium;
        }

        private double CalcPremiumForIteration()
        {
            var p = contract.Price;
            var steps = (int) contract.Term;
            for (var i = 0; i < steps; i++)
            {
                var delta = GetDelta();
                p = p * Math.Exp(delta);
                if (p < 0.00001) p = 0.00001;
            }
            var s = contract.Side == Side.Call ? 1 : -1;
            var pl = s * contract.Volume * (p - contract.Price);
            return pl < 0 ? 0 : pl;
        }

        private void CalculateDistribution()
        {
            if (prices.Count < 3) return;
            CalcHistVol();

            var deltas = new List<double>(prices.Count - 1);
            for (var i = 1; i < prices.Count; i++)
                deltas.Add(Math.Log(prices[i] / prices[i - 1]));

            if (detrend)
                DetrendDeltas(deltas);
            deltas.Sort();

            //var lastDelta = deltas[0] - 1;
            for (var i = 0; i < deltas.Count; i++)
            {
                var prob = (i + 1.0) / deltas.Count;
                //if (Math.Abs(deltas[i] - lastDelta) < 0.00001)
                //    distribution[distribution.Count - 1] = new QuantProb((deltas[i] + lastDelta) / 2, prob);
                //else
                distribution.Add(new QuantProb(deltas[i], prob));
                //lastDelta = deltas[i];
            }
        }

        private static void DetrendDeltas(List<double> deltas)
        {
            var d = deltas.Average() / (deltas.Count - 1);
            for (var i = 0; i < deltas.Count; i++)
                deltas[i] = deltas[i] - d * i;
        }

        private void CalcHistVol()
        {
            var deltas = new List<double>();
            for (var i = 1; i < prices.Count; i++)
                deltas.Add(100 * (prices[i] - prices[i - 1]) / prices[i - 1]);
            hv = CalcHvByDeltas(deltas);
        }

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

            var min = simPrices.Min();
            var max = simPrices.Max();
            var kY = height / (max - min);
            foreach (var p in simPrices)
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

            bmp.Save(Path.Combine(filePath, "cumfunc.png"), ImageFormat.Png);
        }
    }
}
