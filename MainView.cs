using System.Drawing;
using System.Collections.Generic;

using Pamella;

public class MainView : View
{
    public List<Individual> population = new List<Individual>();

    protected override void OnStart(IGraphics g)
    {
        population.Add(new Rock()
        {
            X = 300,
            Y = 400
        });

        g.SubscribeKeyDownEvent(key =>
        {
            if (key == Input.Escape)
                App.Close();
        });
    }

    protected override void OnRender(IGraphics g)
    {
        g.DrawText(
            new RectangleF(0, 0, g.Width, 100),
            "Rock Paper Scissor IA Game"
        );

        foreach (var indiviual in population)
        {
            indiviual.Move(population);

            indiviual.Draw(g);
        }

        testCollision();
    }

    void testCollision()
    {
        for (int i = 0; i < population.Count; i++)
        {
            var indiviual = population[i];

            for (int j = i + 1; j < population.Count; j++)
            {
                var other = population[j];

                var dx = indiviual.X - other.X;
                var dy = indiviual.Y - other.Y;

                if (dx * dx + dy * dy > 20 * 20)
                    continue;
                
                indiviual.OnTouch(other, population);
                other.OnTouch(indiviual, population);

            }
        }
    }
}