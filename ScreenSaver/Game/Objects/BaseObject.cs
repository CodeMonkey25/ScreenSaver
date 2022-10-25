using System;
using System.Drawing;
using ReactiveUI;
using SkiaSharp;

namespace ScreenSaver.Game.Objects
{
    public abstract class BaseObject : ReactiveObject, IDisposable
    {
        public Rectangle Bounds { get; set; } = Rectangle.Empty;
        public int ZIndex { get; set; } = 0;

        public virtual bool Update(TimeSpan elapsedGameTime) { return false; }

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