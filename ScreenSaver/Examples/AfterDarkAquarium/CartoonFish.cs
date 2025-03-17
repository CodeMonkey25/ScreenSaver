using System.Linq;
using ScreenSaver.Game.Engines;
using ScreenSaver.Game.Objects.Images;

namespace ScreenSaver.Examples.AfterDarkAquarium;

public class CartoonFish : Animated
{
    private static readonly eAquariumKeys[] AquariumKeys =
    [
        eAquariumKeys.CartoonFishYellowFlipped, eAquariumKeys.CartoonFishBlackFlipped, eAquariumKeys.CartoonFishBlueFlipped,
        eAquariumKeys.CartoonFishGreenFlipped, eAquariumKeys.CartoonFishPurpleFlipped, eAquariumKeys.CartoonFishRedFlipped,
    ];
    private static readonly eAquariumKeys[] AquariumFlippedKeys =
    [
        eAquariumKeys.CartoonFishYellow, eAquariumKeys.CartoonFishBlack, eAquariumKeys.CartoonFishBlue,
        eAquariumKeys.CartoonFishGreen, eAquariumKeys.CartoonFishPurple, eAquariumKeys.CartoonFishRed,
    ];
    
    public override void Initialize(Jeeves jeeves)
    {
        base.Initialize(jeeves);

        SpriteCounterMax = jeeves.Random.Next(100, 150);
        bool isFlipped = jeeves.Random.Next(0, 2) == 0;
        int i = jeeves.Random.Next(0, AquariumKeys.Length);
        eAquariumKeys key = isFlipped ? AquariumFlippedKeys[i] : AquariumKeys[i];
        Images = jeeves.RetrieveSprite(key);
        SpeedX = jeeves.Random.Next(10, 50) * (isFlipped ? -1 : 1);
        X = isFlipped ? jeeves.ScreenWidth - 1 : 1 - Images.First().Width;
        Y = jeeves.Random.Next(0, jeeves.ScreenHeight - Images.First().Height);
    }
}