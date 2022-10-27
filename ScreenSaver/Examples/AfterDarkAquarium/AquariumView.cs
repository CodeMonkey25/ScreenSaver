using System;
using System.Collections.Generic;
using Avalonia;
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

            IEnumerable<(eAquariumKeys key, string fileName)> bitmaps = new[]
            {
                (eAquariumKeys.SeaFloor, "seafloor.jpg"),
                (eAquariumKeys.Bubbles, "bubbles_50.png"),
                (eAquariumKeys.ButterflyFish, "fish-butterfly.png"),
                (eAquariumKeys.GuppyFish, "fish-guppy.png"),
                (eAquariumKeys.SeahorseFish, "fish-seahorse.png"),
                (eAquariumKeys.JellyFish, "fish-jelly.png"),
                (eAquariumKeys.MinnowFish, "fish-minnow.png"),
                (eAquariumKeys.RedFish, "fish-red.png"),
                (eAquariumKeys.StripedFish, "fish-striped.png"),
                (eAquariumKeys.AngelFish, "fish-angel.png"),
                (eAquariumKeys.FlounderFish, "fish-flounder.png"),
            };
            LoadBitmaps(jeeves, bitmaps);
            
            Add(jeeves.RetrieveObject<AquariumView, SeaFloor>());
        }

        public override void UnloadContent(Jeeves jeeves)
        {
            base.UnloadContent(jeeves);
            jeeves.CleanUp<AquariumView>();
        }

        private void LoadBitmaps<T>(Jeeves jeeves, IEnumerable<(T key, string fileName)> bitmaps ) where T : Enum
        {
            IAssetLoader? assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            if (assets == null) return;
            
            string assemblyName = GetType().Assembly.GetName().Name ?? string.Empty;
            foreach ((T key, string fileName) in bitmaps)
            {
                jeeves.AddBitmap<AquariumView, T>(
                    key, 
                    SKBitmap.Decode(assets.Open(new Uri($"avares://{assemblyName}/Assets/{fileName}")))
                );
            }
        }
    }
}