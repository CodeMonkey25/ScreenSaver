using System;
using ScreenSaver.Game.Objects;
using SkiaSharp;

namespace ScreenSaver.Game.Views
{
    public abstract class GameObjectsView : GameView
    {
        private readonly Container _gameObjects = new();
        
        public override bool Update(TimeSpan elapsedGameTime)
        {
            base.Update(elapsedGameTime);
            
            return _gameObjects.Update(elapsedGameTime);
        }

        public override void Draw(SKCanvas canvas)
        {
            base.Draw(canvas);
            
            _gameObjects.Draw(canvas);
        }
        
        protected void Add(BaseObject baseObject)
        {
            _gameObjects.Add(baseObject);
        }

        protected void Remove(BaseObject baseObject)
        {
            _gameObjects.Remove(baseObject);
        }
    }
}