using Avalonia;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;

namespace AvaloniaUI.PrintToPDF
{
    public static class Print
    {
        public static void ToFile(string fileName, params Visual[] visuals) => ToFile(fileName, visuals.AsEnumerable());
        public static void ToFile(string fileName, IEnumerable<Visual> visuals)
        {
            var metadata = new SKDocumentPdfMetadata
            {
                Author = "Cool Developer",
                Creation = DateTime.Now,
                Creator = "Cool Developer Library",
                Keywords = "SkiaSharp, Sample, PDF, Developer, Library",
                Modified = DateTime.Now,
                Producer = "SkiaSharp",
                Subject = "SkiaSharp Sample PDF",
                Title = "Sample PDF",
            };
            using var doc = SKDocument.CreatePdf(fileName, metadata);
            foreach (var visual in visuals)
            {
                var bounds = visual.Bounds;
                var page = doc.BeginPage((float)bounds.Width, (float)bounds.Height);
                DrawingContextHelper.WrapSkiaCanvas(page, SkiaPlatform.DefaultDpi);
                DrawingContextHelper();
                // var image = new RenderTargetBitmap(new PixelSize((int)bounds.Width, (int)bounds.Height), SkiaPlatform.DefaultDpi);
                // image.Render(visual);
                // image.Save("thenas");
                
                // using var context =
                //     new DrawingContext(DrawingContextHelper.WrapSkiaCanvas(page, SkiaPlatform.DefaultDpi));
                // ImmediateRenderer.Render(visual, drawingContext);
                doc.EndPage();
            }
            doc.Close();
        }
        
        // private void DrawVisualToCanvas(SKCanvas canvas, Visual visual)
        // {
        //     using var contextImpl = DrawingContextHelper.WrapSkiaCanvas(canvas, SkiaPlatform.DefaultDpi);
        //     using var context = new DrawingContext(contextImpl);
        //
        //     using var bitmap = new RenderTargetBitmap(new PixelSize(100, 100));
        //     bitmap.Render(visual);
        //     contextImpl.DrawBitmap(bitmap.PlatformImpl, 1,
        //         new Rect(0, 0, 100, 100),
        //         new Rect(0, 0, 100, 100));
        // }
    }
}