using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace OptionCalculator.Model
{
    /// <summary>
    /// the class to build and save into a file a random timeseries
    /// </summary>
    public class PriceHistoryMaker
    {
        private struct PriceOnDate
        {
            public DateTime date;

            public double price;

            public PriceOnDate(DateTime date, double price)
            {
                this.date = date;
                this.price = price;
            }
        }

        private List<PriceOnDate> data = new List<PriceOnDate>();

        private static readonly Random rand = new Random();

        public void BuildTrack(double startPrice, double sigma, DateTime startDate, DateTime endDate)
        {
            do
            {
            } // TryBuildTrack can build inappropriate price data
            while (!TryBuildTrack(startPrice, sigma, startDate, endDate));
        }

        public void StoreTrack(string path)
        {
            if (data.Count < 2) return;
            using (var sw = new StreamWriter(path, false, Encoding.ASCII))
                for (var i = 1; i < data.Count; i++)
                {
                    var o = data[i - 1].price;
                    var c = data[i].price;
                    var h = Math.Max(o, c);
                    var l = Math.Min(o, c);
                    sw.WriteLine($"{data[i - 1].date:yyyy.MM.dd} 00:00,{DoubleToString(o)},{DoubleToString(h)},{DoubleToString(l)},{DoubleToString(c)},1440");
                }
        }

        private bool TryBuildTrack(double startPrice, double sigma, DateTime startDate, DateTime endDate)
        {
            data.Add(new PriceOnDate(startDate.Date, startPrice));
            for (var date = startDate.Date; date <= endDate; date = date.AddDays(1))
            {
                //startPrice += startPrice * NextGaussianDouble() * sigma;
                startPrice = startPrice * Math.Exp(NextGaussianDouble() * sigma);
                if (startPrice <= 0)
                { // hypothetically, the variable startPrice could be a negative number
                  // in this case we have to recalculate the entire time series
                    data.Clear();
                    return false;
                }
                data.Add(new PriceOnDate(date, startPrice));
            }
            return true;
        }

        private static double NextGaussianDouble()
        {
            double u, v, s;
            do
            {
                u = 2.0 * rand.NextDouble() - 1.0;
                v = 2.0 * rand.NextDouble() - 1.0;
                s = u * u + v * v;
            }
            while (s >= 1.0);

            double fac = Math.Sqrt(-2.0 * Math.Log(s) / s);
            return u * fac;
        }

        private static string DoubleToString(double d)
        {
            var fmt = d > 10000 ? "f0" : d > 1500 ? "f1" : d > 500 ? "f2" : d > 150 ? "f3" : d > 15 ? "f4" : "f5";
            return d.ToString(fmt, CultureInfo.InvariantCulture);
        }
    }
}
