using ScreenSaver.Game.Engines;
using ScreenSaver.Game.Objects.Images;
using SkiaSharp;

namespace ScreenSaver.Examples.AfterDarkAquarium;

public class Fish : Animated
{
    public override void Initialize(Jeeves jeeves)
    {
        base.Initialize(jeeves);

        SpriteCounterMax = jeeves.Random.Next(800, 1200);
        eAquariumKeys[] keys = new[]
        {
            eAquariumKeys.AngelFish, eAquariumKeys.ButterflyFish, eAquariumKeys.FlounderFish, eAquariumKeys.GuppyFish,
            eAquariumKeys.JellyFish, eAquariumKeys.MinnowFish, eAquariumKeys.RedFish, eAquariumKeys.SeahorseFish,
            eAquariumKeys.StripedFish
        };
        eAquariumKeys key = keys[jeeves.Random.Next(0, keys.Length)];
        SKBitmap bitmap = jeeves.RetrieveBitmap<AquariumView, eAquariumKeys>(key);

        SpeedX = jeeves.Random.Next(10, 50);
        X = 10;
        Y = jeeves.Random.Next(10, jeeves.ScreenHeight - 10);

        if (jeeves.Random.Next(0, 2) == 0)
        {
            SpeedX *= -1;
            X = jeeves.ScreenWidth - 10;
            SKBitmap flippedBitmap = new SKBitmap(bitmap.Width, bitmap.Height);
            using (SKCanvas canvas = new SKCanvas(flippedBitmap))
            {
                canvas.Clear();
                canvas.Scale(-1, 1, bitmap.Width / 2, 0);
                canvas.DrawBitmap(bitmap, new SKPoint());
            }
            bitmap = flippedBitmap;
        }
        ExtractSprites(bitmap, 2);
    }
}