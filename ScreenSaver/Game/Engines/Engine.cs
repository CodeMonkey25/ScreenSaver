using System;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ScreenSaver.Game.Events;
using ScreenSaver.Game.Views;
using SkiaSharp;

namespace ScreenSaver.Game.Engines
{
    public abstract class Engine : ReactiveObject, IDisposable
    {
        #region Properties
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
        
        [Reactive] public int Width { get; set; }
        [Reactive] public int Height { get; set; }

        [Reactive] public GameView CurrentGameView { get; protected set; } = NullGameView.Instance;
        [Reactive] public SKImage? Image { get; protected set; }
        
        protected CompositeDisposable Disposables { get; } = new();
        #endregion
        
        #region Dispose
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
        #endregion
        
        #region GameView
        public void SwitchGameState(GameView gameView)
        {
            UnloadGameState();
            CurrentGameView = gameView;
            LoadGameState();
        }

        private void UnloadGameState()
        {
            if (CurrentGameView is NullGameView) return;

            CurrentGameView.OnStateSwitched -= CurrentGameView_OnStateSwitched;
            CurrentGameView.OnEventNotification -= CurrentGameView_OnEventNotification;
            CurrentGameView.UnloadContent();
            CurrentGameView = NullGameView.Instance;
        }

        private void LoadGameState()
        {
            CurrentGameView.Initialize();
            CurrentGameView.LoadContent();
            CurrentGameView.OnStateSwitched -= CurrentGameView_OnStateSwitched;
            CurrentGameView.OnStateSwitched += CurrentGameView_OnStateSwitched;
            CurrentGameView.OnEventNotification -= CurrentGameView_OnEventNotification;
            CurrentGameView.OnEventNotification += CurrentGameView_OnEventNotification;
        }
        #endregion
        
        #region Events
        private void CurrentGameView_OnStateSwitched(object? sender, GameView e)
        {
            SwitchGameState(e);
        }

        private void CurrentGameView_OnEventNotification(object? sender, GameViewEvent e)
        {
            switch (e)
            {
                case GameQuit _:
                    // Exit();
                    break;
            }
        }
        #endregion
    }
}