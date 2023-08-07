using Pamella;
using System.Drawing;

public class MainView : View
{
    protected override void OnStart(IGraphics g)
    {
        g.SubscribeKeyDownEvent(key =>
        {
            if (key == Input.Escape)
                App.Close();
        });
    }

    protected override void OnRender(IGraphics g)
    {
        g.DrawText(
            new RectangleF(0, 0, g.Width, g.Height),
            "Aplicação criada"
        );
    }
}