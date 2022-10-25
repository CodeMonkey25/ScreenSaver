using System;
using System.Drawing;
using ReactiveUI;
using SkiaSharp;

namespace ScreenSaver.Game.Objects
{
    public abstract class BaseObject : ReactiveObject
    {
        public Rectangle Bounds { get; set; } = Rectangle.Empty;
        public int ZIndex { get; set; } = 0;
        
        public virtual void Update(TimeSpan elapsedGameTime) { }

        public virtual void Draw(SKCanvas canvas) { }
    }
}