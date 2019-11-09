using Prism.Mvvm;

namespace GameTelemetryViewer.Telemetry
{
    public class GameTelemetryViewModel : BindableBase
    {

        private string _module;
        private string _function;
        private int _lineNumber;

        public GameTelemetryViewModel(string module, string function, int lineNumber)
        {
            _module = module;
            _function = function;
            _lineNumber = lineNumber;
        }

        public string Module
        {
            get
            {
                return _module;
            }
            set
            {
                SetProperty(ref _module, value);
            }
        }

        public string Function
        {
            get
            {
                return _function;
            }
            set
            {
                SetProperty(ref _function, value);
            }
        }

        public int LineNumber
        {
            get
            {
                return _lineNumber;
            }
            set
            {
                SetProperty(ref _lineNumber, value);
            }
        }
    }
}