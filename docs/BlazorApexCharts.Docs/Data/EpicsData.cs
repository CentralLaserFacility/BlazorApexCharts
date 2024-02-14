using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bogus.Bson;
using Clf.ChannelAccess;

namespace BlazorApexCharts.Docs.Data
{
  public class EpicsData : IDisposable
  {
    private ChannelsHandler _channelsHandler = new ChannelsHandler();

    string ioc_prefix = "BENCH:HIRST1:";

    IChannel _streamChannel = null;
    double _myValue;
    bool isConnected;
    bool state;

    //whats this?
    //ChannelMonitor_ObservingChannelValueAsObject

    void IntialiseChannels()
    {
      var carrier = new MagneticResult
      {
        Date = DateTime.Now,
        fieldValue = _myValue,
        egu = "mT"
      };

      _channelsHandler.InstallChannel(_streamChannel = Hub.GetOrCreateChannel("BENCH:HIRST1:val"),
        (valueInfo, _) => UpdateGraph((double)valueInfo.Value));
    }

    private void UpdateGraph(double value)
    {
      _myValue = value;
    }

    public double GetReading()
    {
      return _myValue;
    }

    public static Task<MagneticResult[]> GetProcessVariableAsync()
    {
      return Task.FromResult(
          Enumerable.Range(1, 5).Select(index => new MagneticResult
          {
            Date = DateTime.Now,
            fieldValue = Random.Shared.Next(-2, 5),
            egu = "mT"
          }
          ).ToArray());
    }

    public void Dispose()
    {
      _channelsHandler?.Dispose();
    }

    public class MagneticResult
    {
      public DateTime Date { get; set; }
      public double fieldValue { get; set; }
      public string egu { get; set; }
    }
  }
}
