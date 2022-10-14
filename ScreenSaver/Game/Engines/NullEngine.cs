using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using ReactiveUI;

namespace ScreenSaver.Game.Engines
{
    public class NullEngine : Engine
    {
        public NullEngine()
        {
            StartCommand = ReactiveCommand.Create(() => {}).DisposeWith(Disposables);
            StopCommand = ReactiveCommand.Create(() => {}).DisposeWith(Disposables);
            // PropertyChanging += (_, _) => throw new Exception("Cannot modify the Null Engine!");
            PropertyChanging += OnPropertyChanging;
        }

        private void OnPropertyChanging(object? sender, PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Width):
                case nameof(Height):
                    break;
                default:
                    throw new Exception("Cannot modify the Null Engine!");
            }
        }
    }
}