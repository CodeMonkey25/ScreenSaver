using System.Collections.Generic;
using System.Linq;
using ScreenSaver.Game.Engines;
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

        public override bool Update(Jeeves jeeves)
        {
            bool result = base.Update(jeeves);

            foreach (BaseObject gameObject in _gameObjects.SelectMany(go => go).ToList())
            {
                jeeves.ParentWidth = Width;
                jeeves.ParentHeight = Height;
                
                if (gameObject.Update(jeeves))
                    result = true;
                
                if (gameObject.RequestDelete)
                {
                    Remove(gameObject);
                    // gameObject.Dispose();
                    jeeves.StoreObject(gameObject);
                }
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
            while (_gameObjects.Count <= baseObject.Z)
            {
                _gameObjects.Add(new List<BaseObject>());
            }
        
            _gameObjects[baseObject.Z].Add(baseObject);
        }

        public void Remove(BaseObject baseObject)
        {
            _gameObjects[baseObject.Z].Remove(baseObject);
        }
    }
}