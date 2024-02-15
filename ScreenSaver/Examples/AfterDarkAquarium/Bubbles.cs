using ScreenSaver.Game.Engines;
using ScreenSaver.Game.Objects.Images;

namespace ScreenSaver.Examples.AfterDarkAquarium;

public class Bubbles : Animated
{
    public override void Initialize(Jeeves jeeves)
    {
        base.Initialize(jeeves);
        SpriteCounterMax = jeeves.Random.Next(800, 1200);
        Images = jeeves.RetrieveSprite<AquariumView, eAquariumKeys>(eAquariumKeys.Bubbles);

        SpeedY = -25;
        Y = jeeves.ScreenHeight - 10;
        X = jeeves.Random.Next(10, jeeves.ScreenWidth - 10);
    }
}