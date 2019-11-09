using GameTelemetryViewer.ApplicationStateManagers;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTelemetryViewer.Telemetry
{
    public class TelemetryViewModel : BindableBase
    {
        public DelegateCommand StartTelemetryCommand { get; private set; }
        public DelegateCommand StopTelemetryCommand { get; private set; }
        public ObservableCollection<GameTelemetryViewModel> Telemetry { get; private set; }

        public TelemetryViewModel()
        {
            TelemetryStateService.Instance.RegisterTelemetryHandler(Register);
            StartTelemetryCommand = new DelegateCommand(Start);
            StopTelemetryCommand = new DelegateCommand(Stop);
            Telemetry = new ObservableCollection<GameTelemetryViewModel>();
        }

        private void Register(GameTelemetry gameTelemetry)
        {
            App.Current.Dispatcher.Invoke((Action)delegate // exec on ui thread
            {
                if (Telemetry.Count == 25) { Telemetry.Clear(); }
                Telemetry.Add(new GameTelemetryViewModel(gameTelemetry.Module, gameTelemetry.Function, gameTelemetry.LineNumber));
            });           
        }

        private void Stop()
        {
            TelemetryStateService.Instance.StopTelemetry();
        }

        private void Start()
        {
            TelemetryStateService.Instance.StartTelemetry();
        }
    }
}
