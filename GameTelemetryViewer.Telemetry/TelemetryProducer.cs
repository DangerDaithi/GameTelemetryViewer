using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameTelemetryViewer.Telemetry
{
    public class MockTelemetryProducer : ITelemetryProducer
    {
        private CancellationTokenSource _cancellationTokenSource;
        private List<Action<GameTelemetry>> _handlers;
        private readonly int _delayBetweenTelemetryEventsMs;
        private bool _started = false; // to be explicit
        private Dictionary<int, GameTelemetry> _mockTelemetry = new Dictionary<int, GameTelemetry>() {

            {0, new GameTelemetry("main", "main()", 123) },
            {1, new GameTelemetry("routine", "loadLevel()", 3456) },
            {2, new GameTelemetry("physics", "calculate()", 13) },
            {3, new GameTelemetry("ai", "move()", 5347) },
            {4, new GameTelemetry("automation", "start()", 89) },
            {5, new GameTelemetry("input", "executeInputStream()", 34) },
            {6, new GameTelemetry("engine", "enginePost()", 123) },
            {7, new GameTelemetry("vehicle", "start()", 12) },
            {8, new GameTelemetry("vehicle", "stop()", 81) },
            {9, new GameTelemetry("player", "respawn()", 49) },
        };
        

        public MockTelemetryProducer(CancellationTokenSource cancellationTokenSource, int delayBetweenTelemetryEventsMs = 1000)
        {
            _cancellationTokenSource = cancellationTokenSource;
            _handlers = new List<Action<GameTelemetry>>();
            _delayBetweenTelemetryEventsMs = delayBetweenTelemetryEventsMs;
        }

        public void RegisterTelemetryHandler(Action<GameTelemetry> handler)
        {
            _handlers.Add(handler);
        }

        public async Task Start()
        {
            if (_started)
            {
                return;
            }
            _started = true;
            _cancellationTokenSource = new CancellationTokenSource();
            var random = new Random(DateTime.UtcNow.Millisecond);
            await Task.Run(() =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    _handlers.ForEach(h =>
                    {
                        h(_mockTelemetry[random.Next(0, 9)]);
                    });
                    Thread.Sleep(_delayBetweenTelemetryEventsMs);
                }
            });
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _started = false;
        }
    }
}
