using System.Drawing;
using System.Collections.Generic;

using Pamella;

public class Rock : Individual
{
    private static Image img = Bitmap.FromFile("rock.png");

    public override void Draw(IGraphics g)
    {
        g.DrawImage(
            new RectangleF(X - 20, Y - 20, 40, 40),
            img
        );
    }

    public override void OnTouch(Individual other, List<Individual> population)
    {
        if (other is Paper)
        {
            population.Remove(this);
            population.Add(new Paper
            {
                X = this.X,
                Y = this.Y
            });
        }
    }
}