using System.Linq;
using ScreenSaver.Game.Engines;
using ScreenSaver.Game.Objects.Images;

namespace ScreenSaver.Examples.AfterDarkAquarium;

public class Scuba : Animated
{
    public override void Initialize(Jeeves jeeves)
    {
        base.Initialize(jeeves);

        SpriteCounterMax = jeeves.Random.Next(75, 125);
        bool isFlipped = jeeves.Random.Next(0, 2) == 0;
        eAquariumKeys key = isFlipped ? eAquariumKeys.ScubaFlipped : eAquariumKeys.Scuba;
        Images = jeeves.RetrieveSprite(key);
        SpeedX = jeeves.Random.Next(100, 150) * (isFlipped ? -1 : 1);
        X = isFlipped ? jeeves.ScreenWidth - 1 : 1 - Images.First().Width;
        Y = jeeves.Random.Next(0, jeeves.ScreenHeight - Images.First().Height);
    }
}