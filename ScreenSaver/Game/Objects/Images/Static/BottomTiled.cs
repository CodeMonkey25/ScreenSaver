using ScreenSaver.Game.Engines;
using SkiaSharp;

namespace ScreenSaver.Game.Objects.Images.Static
{
    public class BottomTiled : BaseObject
    {
        protected SKBitmap? Bitmap { get; set; }
        
        private int _parentWidth, _parentHeight;

        public override bool Update(Jeeves jeeves)
        {
            bool needUpdate = base.Update(jeeves);

            if (Bitmap != null)
            {
                if (_parentWidth != jeeves.ParentWidth || _parentHeight != jeeves.ParentHeight)
                {
                    _parentWidth = jeeves.ParentWidth;
                    _parentHeight = jeeves.ParentHeight;
                    needUpdate = true;
                }
            }

            return needUpdate;
        }

        public override void Draw(SKCanvas canvas)
        {
            base.Draw(canvas);
            if (Bitmap == null) return;
            
            int x = 0;
            int y = _parentHeight - Bitmap.Height;

            while (x < _parentWidth)
            {
                canvas.DrawBitmap(Bitmap, x, y);
                x += Bitmap.Width;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Bitmap?.Dispose();
        }
    }
}