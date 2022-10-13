using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ScreenSaver.Game
{
    public class Engine : ReactiveObject, IDisposable
    {
        [Reactive] public double TargetFrameRate { get; set; } = 30d;
        [Reactive] public bool IsEnabled { get; set; } = false;
        
        private readonly CompositeDisposable _disposables = new();
        
        public Engine()
        {
            this
                .WhenAnyValue(x=>x.IsEnabled, x=>x.TargetFrameRate)
                // .StartWith((false, 30d))
                .Select(t =>
                {
                    (bool isEnabled, double frameRate) = t;
                    return isEnabled
                        ? Observable.Interval(TimeSpan.FromMilliseconds(1000d / frameRate))
                            .TimeInterval()
                            .Select(timeInterval => (long)timeInterval.Interval.TotalMilliseconds)
                        : Observable.Empty<long>();
                })
                .Switch()
                .Do(Tick)
                .Subscribe()
                .DisposeWith(_disposables);
            
            // Observable.Interval(TimeSpan.FromMilliseconds(1000d / DesiredFrameRate))
            //     .TimeInterval()
            //     .Where(_ => IsEnabled)
            //     .Select(interval => (long)interval.Interval.TotalMilliseconds)
            //     .Do(Tick)
            //     .Subscribe()
            //     .DisposeWith(_disposables);
        }

        private void Tick(long gameTime)
        {
            // Debug.WriteLine($"{gameTime}");
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}