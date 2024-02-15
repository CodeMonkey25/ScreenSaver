using System;
using ScreenSaver.Game.Engines;
using SkiaSharp;

namespace ScreenSaver.Game.Objects.Images
{
    public class Animated : BaseObject
    {
        protected SKImage[] Images = Array.Empty<SKImage>();
        private int _index = 0;
        protected int SpriteCounterMax { get; set; } = 1000;
        private int _spriteCounter = 0;

        protected int SpeedX { get; set; } = 0;
        protected int SpeedY { get; set; } = 0;

        public override bool Update(Jeeves jeeves)
        {
            bool needRedraw = base.Update(jeeves);
        
            if (_index >= 0 && _index < Images.Length)
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
                    if (_index >= Images.Length) _index = 0;
                    _spriteCounter %= SpriteCounterMax;
                    needRedraw = true;
                }

                float imageTop = Y;
                float imageBottom = Y + Images[_index].Height;
                float imageLeft = X;
                float imageRight = X + Images[_index].Width;
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
            if (_index >= Images.Length) return;
            
            canvas.DrawImage(Images[_index], X, Y);
        }
    }
}