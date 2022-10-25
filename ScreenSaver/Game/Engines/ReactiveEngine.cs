using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using SkiaSharp;

namespace ScreenSaver.Game.Engines
{
    public class ReactiveEngine : Engine
    {
        private const int FPSSampleSize = 60;
        private readonly Queue<float> _rollingFPS = new(FPSSampleSize);
        private readonly Queue<SKBitmap> _freeBitmaps = new();

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
                // .ObserveOn(RxApp.TaskpoolScheduler)
                .Do(Tick)
                .Subscribe()
                .DisposeWith(Disposables);
        }

        #region Overrides
        protected override void Dispose(bool disposing)
        {
            foreach (SKBitmap freeBitmap in _freeBitmaps)
            {
                freeBitmap.Dispose();
            }
            _freeBitmaps.Clear();
            
            base.Dispose(disposing);
        }
        #endregion
        
        private void Tick(TimeSpan elapsedGameTime)
        {
            UpdateStats(elapsedGameTime);

            CurrentGameView.HandleInput(elapsedGameTime);
            if (CurrentGameView.Update(elapsedGameTime))
            {
                SKBitmap bitmap = GetBitmap();
                Render(bitmap);
                ReleaseBitmap(bitmap);
            }
        }

        private SKBitmap GetBitmap()
        {
            while (_freeBitmaps.Any())
            {
                SKBitmap bitmap = _freeBitmaps.Dequeue();
                if (bitmap.Width == Width && bitmap.Height == Height)
                    return bitmap;
                bitmap.Dispose();
            }

            return new SKBitmap(Width, Height);
        }

        private void ReleaseBitmap(SKBitmap bitmap)
        {
            _freeBitmaps.Enqueue(bitmap);
        }
        
        private void UpdateStats(TimeSpan elapsedGameTime)
        {
            ++Ticks;
            FPS = 1.0f / (float)elapsedGameTime.TotalSeconds;

            while (FPSSampleSize <= _rollingFPS.Count)
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