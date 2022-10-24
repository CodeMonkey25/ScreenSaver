using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using SkiaSharp;

namespace ScreenSaver.Game.Engines
{
    public class ReactiveEngine : Engine
    {
        private const int ROLLING_SIZE = 60;
        private readonly Queue<float> _rollingFPS = new(ROLLING_SIZE);
        
        public ReactiveEngine()
        {
            TargetFrameRate = 30f;
            
            StartCommand = ReactiveCommand.Create(
                () => { IsEnabled = true; },
                this.WhenAnyValue(x => x.IsEnabled, x => !x)
            ).DisposeWith(Disposables);
            
            StopCommand = ReactiveCommand.Create(
                () => { IsEnabled = false; },
                this.WhenAnyValue(x => x.IsEnabled)
            ).DisposeWith(Disposables);
            
            this
                .WhenAnyValue(x=>x.IsEnabled, x=>x.TargetFrameRate)
                // .StartWith((false, 30d))
                .SubscribeOn(NewThreadScheduler.Default)
                .ObserveOn(NewThreadScheduler.Default)
                .Select(t =>
                {
                    (bool isEnabled, float frameRate) = t;
                    return isEnabled
                        ? Observable.Interval(TimeSpan.FromMilliseconds(1000d / frameRate))
                            .TimeInterval()
                            .Select(timeInterval => timeInterval.Interval)
                        : Observable.Empty<TimeSpan>();
                })
                .Switch()
                .Do(Tick)
                .Subscribe()
                .DisposeWith(Disposables);
            
            // Observable.Interval(TimeSpan.FromMilliseconds(1000d / DesiredFrameRate))
            //     .TimeInterval()
            //     .Where(_ => IsEnabled)
            //     .Select(interval => (long)interval.Interval.TotalMilliseconds)
            //     .Do(Tick)
            //     .Subscribe()
            //     .DisposeWith(_disposables);
        }

        private void Tick(TimeSpan elapsedGameTime)
        {
            // Debug.WriteLine($"{elapsedGameTime}");
            UpdateStats(elapsedGameTime);
            using (SKBitmap bitmap = new SKBitmap((int)Width, (int)Height))
            using (SKCanvas canvas = new SKCanvas(bitmap))
            {
                canvas.Clear();
                
                CurrentGameView.HandleInput(elapsedGameTime);
                CurrentGameView.Update(elapsedGameTime);
                CurrentGameView.Draw(canvas);
                Image = SKImage.FromBitmap(bitmap);
            }
        }

        private void UpdateStats(TimeSpan elapsedGameTime)
        {
            ++Ticks;
            FPS = 1.0f / (float)elapsedGameTime.TotalSeconds;

            while (ROLLING_SIZE <= _rollingFPS.Count)
            {
                _rollingFPS.Dequeue();
            }
            _rollingFPS.Enqueue(FPS);

            float sum = 0.0f;
            float max = float.MinValue;
            float min = float.MaxValue;
            foreach (float fps in _rollingFPS)
            {
                sum += fps;
                if (fps > max) max = fps;
                if (fps < min) min = fps;
            }

            MinFPS = min;
            MaxFPS = max;
            AverageFPS = sum / _rollingFPS.Count;
        }
    }
}