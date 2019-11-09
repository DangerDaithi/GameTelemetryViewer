using GameTelemetryViewer.Telemetry;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameTelemetryViewer.ApplicationStateManagers
{
    public class TelemetryStateService : BindableBase
    {
        private static TelemetryStateService _instance;
        private static readonly object _instanceLock = new object();
        private ITelemetryProducer _telemetryProducer;

        private TelemetryStateService()
        {
            _telemetryProducer = new MockTelemetryProducer(new CancellationTokenSource(), 100);
        }

        public static TelemetryStateService Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new TelemetryStateService();
                    }
                    return _instance;
                }
            }
        }

        public void StartTelemetry()
        {
            _telemetryProducer.Start();
        }

        public void StopTelemetry()
        {
            _telemetryProducer.Stop();
        }

        public void RegisterTelemetryHandler(Action<GameTelemetry> handler)
        {
            _telemetryProducer.RegisterTelemetryHandler(handler);
        }
    }
}
