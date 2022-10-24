using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using ScreenSaver.Game.Events;
using ScreenSaver.Game.Objects;
using SkiaSharp;

namespace ScreenSaver.Game.Views
{
    public abstract class GameView : ReactiveObject
    {
        public event EventHandler<GameView>? OnStateSwitched;
        public event EventHandler<GameViewEvent>? OnEventNotification;
        
        protected bool Debug { get; set; }
        
        private readonly IList<IList<GameObject>> _gameObjects = new List<IList<GameObject>>();
        
        public virtual void HandleInput(TimeSpan elapsedGameTime) { }

        public virtual void Update(TimeSpan elapsedGameTime)
        {
            foreach (GameObject gameObject in _gameObjects.SelectMany(go => go))
            {
                gameObject.Update(elapsedGameTime);
            }
        }

        public virtual void Draw(SKCanvas canvas)
        {
            foreach (GameObject gameObject in _gameObjects.SelectMany(go => go))
            {
                if (Debug)
                {
                    gameObject.RenderBoundingBoxes(canvas);
                }
                gameObject.Draw(canvas);
            }
        }
        
        protected void AddGameObject(GameObject gameObject)
        {
            while (_gameObjects.Count <= gameObject.ZIndex)
            {
                _gameObjects.Add(new List<GameObject>());
            }
        
            _gameObjects[gameObject.ZIndex].Add(gameObject);
        }

        protected void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects[gameObject.ZIndex].Remove(gameObject);
        }

        public virtual void Initialize() { }

        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }
    }
}