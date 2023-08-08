using System;
using System.Collections.Generic;

public class RandomStrategy : IStrategy
{
    public void Move(Individual self, IEnumerable<Individual> population)
    {
        self.X += 10 * Random.Shared.NextSingle() - 5;
        self.Y += 10 * Random.Shared.NextSingle() - 5;
    }
}