using System.Drawing;
using System.Collections.Generic;

using Pamella;

public class Scissor : Individual
{
    private static Image img = Bitmap.FromFile("scissor.png");

    public override void Draw(IGraphics g)
    {
        g.DrawImage(
            new RectangleF(X - 20, Y - 20, 40, 40),
            img
        );
    }

    public override void OnTouch(Individual other, List<Individual> population)
    {
        if (other is Rock)
        {
            population.Remove(this);
            population.Add(new Rock
            {
                X = this.X,
                Y = this.Y
            });
        }
    }
}