using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.ReactiveUI;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using ScreenSaver.ViewModels;
using SkiaSharp;

namespace ScreenSaver.Views
{
    public class SkiaImageView : ReactiveUserControl<SkiaImageViewModel>
    {
        public static readonly StyledProperty<SKImage> SourceImageProperty = AvaloniaProperty.Register<SkiaImageView, SKImage>("SourceImage");

        public SKImage SourceImage
        {
            get => GetValue(SourceImageProperty);
            set => SetValue(SourceImageProperty, value);
        }
        
        public SkiaImageView()
        {
            ClipToBounds = true;
        }

        public override void Render(DrawingContext context)
        {
            // Render elements
            context.Custom(new ElementRenderOperation(new Rect(0, 0, Bounds.Width, Bounds.Height), SourceImage));
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
        
        private class ElementRenderOperation : ICustomDrawOperation
        {
            private static readonly FormattedText NoEngine = new() { Text = "Current rendering API is not Skia", Typeface = Typeface.Default, FontSize = 50, };
            private static readonly FormattedText NoImage = new() { Text = "No image has been provided", Typeface = Typeface.Default, FontSize = 50, };

            public ElementRenderOperation(Rect bounds, SKImage sourceImage)
            {
                Bounds = bounds;
                SourceImage = sourceImage;
            }

            public void Dispose()
            {
                // No-op
            }

            /// <summary>
            /// The bounds of the drawing canvas.
            /// </summary>
            public Rect Bounds { get; }

            private SKImage SourceImage { get; }
            public bool HitTest(Point p) => false;
            public bool Equals(ICustomDrawOperation? other) => false;

            public void Render(IDrawingContextImpl context)
            {
                if (context is not ISkiaDrawingContextImpl skiaDrawingContextImpl)
                {
                    context.DrawText(Brushes.White, new Point(), NoEngine.PlatformImpl);
                    return;
                }

                SKCanvas canvas = skiaDrawingContextImpl.SkCanvas;
                canvas.Clear(SKColors.Black);
                // canvas.ClipRect(SKRect.Create((float)Bounds.Width, (float)Bounds.Height));
                
                if (SourceImage == null)
                {
                    context.DrawText(Brushes.White, new Point(), NoImage.PlatformImpl);
                    return;
                }

                canvas.DrawImage(SourceImage, 0, 0);
            }
        }
    }
}