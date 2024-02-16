using ScreenSaver.Game.Engines;
using ScreenSaver.Game.Objects.Images;

namespace ScreenSaver.Examples.AfterDarkAquarium;

public class Fish : Animated
{
    private static readonly eAquariumKeys[] AquariumKeys =
    [
        eAquariumKeys.AngelFish, eAquariumKeys.ButterflyFish, eAquariumKeys.FlounderFish, eAquariumKeys.GuppyFish,
        eAquariumKeys.JellyFish, eAquariumKeys.MinnowFish, eAquariumKeys.RedFish, eAquariumKeys.SeahorseFish,
        eAquariumKeys.StripedFish
    ];
    private static readonly eAquariumKeys[] AquariumFlippedKeys =
    [
        eAquariumKeys.AngelFishFlipped, eAquariumKeys.ButterflyFishFlipped, eAquariumKeys.FlounderFishFlipped, 
        eAquariumKeys.GuppyFishFlipped, eAquariumKeys.JellyFishFlipped, eAquariumKeys.MinnowFishFlipped,
        eAquariumKeys.RedFishFlipped, eAquariumKeys.SeahorseFishFlipped, eAquariumKeys.StripedFishFlipped
    ];
    
    public override void Initialize(Jeeves jeeves)
    {
        base.Initialize(jeeves);

        SpriteCounterMax = jeeves.Random.Next(800, 1200);

        int i = jeeves.Random.Next(0, AquariumKeys.Length);
        eAquariumKeys key = AquariumKeys[i];
        
        SpeedX = jeeves.Random.Next(10, 50);
        X = 10;
        Y = jeeves.Random.Next(10, jeeves.ScreenHeight - 10);

        if (jeeves.Random.Next(0, 2) == 0)
        {
            SpeedX *= -1;
            X = jeeves.ScreenWidth - 10;
            key = AquariumFlippedKeys[i];
        }
        Images = jeeves.RetrieveSprite(key);
    }
}