using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ScreenSaver.Game;

namespace ScreenSaver.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        [Reactive] public bool IsFullScreen { get; private set; }
        public ReactiveCommand<Unit, Unit> StartCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleFullScreenCommand { get; }

        internal Engine Engine { get; } = new();

        public MainWindowViewModel()
        {
            StartCommand = ReactiveCommand.Create(StartEngine, this.WhenAnyValue(x => x.Engine.IsEnabled, x => !x));
            StopCommand = ReactiveCommand.Create(StopEngine, this.WhenAnyValue(x => x.Engine.IsEnabled));
            ToggleFullScreenCommand = ReactiveCommand.Create(ToggleFullScreen);
            
            this.WhenActivated(
                disposables =>
                {
                    disposables.Add(StartCommand);
                    disposables.Add(StopCommand);
                    disposables.Add(ToggleFullScreenCommand);
                    disposables.Add(Engine);
                }
            );
        }

        #region Commands
        private void StartEngine()
        {
            Engine.IsEnabled = true;
        }

        private void StopEngine()
        {
            Engine.IsEnabled = false;
        }

        private void ToggleFullScreen()
        {
            IsFullScreen = !IsFullScreen;
        }
        #endregion
    }
}