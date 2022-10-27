using System;
using ReactiveUI;
using ScreenSaver.Game.Engines;
using ScreenSaver.Game.Events;
using SkiaSharp;

namespace ScreenSaver.Game.Views
{
    public abstract class GameView : ReactiveObject
    {
        public event EventHandler<GameView>? OnStateSwitched;
        public event EventHandler<GameViewEvent>? OnEventNotification;
        
        public virtual void Initialize(Jeeves jeeves) { }

        public virtual void LoadContent(Jeeves jeeves) { }

        public virtual void UnloadContent(Jeeves jeeves) { }

        public virtual bool Update(Jeeves jeeves) { return false; }

        public virtual void Draw(SKCanvas canvas) { }
        
        public virtual void HandleInput(Jeeves jeeves) { }
    }
}