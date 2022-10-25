using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ScreenSaver.Game.Views;

namespace ScreenSaver.Game.Engines
{
    public class NullEngine : Engine
    {
        public static NullEngine Instance { get; } = new();
        
        private NullEngine()
        {
            CurrentGameView = NullGameView.Instance;
            StartCommand = ReactiveCommand.Create(() => {}).DisposeWith(Disposables);
            StopCommand = ReactiveCommand.Create(() => {}).DisposeWith(Disposables);
            
            PropertyChanging += OnPropertyChanging;

            this.WhenAnyValue(x => x.Width, x => x.Height)
                .Throttle(TimeSpan.FromMilliseconds(250))
                .Do(
                    t =>
                    {
                        (int w, int h) = t;
                        Render(w, h);
                    }
                ).Subscribe()
                .DisposeWith(Disposables);
        }
        
        private void OnPropertyChanging(object? sender, PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Width):
                case nameof(Height):
                case nameof(Image):
                    break;
                default:
                    throw new Exception("Cannot modify the Null Engine!");
            }
        }
    }
}