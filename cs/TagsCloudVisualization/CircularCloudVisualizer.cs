using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace TagsCloudVisualization;

public class CircularCloudVisualizer
{
    private readonly VisualizerParams _visualizerParams;
    
    public CircularCloudVisualizer(VisualizerParams visualizerParams)
    {
        _visualizerParams = visualizerParams;
    }

    public void Visualize(CircularCloudLayouter layouter)
    {
        Visualize(layouter.GetRectangles);
    }

    public void Visualize(IEnumerable<Rectangle> rectangles)
    {
        var bitmap = GetImage(rectangles);
        SaveImage(bitmap);
    }

    private Image GetImage(IEnumerable<Rectangle> rectangles)
    {
        var bitmap = new Bitmap(_visualizerParams.Width, _visualizerParams.Height);
        var graphics = Graphics.FromImage(bitmap);
        
        graphics.FillRectangle(new SolidBrush(_visualizerParams.BgColor), new Rectangle(0, 0, _visualizerParams.Width, _visualizerParams.Height));

        var rectanglesPen = new Pen(_visualizerParams.RectangleColor);
        graphics.DrawRectangles(rectanglesPen, rectangles.ToArray());

        return bitmap;
    }

    private void SaveImage(Image bitmap)
    {
        if (!Directory.Exists(_visualizerParams.PathToFile))
            Directory.CreateDirectory(_visualizerParams.PathToFile);
        
        bitmap.Save(_visualizerParams.PathWithFileName, ImageFormat.Png);
    }
}