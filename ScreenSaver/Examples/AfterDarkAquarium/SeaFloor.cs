using ScreenSaver.Game.Engines;
using ScreenSaver.Game.Objects.Images.Static;

namespace ScreenSaver.Examples.AfterDarkAquarium
{
    public class SeaFloor : BottomTiled
    {
        public override void Initialize(Jeeves jeeves)
        {
            base.Initialize(jeeves);
            Bitmap = jeeves.RetrieveBitmap(eAquariumKeys.SeaFloor);
        }
    }
}