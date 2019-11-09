namespace GameTelemetryViewer.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using GameTelemetryViewer.Telemetry;

    class Program
    {
        static void Main(string[] args)
        {
            var _totalTelemetryEvents = 0;
            var source = new CancellationTokenSource();
            var token = source.Token;
            var telemetryProducer = new MockTelemetryProducer(source);
            
            Console.WriteLine("Producing some game telemtry");

            telemetryProducer.RegisterTelemetryHandler((g) =>
            {
                Console.WriteLine($"Module: {g.Module}, Function: {g.Function}, Line: {g.LineNumber}");
                _totalTelemetryEvents++;
            });

            telemetryProducer.Start();

            Console.ReadKey();
            telemetryProducer.Stop();
            Console.WriteLine($"Telemetry production has now been stopped, is cancelled {source.IsCancellationRequested}, total events = {_totalTelemetryEvents}");
            Console.ReadKey();

        }
    }
}
