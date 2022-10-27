using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using ScreenSaver.Game.Objects;
using SkiaSharp;

namespace ScreenSaver.Game.Views
{
    public class ViewStash : ReactiveObject, IDisposable
    {
        private readonly Dictionary<Enum, SKBitmap> _bitmaps = new();
        private readonly Dictionary<Type, Queue<BaseObject>> _baseObjects = new();

        public void Dispose()
        {
            foreach (SKBitmap skBitmap in _bitmaps.Values)
            {
                skBitmap.Dispose();
            }
            _bitmaps.Clear();
        }

        public void AddBitmap<T>(T key, SKBitmap bitmap) where T : Enum
        {
            _bitmaps[key] = bitmap;
        }

        public SKBitmap RetrieveBitmap<T>(T key) where T : Enum
        {
            return _bitmaps[key];
        }

        public void StoreObject<T>(T obj) where T : BaseObject
        {
            Type typeKey = typeof(T);
            if (!_baseObjects.ContainsKey(typeKey))
            {
                _baseObjects.Add(typeKey, new Queue<BaseObject>());
            }
            _baseObjects[typeKey].Enqueue(obj);
        }
        
        public T? RetrieveObject<T>() where T : BaseObject
        {
            Type typeKey = typeof(T);
            if (_baseObjects.ContainsKey(typeKey))
            {
                if (_baseObjects[typeKey].Any())
                {
                    return (T)_baseObjects[typeKey].Dequeue();
                }
            }

            return null;
        }
    }
}