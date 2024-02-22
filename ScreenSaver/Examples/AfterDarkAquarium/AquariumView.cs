using System;
using System.Linq;
using Avalonia.Platform;
using ScreenSaver.Game.Engines;
using ScreenSaver.Game.Views;
using SkiaSharp;

namespace ScreenSaver.Examples.AfterDarkAquarium
{
    public class AquariumView : GameObjectsView
    {
        public override void Initialize(Jeeves jeeves)
        {
            base.Initialize(jeeves);
            jeeves.Prepare<AquariumView>();
        }

        public override void LoadContent(Jeeves jeeves)
        {
            base.LoadContent(jeeves);
            jeeves.CacheEnabled = false;

            LoadBitmap(jeeves, eAquariumKeys.SeaFloor, "seafloor.jpg");
            
            LoadSprites(jeeves, eAquariumKeys.Bubbles, eAquariumKeys.BubblesFlipped, "bubbles_50.png", 2, 1, 2);
           
            LoadSprites(jeeves, eAquariumKeys.ButterflyFish, eAquariumKeys.ButterflyFishFlipped, "fish-butterfly.png", 2, 1, 2);
            LoadSprites(jeeves, eAquariumKeys.GuppyFish, eAquariumKeys.GuppyFishFlipped, "fish-guppy.png", 2, 1, 2);
            LoadSprites(jeeves, eAquariumKeys.SeahorseFish, eAquariumKeys.SeahorseFishFlipped, "fish-seahorse.png", 2, 1, 2);
            LoadSprites(jeeves, eAquariumKeys.JellyFish, eAquariumKeys.JellyFishFlipped, "fish-jelly.png", 2, 1, 2);
            LoadSprites(jeeves, eAquariumKeys.MinnowFish, eAquariumKeys.MinnowFishFlipped, "fish-minnow.png", 2, 1, 2);
            LoadSprites(jeeves, eAquariumKeys.RedFish, eAquariumKeys.RedFishFlipped, "fish-red.png", 2, 1, 2);
            LoadSprites(jeeves, eAquariumKeys.StripedFish, eAquariumKeys.StripedFishFlipped, "fish-striped.png", 2, 1, 2);
            LoadSprites(jeeves, eAquariumKeys.AngelFish, eAquariumKeys.AngelFishFlipped, "fish-angel.png", 2, 1, 2);
            LoadSprites(jeeves, eAquariumKeys.FlounderFish, eAquariumKeys.FlounderFishFlipped, "fish-flounder.png", 2, 1, 2);
            LoadSprites(jeeves, eAquariumKeys.Scuba, eAquariumKeys.ScubaFlipped, "scuba.png", 10, 3, 4);

            Add(jeeves.RetrieveObject<SeaFloor>());
        }

        public override void UnloadContent(Jeeves jeeves)
        {
            base.UnloadContent(jeeves);
            jeeves.CleanUp<AquariumView>();
        }

        public override bool Update(Jeeves jeeves)
        {
            switch (jeeves.Random.Next(10000))
            {
                case 0:
                    Add(jeeves.RetrieveObject<Scuba>());
                    break;
                case < 20:
                    Add(jeeves.RetrieveObject<Bubbles>());
                    break;
                case < 50:
                    Add(jeeves.RetrieveObject<Fish>());
                    break;
            }
            return base.Update(jeeves);
        }

        private void LoadBitmap<T>(Jeeves jeeves, T key, string fileName) where T : Enum
        {
            string assemblyName = GetType().Assembly.GetName().Name ?? string.Empty;
            jeeves.AddBitmap(key, SKBitmap.Decode(AssetLoader.Open(new Uri($"avares://{assemblyName}/Assets/{fileName}"))));
        }

        private void LoadSprites<T>(Jeeves jeeves, T key, T flippedKey, string fileName, int count, int rows, int columns) where T : Enum
        {
            string assemblyName = GetType().Assembly.GetName().Name ?? string.Empty;

            using (SKBitmap bitmap = SKBitmap.Decode(AssetLoader.Open(new Uri($"avares://{assemblyName}/Assets/{fileName}"))))
            {
                SKBitmap[] bitmaps = ExtractSprites(bitmap, count, rows, columns);
                jeeves.AddSprite(key, bitmaps.Select(SKImage.FromBitmap).ToArray());

                SKBitmap[] flippedBitmaps = bitmaps.Select(FlipBitmap).ToArray();
                jeeves.AddSprite(flippedKey, flippedBitmaps.Select(SKImage.FromBitmap).ToArray());
                
                foreach (SKBitmap skBitmap in bitmaps) { skBitmap.Dispose(); }
                foreach (SKBitmap skBitmap in flippedBitmaps) { skBitmap.Dispose(); }
            }
        }

        private static SKBitmap[] ExtractSprites(SKBitmap bitmap, int count, int rows, int columns)
        {
            SKBitmap[] bitmaps = new SKBitmap[count];

            int width = bitmap.Width / columns;
            int height = bitmap.Height / rows;
            for (int i = 0; i < count; i++)
            {
                int row = i / columns;
                int col = i % columns;
                int left = col * width;
                int right = left + width;
                int top = row * height;
                int bottom = top + height;
                
                using (SKPixmap pixmap = new SKPixmap(bitmap.Info, bitmap.GetPixels()))
                using (SKPixmap subset = pixmap.ExtractSubset(new SKRectI(left, top, right, bottom)))
                using (SKData data = subset.Encode(SKPngEncoderOptions.Default))
                {
                    bitmaps[i] = SKBitmap.Decode(data);
                }
            }

            return bitmaps;
        }

        private static SKBitmap FlipBitmap(SKBitmap bitmap)
        {
            SKBitmap flippedBitmap = new SKBitmap(bitmap.Width, bitmap.Height);
            using (SKCanvas canvas = new SKCanvas(flippedBitmap))
            {
                canvas.Clear();
                canvas.Scale(-1, 1, bitmap.Width / 2, 0);
                canvas.DrawBitmap(bitmap, new SKPoint());
            }
            return flippedBitmap;
        }
    }
}