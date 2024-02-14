using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ScreenSaver.Game.Engines;

namespace ScreenSaver.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        [Reactive] public bool IsFullScreen { get; private set; }
        [Reactive] internal Engine Engine { get; private set; } = NullEngine.Instance;
        [Reactive] public ReactiveCommand<Unit, Unit> ToggleFullScreenCommand { get; private set; } = null!;

        public MainWindowViewModel()
        {
            Engine = new ReactiveEngine();
            this.WhenActivated(
                disposables =>
                {
                    ToggleFullScreenCommand = ReactiveCommand.Create(() => { IsFullScreen = !IsFullScreen; }).DisposeWith(disposables);
                    Engine.DisposeWith(disposables);
                    // Engine.SwitchGameState(new AquariumView());
                }
            );
        }
    }
}