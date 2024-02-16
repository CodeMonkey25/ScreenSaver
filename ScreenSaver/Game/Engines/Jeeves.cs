using System;
using ReactiveUI;
using ScreenSaver.Game.Objects;
using ScreenSaver.Game.Views;
using SkiaSharp;

namespace ScreenSaver.Game.Engines
{
    public class Jeeves : ReactiveObject, IDisposable
    {
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public int ParentWidth { get; set; }
        public int ParentHeight { get; set; }
        public TimeSpan ElapsedGameTime { get; set; }
        public Random Random { get; } = new Random();

        private readonly ViewStash _stash = new();

        public void Dispose()
        {
            _stash.Dispose();
        }
        
        public void Prepare<T1>() where T1 : GameView
        {
        }
        
        public void CleanUp<T>() where T : GameView
        {
        }

        public void StoreObject(BaseObject obj)
        {
            _stash.StoreObject(obj);
        }
        
        public T RetrieveObject<T>() where T : BaseObject, new()
        {
            T? obj = _stash.RetrieveObject<T>();
            if (obj is null)
            {
                obj = new T();
            }
            obj.Initialize(this);

            return obj;
        }
        
        public void AddBitmap<TEnum>(TEnum key, SKBitmap bitmap) where TEnum : Enum
        {
            _stash.AddBitmap(key, bitmap);
        }

        public SKBitmap RetrieveBitmap<TEnum>(TEnum key) where TEnum : Enum
        {
            return _stash.RetrieveBitmap(key);
        }

        public void AddSprite<TEnum>(TEnum key, SKImage[] images) where TEnum : Enum
        {
            _stash.AddSprite(key, images);
        }
 
        public SKImage[] RetrieveSprite<TEnum>(TEnum key) where TEnum : Enum
        {
            return _stash.RetrieveSprite(key);
        }
    }
}