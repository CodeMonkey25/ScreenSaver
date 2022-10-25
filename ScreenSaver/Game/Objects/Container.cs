using System;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;

namespace ScreenSaver.Game.Objects
{
    public class Container : BaseObject
    {
        private readonly IList<IList<BaseObject>> _gameObjects = new List<IList<BaseObject>>();
        
        #region Overrides

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            foreach (IList<BaseObject> baseObjects in _gameObjects)
            {
                foreach (BaseObject baseObject in baseObjects)
                {
                    baseObject.Dispose();
                }
                baseObjects.Clear();
            }
            _gameObjects.Clear();
        }

        public override bool Update(TimeSpan elapsedGameTime)
        {
            bool result = base.Update(elapsedGameTime);
            
            foreach (BaseObject gameObject in _gameObjects.SelectMany(go => go))
            {
                if (gameObject.Update(elapsedGameTime))
                    result = true;
            }

            return result;
        }

        public override void Draw(SKCanvas canvas)
        {
            base.Draw(canvas);
            
            foreach (BaseObject gameObject in _gameObjects.SelectMany(go => go))
            {
                gameObject.Draw(canvas);
            }
        }
        #endregion
        
        public void Add(BaseObject baseObject)
        {
            while (_gameObjects.Count <= baseObject.ZIndex)
            {
                _gameObjects.Add(new List<BaseObject>());
            }
        
            _gameObjects[baseObject.ZIndex].Add(baseObject);
        }

        public void Remove(BaseObject baseObject)
        {
            _gameObjects[baseObject.ZIndex].Remove(baseObject);
        }
    }
}