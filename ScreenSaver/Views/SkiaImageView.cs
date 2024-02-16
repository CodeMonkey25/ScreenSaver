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
        public static readonly StyledProperty<SKBitmap?> SourceBitmapProperty = AvaloniaProperty.Register<SkiaImageView, SKBitmap?>(nameof(SourceBitmap));

        public SKBitmap? SourceBitmap
        {
            get => GetValue(SourceBitmapProperty);
            set => SetValue(SourceBitmapProperty, value);
        }
        
        public SkiaImageView()
        {
            ClipToBounds = true;
        }

        public override void Render(DrawingContext context)
        {
            // Render elements
            context.Custom(new ElementRenderOperation(new Rect(0, 0, Bounds.Width, Bounds.Height), SourceBitmap));
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
        
        private class ElementRenderOperation : ICustomDrawOperation
        {
            // private readonly FormattedText _noEngine = new FormattedText("Current rendering API is not Skia", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, 50, null);
            // private readonly IFormattedTextImpl _noImage = new FormattedText("No image has been provided", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, 50, null).PlatformImpl;

            public ElementRenderOperation(Rect bounds, SKBitmap? sourceBitmap)
            {
                Bounds = bounds;
                SourceBitmap = sourceBitmap;
            }

            public void Dispose()
            {
                // No-op
            }

            /// <summary>
            /// The bounds of the drawing canvas.
            /// </summary>
            public Rect Bounds { get; }

            private SKBitmap? SourceBitmap { get; }
            public bool HitTest(Point p) => false;
            public bool Equals(ICustomDrawOperation? other) => false;

            public void Render(ImmediateDrawingContext context)
            {
                ISkiaSharpApiLeaseFeature? skia = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
                if (skia is null)
                {
                    // context.DrawText(Brushes.White, new Point(), _noEngine);
                    return;
                }

                using (ISkiaSharpApiLease lease = skia.Lease())
                {
                    SKCanvas canvas = lease.SkCanvas;
                    canvas.Clear(SKColors.Black);

                    if (SourceBitmap == null)
                    {
                        canvas.DrawText("No image has been provided", 0, 0, new SKPaint(new SKFont(SKTypeface.Default, 50)));
                        return;
                    }

                    canvas.DrawBitmap(SourceBitmap, 0, 0);
                }
            }
        }
    }
}