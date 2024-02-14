using System;
using ScreenSaver.Game.Engines;
using SkiaSharp;

namespace ScreenSaver.Game.Objects.Images
{
    public class Animated : BaseObject
    {
        private SKImage[] _images = Array.Empty<SKImage>();
        private int _index = 0;
        protected int SpriteCounterMax { get; set; } = 1000;
        private int _spriteCounter = 0;

        protected int SpeedX { get; set; } = 0;
        protected int SpeedY { get; set; } = 0;

        public override void Initialize(Jeeves jeeves)
        {
            base.Initialize(jeeves);
        }

        public void ExtractSprites(SKBitmap bitmap, int count)
        {
            _images = new SKImage[count];

            int width = bitmap.Width / count;
            int height = bitmap.Height;
            for (int i = 0; i < count; i++)
            {
                int left = i * width;
                int right = left + width;
                using (SKPixmap pixmap = new SKPixmap(bitmap.Info, bitmap.GetPixels()))
                using (SKPixmap subset = pixmap.ExtractSubset(new SKRectI(left, 0, right, height)))
                using (SKData data = subset.Encode(SKPngEncoderOptions.Default))
                {
                    _images[i] = SKImage.FromEncodedData(data);
                }
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            foreach (SKImage skImage in _images)
            {
                skImage.Dispose();
            }
        }
        
        public override bool Update(Jeeves jeeves)
        {
            bool needRedraw = base.Update(jeeves);
        
            if (_index >= 0 && _index < _images.Length)
            {
                
                float deltaX = SpeedX * (float)jeeves.ElapsedGameTime.TotalSeconds;
                if (deltaX != 0)
                {
                    X += deltaX;
                    needRedraw = true;
                }

                float deltaY = SpeedY * (float)jeeves.ElapsedGameTime.TotalSeconds;
                if (deltaY != 0)
                {
                    Y += deltaY;
                    needRedraw = true;
                }

                _spriteCounter += (int)jeeves.ElapsedGameTime.TotalMilliseconds;
                if (_spriteCounter > SpriteCounterMax)
                {
                    _index++;
                    if (_index >= _images.Length) _index = 0;
                    _spriteCounter %= SpriteCounterMax;
                    needRedraw = true;
                }

                float imageTop = Y;
                float imageBottom = Y + _images[_index].Height;
                float imageLeft = X;
                float imageRight = X + _images[_index].Width;
                if (imageTop > jeeves.ParentHeight || imageBottom < 0 || imageLeft > jeeves.ParentWidth || imageRight < 0)
                {
                    RequestDelete = true;
                    needRedraw = true;
                }
            }
        
            return needRedraw;
        }
        
        public override void Draw(SKCanvas canvas)
        {
            base.Draw(canvas);
            if (_index >= _images.Length) return;
            
            canvas.DrawImage(_images[_index], X, Y);
        }
    }
}