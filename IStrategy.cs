using System.Collections.Generic;

public interface IStrategy
{
    void Move(Individual self, IEnumerable<Individual> population);
}