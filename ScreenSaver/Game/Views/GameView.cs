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
        
        public virtual void Update(TimeSpan elapsedGameTime) { }

        public virtual void Draw(SKCanvas canvas) { }
        
        public virtual void HandleInput(TimeSpan elapsedGameTime) { }
    }
}