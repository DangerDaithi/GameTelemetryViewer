using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTelemetryViewer.Telemetry
{
    public class GameTelemetry
    {
        public GameTelemetry(string module, string function, int lineNumber)
        {
            Module = module ?? throw new ArgumentException(nameof(module));
            Function = function ?? throw new ArgumentException(nameof(function));
            LineNumber = lineNumber;
        }

        public string Module { get; }
        public string Function { get; }
        public int LineNumber { get; set; }
    }
}
