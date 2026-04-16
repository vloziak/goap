namespace AI4Dev.StoryGen;

/// <summary>
/// Base class for a story character.
///
/// TWorld is your story's world type (e.g. ProjectWorld, CabinWorld).
/// Characters access it via this.World with full type safety.
///
/// To create a character:
///   1. Inherit from Character&lt;YourWorld&gt;
///   2. Add public Need fields
///   3. Override Needs to return them as a list
///   4. In the constructor, add actions to Actions list
/// </summary>
abstract class Character<TWorld> : Character where TWorld : World
{
    public new TWorld World { get; }

    protected Character(string name, TWorld world) : base(name, world)
    {
        World = world;
    }
}

/// <summary>
/// Non-generic base. The engine works with this.
/// </summary>
abstract class Character
{
    public string Name { get; }
    public World World { get; }
    public abstract List<Need> Needs { get; }
    public List<StoryAction> Actions { get; } = [];

    static readonly StoryAction Idle = new("idle",
        narrations: ["Stands around doing nothing.", "Stares blankly. No idea what to do."],
        score: () => 0.01,
        effect: () => { });

    protected Character(string name, World world)
    {
        Name = name;
        World = world;
    }

    /// <summary>
    /// Pick the best action. Returns the action and all scored candidates.
    /// </summary>
    public (StoryAction chosen, List<(string name, double raw, double noisy)> scores) Decide()
    {
        StoryAction? best = null;
        double bestScore = -1;
        var allScores = new List<(string name, double raw, double noisy)>();

        foreach (var action in Actions.Append(Idle))
        {
            double raw = action.Score();
            double noisy = raw * (0.6 + Random.Shared.NextDouble() * 0.8);
            allScores.Add((action.Name, raw, noisy));
            if (noisy > bestScore)
            {
                bestScore = noisy;
                best = action;
            }
        }

        return (best!, allScores);
    }

    public void Tick()
    {
        foreach (var need in Needs)
        {
            need.Tick();
        }
    }

    /// Helper: randomize a base value by +-50%.
    protected static double Rand(double baseValue)
    {
        return baseValue * (0.5 + Random.Shared.NextDouble());
    }
}
