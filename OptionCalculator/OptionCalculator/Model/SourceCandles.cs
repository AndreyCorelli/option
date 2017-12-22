using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OptionCalculator.Model
{
    /// <summary>
    /// a class to read prices from a file
    /// the file that may contain just prices or OHLC (candle) data
    /// </summary>
    public static class SourceCandles
    {
        /// <summary>
        /// read data in OHLC format (candles), but return Close prices only
        /// </summary>
        public static List<double> ReadCandles(string path)
        {
            var prices = new List<double>();
            if (!File.Exists(path)) return prices;

            var cndl = new List<Candle>();

            using (var sr = new StreamReader(path, Encoding.ASCII))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    var c = Candle.ParseLine(line);
                    if (c != null) cndl.Add(c);
                }
            }

            if (cndl.Count == 0) return ReadScalars(path);
            prices.Add(cndl[0].Open);
            foreach (var candle in cndl)
                prices.Add(candle.Close);
            return prices;
        }

        /// <summary>
        /// read the file provided line by line
        /// each line contains one price
        /// </summary>
        private static List<double> ReadScalars(string path)
        {
            var prices = new List<double>();
            using (var sr = new StreamReader(path, Encoding.ASCII))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    var s = line.Trim().Replace(",", ".").ToDoubleUniformSafe();
                    if (s.HasValue) prices.Add(s.Value);
                }
            }
            return prices;
        }
    }
}
