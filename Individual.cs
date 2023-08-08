using System.Collections.Generic;

using Pamella;

public abstract class Individual
{
    public IStrategy Strategy { get; set; }

    public float X { get; set; }
    public float Y { get; set; }

    public void Move(IEnumerable<Individual> population)
        => Strategy?.Move(this, population);

    public abstract void Draw(IGraphics g);
    public abstract void OnTouch(Individual other, List<Individual> population);

    public abstract bool IsTarget(Individual other);
    public abstract bool IsEnemy(Individual other);
}