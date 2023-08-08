using System.Linq;
using System.Drawing;
using System.Collections.Generic;

using Pamella;

public class MainView : View
{
    private int N = 2;
    private float variance = 120;

    public List<Individual> population = new List<Individual>();

    protected override void OnStart(IGraphics g)
    {
        var random = new RandomStrategy();
        var killer = new KillerStrategy();

        initPopulation<Rock>(N, g.Width, g.Height, killer);
        initPopulation<Paper>(N, g.Width, g.Height, killer);
        initPopulation<Scissor>(N, g.Width, g.Height, killer);

        AlwaysInvalidateMode();

        g.SubscribeKeyDownEvent(key =>
        {
            if (key == Input.Escape)
                App.Close();
        });
    }

    protected override void OnRender(IGraphics g)
    {
        g.Clear(Color.White);

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

    void initPopulation<T>(int N, int widht, int height, IStrategy strategy) where T : Individual, new()
    {
        float baseX = rand() * widht;
        float baseY = rand() * height;

        for (int k = 0; k < N; k++)
        {
            var individual = new T();

            individual.Strategy = strategy;

            individual.X = baseX + (2 * rand() - 1) * variance;
            individual.Y = baseY + (2 * rand() - 1) * variance;

            population.Add(individual);
        }
    }

    void testCollision()
    {
        var collisions = new List<(Individual, Individual)>();
        
        for (int i = 0; i < population.Count; i++)
        {
            var individual = population[i];

            for (int j = 0; j < population.Count; j++)
            {
                var other = population[j];

                var dx = individual.X - other.X;
                var dy = individual.Y - other.Y;

                if (dx * dx + dy * dy > 20 * 20)
                    continue;
                
                collisions.Add((individual, other));
                collisions.Add((other, individual));
                break;
            }
        }

        foreach (var collision in collisions)
        {
            var individual = collision.Item1;
            var other = collision.Item2;

            individual.OnTouch(other, population);
            other.OnTouch(individual, population);
        }
    }
}