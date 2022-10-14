using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SkiaSharp;

namespace ScreenSaver.Game.Engines
{
    public abstract class Engine : ReactiveObject, IDisposable
    {
        protected CompositeDisposable Disposables { get; } = new();

        public string EngineType => GetType().Name;
        
        [Reactive] public ReactiveCommand<Unit, Unit> StartCommand { get; protected set; } = null!;
        [Reactive] public ReactiveCommand<Unit, Unit> StopCommand { get; protected set; } = null!;
        [Reactive] public float TargetFrameRate { get; set; }
        [Reactive] public bool IsEnabled { get; set; }
        [Reactive] public long Ticks { get; protected set; }
        [Reactive] public float FPS { get; protected set; }
        [Reactive] public float MinFPS { get; protected set; }
        [Reactive] public float MaxFPS { get; protected set; }
        [Reactive] public float AverageFPS { get; protected set; }
        
        [Reactive] public double Width { get; set; }
        [Reactive] public double Height { get; set; }

        [Reactive] public SKImage Image { get; protected set; }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disposables.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}