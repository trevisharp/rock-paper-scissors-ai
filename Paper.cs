using System.Drawing;
using System.Collections.Generic;

using Pamella;

public class Paper : Individual
{
    public static readonly Image Img = Bitmap.FromFile("paper.png");

    public override void Draw(IGraphics g)
    {
        g.DrawImage(
            new RectangleF(X - 20, Y - 20, 40, 40),
            Img
        );
    }

    public override void OnTouch(Individual other, List<Individual> population)
    {
        if (other is Scissor)
        {
            population.Remove(this);
            population.Add(new Scissor
            {
                X = this.X,
                Y = this.Y
            });
        }
    }
}