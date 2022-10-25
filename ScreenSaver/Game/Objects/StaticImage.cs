using System;
using SkiaSharp;

namespace ScreenSaver.Game.Objects
{
    public class StaticImage : BaseObject
    {
        private SKBitmap _bitmap;

        public StaticImage(SKBitmap bitmap)
        {
            _bitmap = bitmap;
        }

        public override bool Update(TimeSpan elapsedGameTime)
        {
            return base.Update(elapsedGameTime) || true;
        }

        public override void Draw(SKCanvas canvas)
        {
            base.Draw(canvas);
            
            canvas.DrawBitmap(_bitmap, 0, 0);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _bitmap.Dispose();
        }
    }
}