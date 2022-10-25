using System;
using Avalonia;
using Avalonia.Platform;
using ScreenSaver.Game.Objects;
using ScreenSaver.Game.Views;
using SkiaSharp;

namespace ScreenSaver.Examples.AfterDarkAquarium
{
    public class AquariumView : GameObjectsView
    {
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            
            IAssetLoader? assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            if (assets == null) return;

            string name = GetType().Assembly.GetName().Name ?? string.Empty;
            SKBitmap bitmap = SKBitmap.Decode(assets.Open(new Uri($"avares://{name}/Assets/seafloor.jpg")));
            StaticImage image = new StaticImage(bitmap);
            Add(image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}