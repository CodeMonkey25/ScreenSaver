using System.Drawing;
using ScreenSaver.Game.Objects;

namespace ScreenSaver.Game.Views
{
    public class NullGameView : GameView
    {
        public NullGameView()
        {
            AddGameObject(
                new TextObject()
                {
                    Text = "No valid view loaded!",
                    Bounds = new Rectangle(50, 50, 100, 100),
                }
            );
        }
    }
}