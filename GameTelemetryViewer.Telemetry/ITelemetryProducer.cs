using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameTelemetryViewer.Telemetry
{
    public interface ITelemetryProducer
    {
        void RegisterTelemetryHandler(Action<GameTelemetry> handler);
        Task Start();
        void Stop();
    }
}