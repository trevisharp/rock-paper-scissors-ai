using System;
using System.Collections.Generic;

public class KillerStrategy : IStrategy
{
    public void Move(Individual self, IEnumerable<Individual> population)
    {
        int count = 0;
        float x = 0;
        float y = 0;

        foreach (var individual in population)
        {
            if (!self.IsTarget(individual))
                continue;
            
            count++;
            x += individual.X;
            y += individual.Y;
        }

        if (count == 0)
            return;

        var ox = x / count;
        var oy = y / count;

        var dx = self.X - ox;
        var dy = self.Y - oy;

        var mod = MathF.Sqrt(dx * dx + dy * dy);

        self.X -= 5 * dx / mod;
        self.Y -= 5 * dy / mod;
    }
}