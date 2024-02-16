using System;
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

            LoadBitmap(jeeves, eAquariumKeys.SeaFloor, "seafloor.jpg");
            
            LoadSprites(jeeves, eAquariumKeys.Bubbles, "bubbles_50.png", 2);
           
            LoadSprites(jeeves, eAquariumKeys.ButterflyFish, "fish-butterfly.png", 2);
            LoadSprites(jeeves, eAquariumKeys.GuppyFish, "fish-guppy.png", 2);
            LoadSprites(jeeves, eAquariumKeys.SeahorseFish, "fish-seahorse.png", 2);
            LoadSprites(jeeves, eAquariumKeys.JellyFish, "fish-jelly.png", 2);
            LoadSprites(jeeves, eAquariumKeys.MinnowFish, "fish-minnow.png", 2);
            LoadSprites(jeeves, eAquariumKeys.RedFish, "fish-red.png", 2);
            LoadSprites(jeeves, eAquariumKeys.StripedFish, "fish-striped.png", 2);
            LoadSprites(jeeves, eAquariumKeys.AngelFish, "fish-angel.png", 2);
            LoadSprites(jeeves, eAquariumKeys.FlounderFish, "fish-flounder.png", 2);

            LoadFlippedSprites(jeeves, eAquariumKeys.ButterflyFishFlipped, "fish-butterfly.png", 2);
            LoadFlippedSprites(jeeves, eAquariumKeys.GuppyFishFlipped, "fish-guppy.png", 2);
            LoadFlippedSprites(jeeves, eAquariumKeys.SeahorseFishFlipped, "fish-seahorse.png", 2);
            LoadFlippedSprites(jeeves, eAquariumKeys.JellyFishFlipped, "fish-jelly.png", 2);
            LoadFlippedSprites(jeeves, eAquariumKeys.MinnowFishFlipped, "fish-minnow.png", 2);
            LoadFlippedSprites(jeeves, eAquariumKeys.RedFishFlipped, "fish-red.png", 2);
            LoadFlippedSprites(jeeves, eAquariumKeys.StripedFishFlipped, "fish-striped.png", 2);
            LoadFlippedSprites(jeeves, eAquariumKeys.AngelFishFlipped, "fish-angel.png", 2);
            LoadFlippedSprites(jeeves, eAquariumKeys.FlounderFishFlipped, "fish-flounder.png", 2);
            
            Add(jeeves.RetrieveObject<SeaFloor>());
        }

        public override void UnloadContent(Jeeves jeeves)
        {
            base.UnloadContent(jeeves);
            jeeves.CleanUp<AquariumView>();
        }

        public override bool Update(Jeeves jeeves)
        {
            switch (jeeves.Random.Next(1000))
            {
                case < 2:
                    Add(jeeves.RetrieveObject<Bubbles>());
                    break;
                case < 5:
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

        private void LoadSprites<T>(Jeeves jeeves, T key, string fileName, int count) where T : Enum
        {
            string assemblyName = GetType().Assembly.GetName().Name ?? string.Empty;

            using (SKBitmap bitmap = SKBitmap.Decode(AssetLoader.Open(new Uri($"avares://{assemblyName}/Assets/{fileName}"))))
            {
                SKImage[] images = ExtractSprites(bitmap, count);
                jeeves.AddSprite(key, images);
            }
        }

        private void LoadFlippedSprites<T>(Jeeves jeeves, T key, string fileName, int count) where T : Enum
        {
            string assemblyName = GetType().Assembly.GetName().Name ?? string.Empty;

            using (SKBitmap bitmap = SKBitmap.Decode(AssetLoader.Open(new Uri($"avares://{assemblyName}/Assets/{fileName}"))))
            using (SKBitmap flippedBitmap = FlipBitmap(bitmap))
            {
                SKImage[] images = ExtractSprites(flippedBitmap, count);
                jeeves.AddSprite(key, images);
            }
        }

        private static SKImage[] ExtractSprites(SKBitmap bitmap, int count)
        {
            SKImage[] images = new SKImage[count];

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
                    images[i] = SKImage.FromEncodedData(data);
                }
            }

            return images;
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