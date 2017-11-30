using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace OptionCalculator.Model
{
    public enum Side
    {
        Call = 0, Put = 1
    }

    public class Calculator
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

        private SourceCandles candles;
        private double volume;
        private double term;
        private double price;
        private double strike;
        private bool detrend;
        private Dictionary<double, double> deltaByProb = new Dictionary<double, double>();
        private List<QuantProb> distribution = new List<QuantProb>();
        private readonly Random rand = new Random();
        private Side side;
        private string filePath;
        private List<double> simPrices = new List<double>();
        private double premium;
        private double hv;

        public double HV => hv;

        public double CalcPremium(SourceCandles candles, bool detrend,
            Side side,
            double volume, double term, double price, double strike, string fileNameToStore)
        {
            this.candles = candles;
            this.volume = volume;
            this.term = term;
            this.price = price;
            this.strike = strike;
            this.detrend = detrend;
            this.side = side;
            filePath = fileNameToStore;
            CalculateDistribution(detrend);
            return DoCalcPremium();
        }

        private double DoCalcPremium()
        {
            const int iterations = 10000;
            double sum = 0;
            for (var i = 0; i < iterations; i++)
            {
                var p = CalcPremiumForIteration();
                sum += p;
                simPrices.Add(p);
            }
            premium = sum / iterations;
            StoreInFile();
            return premium;
        }

        private double CalcPremiumForIteration()
        {
            var p = price;
            var steps = (int) term;
            for (var i = 0; i < steps; i++)
            {
                var delta = GetDelta();
                p += p * delta;
                if (p < 0.00001) p = 0.00001;
            }
            var s = side == Side.Call ? 1 : -1;
            var pl = s * volume * (p - price);
            return pl < 0 ? 0 : pl;
        }

        private void CalculateDistribution(bool detrend)
        {
            var srcCandles = detrend ? candles.detrendedCandles : candles.sourceCandles;
            CalcHistVol(srcCandles);
            
            var deltas = srcCandles.Select(c => (c.Close - c.Open) / c.Open).OrderBy(c => c).ToList();
            var lastDelta = deltas[0] - 1;
            for (var i = 0; i < deltas.Count; i++)
            {
                var prob = (i + 1.0) / deltas.Count;
                if (Math.Abs(deltas[i] - lastDelta) < 0.00001)
                    distribution[distribution.Count - 1] = new QuantProb(deltas[i], prob);
                else
                    distribution.Add(new QuantProb(deltas[i], prob));
                lastDelta = deltas[i];
            }
        }

        private void CalcHistVol(List<Candle> srcCandles)
        {
            if (srcCandles.Count < 2) return;

            var days = srcCandles.Count;
            var b = 0.0;
            for (var i = 1; i < days; i++)
                b += Math.Log(srcCandles[i].Close / srcCandles[i - 1].Close);
            b /= (days - 1);

            var c = 0.0;
            for (var i = 1; i < days; i++)
            {
                var d = Math.Log(srcCandles[i].Close / srcCandles[i - 1].Close) - b;
                c += d * d;
            }
            c /= (days - 2);
            hv = Math.Sqrt(c) * Math.Sqrt(days) * 100;
        }

        private double GetDelta()
        {
            var sign = detrend ? (rand.Next(2) == 0 ? 1 : -1) : 1;
            return sign * GetUnsignedDelta();
        }

        private double GetUnsignedDelta()
        {
            var p = rand.NextDouble();
            for (var i = 0; i < distribution.Count; i++)
            {
                if (!(distribution[i].p > p)) continue;
                if (i > 0)
                {
                    var deltaP = (p - distribution[i - 1].p) / (distribution[i].p - distribution[i - 1].p);
                    var val = distribution[i - 1].val + (distribution[i].val - distribution[i - 1].val) * deltaP;
                    return val;
                }
                return distribution[0].val;
            }
            return distribution.Last().val;
        }

        private void StoreInFile()
        {
            using (var sw = new StreamWriter(filePath, false, Encoding.ASCII))
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

            bmp.Save(filePath + ".png", ImageFormat.Png);
        }
    }
}
