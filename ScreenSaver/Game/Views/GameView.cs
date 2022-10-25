using System;
using ReactiveUI;
using ScreenSaver.Game.Events;
using SkiaSharp;

namespace ScreenSaver.Game.Views
{
    public abstract class GameView : ReactiveObject
    {
        public event EventHandler<GameView>? OnStateSwitched;
        public event EventHandler<GameViewEvent>? OnEventNotification;
        
        public virtual void Initialize() { }

        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }

        public virtual bool Update(TimeSpan elapsedGameTime) { return false; }

        public virtual void Draw(SKCanvas canvas) { }
        
        public virtual void HandleInput(TimeSpan elapsedGameTime) { }
    }
}