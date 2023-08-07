using System.Drawing;
using System.Collections.Generic;

using Pamella;

public class Rock : Individual
{
    public static readonly Image Img = Bitmap.FromFile("rock.png");

    public override void Draw(IGraphics g)
    {
        g.DrawImage(
            new RectangleF(X - 20, Y - 20, 40, 40),
            Img
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