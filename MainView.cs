using System.Linq;
using System.Drawing;
using System.Collections.Generic;

using Pamella;

public class MainView : View
{
    private int N = 15;
    private float variance = 120;

    public List<Individual> population = new List<Individual>();

    protected override void OnStart(IGraphics g)
    {
        initPopulation<Rock>(N, g.Width, g.Height);
        initPopulation<Paper>(N, g.Width, g.Height);
        initPopulation<Scissor>(N, g.Width, g.Height);

        g.SubscribeKeyDownEvent(key =>
        {
            if (key == Input.Escape)
                App.Close();
        });
    }

    protected override void OnRender(IGraphics g)
    {
        foreach (var indiviual in population)
        {
            indiviual.Move(population);

            indiviual.Draw(g);
        }

        testCollision();

        drawHUD(g);
    }

    void drawHUD(IGraphics g)
    {
        g.DrawText(
            new RectangleF(0, 0, g.Width, 100),
            "Rock Paper Scissor IA Game"
        );

        g.DrawImage(
            new RectangleF(5, 5, 85, 85),
            Rock.Img
        );
        g.DrawText(
            new RectangleF(90, 5, 85, 85),
            new Font("Comic Sans", 20f),
            population.Count(p => p is Rock).ToString()
        );

        g.DrawImage(
            new RectangleF(5, 90, 85, 85),
            Paper.Img
        );
        g.DrawText(
            new RectangleF(90, 90, 85, 85),
            new Font("Comic Sans", 20f),
            population.Count(p => p is Paper).ToString()
        );

        g.DrawImage(
            new RectangleF(5, 180, 85, 85),
            Scissor.Img
        );
        g.DrawText(
            new RectangleF(90, 180, 85, 85),
            new Font("Comic Sans", 20f),
            population.Count(p => p is Scissor).ToString()
        );
    }

    void initPopulation<T>(int N, int widht, int height) where T : Individual, new()
    {
        float baseX = rand() * widht;
        float baseY = rand() * height;

        for (int k = 0; k < N; k++)
        {
            var individual = new T();

            individual.X = baseX + (2 * rand() - 1) * variance;
            individual.Y = baseY + (2 * rand() - 1) * variance;

            population.Add(individual);
        }
    }

    void testCollision()
    {
        int len = population.Count;
        for (int i = 0; i < len; i++)
        {
            var indiviual = population[i];

            for (int j = i + 1; j < len; j++)
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