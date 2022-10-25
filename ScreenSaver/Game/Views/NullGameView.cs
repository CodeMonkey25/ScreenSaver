using SkiaSharp;

namespace ScreenSaver.Game.Views
{
    public class NullGameView : GameView
    {
        public static NullGameView Instance { get; } = new();
        
        private NullGameView() {}
        
        public override void Draw(SKCanvas canvas)
        {
            base.Draw(canvas);
            
            using (SKPaint textPaint = new SKPaint { TextSize = 48, Color = SKColors.White })
            {
                canvas.DrawText("No valid view loaded!", 50, 50, textPaint);
            }
        }
    }
}