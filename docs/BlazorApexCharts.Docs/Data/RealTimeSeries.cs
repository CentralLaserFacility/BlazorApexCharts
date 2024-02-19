using Bogus.Bson;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApexCharts.Docs
{
    public class RealTimeSeriesGenerator
    {
        public List<RealTimeSeries> TimeSeries { get; set; } = new();
        public List<RealTimeSeries> BaselineSeries { get; set; } = new();

        public long Range { get; private set; }

        public RealTimeSeriesGenerator(int points)
        {
            SetRange(points);

            //var date = DateTimeOffset.Now.AddSeconds(-600);

            //for (int i = 0; i < points; i++)
            //{
            //    TimeSeries.Add(GenerateNewPoint(date));
            //    date = date.AddDays(1);
            //}

        }

        private void SetRange(int points)
        {
            Range = DateTimeOffset.Now.ToUnixTimeMilliseconds() - DateTimeOffset.Now.AddSeconds(-points).ToUnixTimeMilliseconds();
        }


        public RealTimeSeries GenerateNewPoint(DateTimeOffset newDate, double value)
        {
            var rnd = new Random();
            return new RealTimeSeries { Date = newDate, Value = Convert.ToDecimal(value), Quantity = rnd.Next(1, 20) };
        }

        public RealTimeSeries GenerateBasePoint(DateTimeOffset newDate, double value)
        {
          var rnd = new Random();
          return new RealTimeSeries { Date = newDate, Value = 0};
        }
  }

    public class RealTimeSeries
    {
        public long DateMilliseconds => Date.ToUnixTimeMilliseconds();
        public DateTimeOffset Date { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
    }
}
