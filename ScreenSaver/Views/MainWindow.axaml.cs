using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScreenSaver.ViewModels;

namespace ScreenSaver.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        #region Direct Properties
        private static readonly DirectProperty<MainWindow, InformationView?> InfoWindowProperty = AvaloniaProperty.RegisterDirect<MainWindow, InformationView?>(
            "InfoWindow", o => o.InfoWindow, (o, v) => o.InfoWindow = v);
        public static readonly DirectProperty<MainWindow, ReactiveCommand<Unit,Unit>> ShowInformationWindowCommandProperty = AvaloniaProperty.RegisterDirect<MainWindow, ReactiveCommand<Unit,Unit>>(
            "ShowInformationWindowCommand", o => o.ShowInformationWindowCommand, (o, v) => o.ShowInformationWindowCommand = v);

        private InformationView? _infoWindow;
        private InformationView? InfoWindow
        {
            get => _infoWindow;
            set => SetAndRaise(InfoWindowProperty, ref _infoWindow, value);
        }

        private ReactiveCommand<Unit,Unit> _showInformationWindowCommand = ReactiveCommand.Create(() => {});
        public ReactiveCommand<Unit,Unit> ShowInformationWindowCommand
        {
            get => _showInformationWindowCommand;
            set => SetAndRaise(ShowInformationWindowCommandProperty, ref _showInformationWindowCommand, value);
        }
        #endregion
        
        public MainWindow()
        {
            InitializeComponent();
            ShowInformationWindowCommand = ReactiveCommand.Create(ShowInformationWindow, this.WhenAnyValue(x => x.InfoWindow, (InformationView? x) => x == null));
            
            this.WhenActivated(
                disposables =>
                {
                    this.WhenAnyValue(x => x.ViewModel!.IsFullScreen)
                        .Do(isFullScreen => WindowState = isFullScreen ? WindowState.FullScreen : WindowState.Maximized)
                        .Subscribe()
                        .DisposeWith(disposables);
                    
                    disposables.Add(ShowInformationWindowCommand);
                }
            );

#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ShowInformationWindow()
        {
            InfoWindow = new InformationView()
            {
                ViewModel = new InformationViewModel(ViewModel!.Engine),
            };
            InfoWindow.Closed += (_, _) => InfoWindow = null;
            InfoWindow.Show(this);
        }
    }
}