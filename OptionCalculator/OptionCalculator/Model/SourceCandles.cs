using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OptionCalculator.Model
{
    public class SourceCandles
    {
        public List<Candle> sourceCandles = new List<Candle>();

        public List<Candle> detrendedCandles = new List<Candle>();

        public bool ReadCandles(string path)
        {
            if (!File.Exists(path)) return false;

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

            if (cndl.Count == 0) return false;
            sourceCandles = cndl;
            Detrend();
            return true;
        }

        private void Detrend()
        {
            var delta = sourceCandles.Last().Close - sourceCandles.First().Open;
            delta /= sourceCandles.Count;
            detrendedCandles = sourceCandles.Select((c, i) => new Candle
            {
                Open = c.Open - delta * (i + 1),
                Close = c.Close - delta * (i + 1),
                High = c.High - delta * (i + 1),
                Low = c.Low - delta * (i + 1),
            }).ToList();
        }
    }
}
