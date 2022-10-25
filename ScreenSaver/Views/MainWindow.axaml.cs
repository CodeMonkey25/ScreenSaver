using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScreenSaver.Game.Engines;
using ScreenSaver.ViewModels;

namespace ScreenSaver.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        #region Direct Properties

        private static readonly DirectProperty<MainWindow, InformationView?> InfoWindowProperty =
            AvaloniaProperty.RegisterDirect<MainWindow, InformationView?>(
                "InfoWindow", o => o.InfoWindow, (o, v) => o.InfoWindow = v);

        public static readonly DirectProperty<MainWindow, ReactiveCommand<Unit, Unit>>
            ShowInformationWindowCommandProperty =
                AvaloniaProperty.RegisterDirect<MainWindow, ReactiveCommand<Unit, Unit>>(
                    "ShowInformationWindowCommand", o => o.ShowInformationWindowCommand,
                    (o, v) => o.ShowInformationWindowCommand = v);

        private InformationView? _infoWindow;

        private InformationView? InfoWindow
        {
            get => _infoWindow;
            set => SetAndRaise(InfoWindowProperty, ref _infoWindow, value);
        }

        private ReactiveCommand<Unit, Unit> _showInformationWindowCommand = ReactiveCommand.Create(() => { });

        private ReactiveCommand<Unit, Unit> ShowInformationWindowCommand
        {
            get => _showInformationWindowCommand;
            set => SetAndRaise(ShowInformationWindowCommandProperty, ref _showInformationWindowCommand, value);
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(OnActivated);
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnActivated(CompositeDisposable disposables)
        {
            ShowInformationWindowCommand = ReactiveCommand.Create(
                ShowInformationWindow,
                this.WhenAnyValue(x => x.InfoWindow, (InformationView? x) => x == null)
            ).DisposeWith(disposables);

            this.WhenAnyValue(x => x.ViewModel!.IsFullScreen)
                .Do(isFullScreen => WindowState = isFullScreen ? WindowState.FullScreen : WindowState.Maximized)
                .Subscribe()
                .DisposeWith(disposables);

            SkiaImageView view = this.FindControl<SkiaImageView>("SkiaView");
            view.GetObservable(BoundsProperty)
                .Do(rect =>
                {
                    if (ViewModel?.Engine is not { } engine)
                    {
                        return;
                    }

                    engine.Width = (int)rect.Width;
                    engine.Height = (int)rect.Height;
                })
                .Subscribe()
                .DisposeWith(disposables);
        }

        private void ShowInformationWindow()
        {
            InformationViewModel model = new();
            if (ViewModel?.Engine is ReactiveEngine engine)
            {
                model.Engine = engine;
            }

            InfoWindow = new InformationView() { ViewModel = model, };
            InfoWindow.Closed += (_, _) =>
            {
                InfoWindow.ViewModel.Engine = NullEngine.Instance;
                InfoWindow = null;
            };
            InfoWindow.Show(this);
        }
    }
}