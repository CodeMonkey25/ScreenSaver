using System.Linq;
using ScreenSaver.Game.Engines;
using ScreenSaver.Game.Objects.Images;

namespace ScreenSaver.Examples.AfterDarkAquarium;

public class Bubbles : Animated
{
    public override void Initialize(Jeeves jeeves)
    {
        base.Initialize(jeeves);
        SpriteCounterMax = jeeves.Random.Next(600, 900);
        Images = jeeves.RetrieveSprite(eAquariumKeys.Bubbles);

        SpeedY = -25;
        Y = jeeves.ScreenHeight - 1;
        X = jeeves.Random.Next(0, jeeves.ScreenWidth - Images.First().Width);
    }
}