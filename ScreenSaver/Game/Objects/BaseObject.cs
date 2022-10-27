using System;
using ReactiveUI;
using ScreenSaver.Game.Engines;
using SkiaSharp;

namespace ScreenSaver.Game.Objects
{
    public abstract class BaseObject : ReactiveObject, IDisposable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public virtual void Initialize(Jeeves jeeves) { }
        
        public virtual bool Update(Jeeves jeeves) { return false; }

        public virtual void Draw(SKCanvas canvas) { }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose stuff here
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}