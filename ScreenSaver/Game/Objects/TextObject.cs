using SkiaSharp;

namespace ScreenSaver.Game.Objects
{
    public class TextObject : BaseObject
    {
        public string Text { get; set; } = string.Empty;

        public override void Draw(SKCanvas canvas)
        {
            base.Draw(canvas);

            using (SKPaint textPaint = new() { TextSize = 48, Color = SKColors.White })
            {
                canvas.DrawText(Text, Bounds.X, Bounds.Y, textPaint);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Text = string.Empty;
        }
    }
}