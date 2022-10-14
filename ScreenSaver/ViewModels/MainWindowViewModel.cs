using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ScreenSaver.Game;
using ScreenSaver.Game.Engines;

namespace ScreenSaver.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        [Reactive] public bool IsFullScreen { get; private set; }
        [Reactive] internal Engine Engine { get; private set; } = new NullEngine();
        public ReactiveCommand<Unit, Unit> ToggleFullScreenCommand { get; private set; } = null!;

        public MainWindowViewModel()
        {
            this.WhenActivated(
                disposables =>
                {
                    ToggleFullScreenCommand = ReactiveCommand.Create(() => { IsFullScreen = !IsFullScreen; }).DisposeWith(disposables);
                    Engine = new ReactiveEngine().DisposeWith(disposables);
                }
            );
        }
    }
}