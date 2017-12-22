using System;
using System.Linq;

namespace OptionCalculator.Model
{
    /// <summary>
    /// candlestick data: open price, high, low and close prices
    /// 
    /// for the calculations we need Close price only
    /// but sometimes we get candlistick data in *.csv format
    /// </summary>
    public class Candle
    {
        public static readonly char[] Separators = { '[', ']', ' ', ',', ';' };

        private static readonly int[] ShortTimeframes = { 240, 60, 30, 15, 5, 1 };
        
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public float Close { get; set; }

        public DateTime TimeOpen { get; set; }

        public int Volume { get; set; }

        public Candle()
        {
        }

        public Candle(Candle candle)
        {
            Open = candle.Open;
            High = candle.High;
            Low = candle.Low;
            Close = candle.Close;
            TimeOpen = candle.TimeOpen;
            Volume = candle.Volume;
        }

        public Candle(float o, float h, float l, float c, DateTime timeOpen)
        {
            Open = o;
            High = h;
            Low = l;
            Close = c;
            TimeOpen = timeOpen;
        }

        public DateTime GetTimeClose(int timeframe)
        {
            return TimeOpen.AddMinutes(timeframe);
        }

        public override string ToString()
        {
            return ToString(";");
        }

        public string ToString(string separator, string dateTimeFormat = null)
        {
            var items = new[]
            {
                TimeOpen.ToStringUniform(dateTimeFormat),
                Open.ToStringUniformPriceFormat(true),
                High.ToStringUniformPriceFormat(true),
                Low.ToStringUniformPriceFormat(true),
                Close.ToStringUniformPriceFormat(true),
                Volume.ToString()
            };
            return string.Join(separator, items);
        }

        public static Candle ParseLine(string line, string dateTimeFormat = "yyyy.MM.dd HH:mm")
        {
            // 2011.03.24 00:00,0.83200,0.90000,0.82700,0.86700,14009
            if (line == null)
                return null;
            var parts = line.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 6) return null;
            try
            {
                var candle = new Candle(parts[2].ToFloatUniform(), parts[3].ToFloatUniform(),
                    parts[4].ToFloatUniform(), parts[5].ToFloatUniform(),
                    (parts[0] + " " + parts[1]).ToDateTimeUniform(dateTimeFormat));
                if (parts.Length > 6) candle.Volume = parts[6].ToInt();
                return candle;
            }
            catch
            {
                return null;
            }
        }

        public static DateTime RoundTime(DateTime time, int timeframe)
        {
            if (timeframe == 1440)
                return time.Date;
            if (timeframe == 10080)
            {
                var date = time.Date;
                var weekDay = (int)date.DayOfWeek;
                var deltaDays = weekDay == 0 ? 6 : weekDay - 1;
                return date.AddDays(-deltaDays);
            }
            if (timeframe == 43200)
                return new DateTime(time.Year, time.Month, 1);
            if (ShortTimeframes.Contains(timeframe))
            {
                var minutes = (int)((time - time.Date).TotalMinutes / timeframe);
                return time.Date.AddMinutes(minutes * timeframe);
            }

            throw new Exception($"Unknown timeframe: {timeframe}");
        }
    }
}
