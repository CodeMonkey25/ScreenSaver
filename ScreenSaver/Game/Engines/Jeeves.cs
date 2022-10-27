using System;
using System.Collections.Generic;
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

        private readonly Dictionary<Type, ViewStash> Stash = new();

        public void Dispose()
        {
            foreach (IDisposable viewStash in Stash.Values)
            {
                viewStash.Dispose();
            }
            Stash.Clear();
        }
        
        public void Prepare<T1>() where T1 : GameView
        {
            Stash.Add(typeof(T1), new ViewStash());
        }
        
        public void CleanUp<T>() where T : GameView
        {
            Type typeKey = typeof(T);
            if (Stash.ContainsKey(typeKey))
            {
                Stash[typeKey].Dispose();
                Stash.Remove(typeKey);
            }
        }
        
        public void AddBitmap<T1, T2>(T2 key, SKBitmap bitmap) where T1 : GameView where T2 : Enum
        {
            Type typeKey = typeof(T1);
            if (!Stash.ContainsKey(typeKey))
            {
                Stash.Add(typeKey, new ViewStash());
            }

            Stash[typeKey].AddBitmap(key, bitmap);
        }

        public SKBitmap RetrieveBitmap<T1, T2>(T2 key) where T1 : GameView where T2 : Enum
        {
            Type typeKey = typeof(T1);
            if (Stash.ContainsKey(typeKey))
            {
                return Stash[typeKey].RetrieveBitmap(key);
            }

            throw new GameException($"Unknown bitmap for View {typeKey.Name} - Key {key}");
        }

        public T2 RetrieveObject<T1, T2>() where T1 : GameView where T2 : BaseObject, new()
        {
            Type typeKey = typeof(T1);
            if (Stash.ContainsKey(typeKey))
            {
                T2? obj = Stash[typeKey].RetrieveObject<T2>();
                if (obj is null)
                {
                    obj = new T2();
                    obj.Initialize(this);
                }

                return obj;
            }

            throw new GameException($"Unknown game object for View {typeKey.Name} - Object {typeof(T2)}");
        }
    }
}