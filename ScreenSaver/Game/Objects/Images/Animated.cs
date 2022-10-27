using SkiaSharp;

namespace ScreenSaver.Game.Objects.Images
{
    public class Animated : BaseObject
    {
        private readonly SKBitmap _bitmap;

        public Animated(SKBitmap bitmap)
        {
            _bitmap = bitmap;
        }
    }
}