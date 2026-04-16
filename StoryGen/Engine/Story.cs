namespace AI4Dev.StoryGen;

/// <summary>
/// Base class for a story.
///
/// To create a story:
///   1. Inherit from Story&lt;YourWorld&gt;
///   2. Override Title, CreateWorld, CreateCast, Ending
///   3. Call Run() to play it
/// </summary>
abstract class Story<TWorld> where TWorld : World
{
    public abstract string Title { get; }
    public bool Debug { get; set; }

    protected abstract TWorld CreateWorld();
    protected abstract List<Character> CreateCast(TWorld world);
    protected abstract string Ending(TWorld world);

    public void Run()
    {
        TWorld world = CreateWorld();
        List<Character> cast = CreateCast(world);

        Console.WriteLine("\n" + new string('=', 70));
        Console.WriteLine($"  {Title}");
        Console.WriteLine($"  Cast: {string.Join(", ", cast.Select(c => c.Name))}");
        if (Debug)
        {
            Console.WriteLine("  [DEBUG MODE — showing utility scores]");
        }
        Console.WriteLine(new string('=', 70) + "\n");

        for (int t = 1; t <= world.TotalTicks; t++)
        {
            world.Tick = t;
            world.OnTick();

            foreach (var c in cast)
            {
                c.Tick();
            }

            // Decide
            var decisions = new List<(Character who, StoryAction action,
                List<(string name, double raw, double noisy)> scores)>();

            foreach (var c in cast)
            {
                var (chosen, scores) = c.Decide();
                decisions.Add((c, chosen, scores));
            }

            // Apply
            foreach (var (_, action, _) in decisions)
            {
                action.Apply();
            }

            // World values bar
            string worldBar = $"TIME [{Bar(world.TimeLeft, 10)}]";
            foreach (var (key, val) in world.DisplayValues)
            {
                worldBar += $"  {key}=[{Bar(val, 10)}] {val * 100:F0}%";
            }

            Console.WriteLine($"  Tick {t,-2}  {worldBar}");

            foreach (var (who, action, scores) in decisions)
            {
                Console.WriteLine($"    {who.Name,-12} => {action.PickNarration()}");

                if (Debug)
                {
                    // Sort by noisy score descending, mark the chosen one
                    var sorted = scores.OrderByDescending(s => s.noisy).ToList();
                    foreach (var (name, raw, noisy) in sorted)
                    {
                        string marker = name == action.Name ? " <--" : "";
                        string bar = new string('#', Math.Clamp((int)(raw * 20), 0, 20));
                        Console.WriteLine($"      {name,-20} raw={raw:F2}  noisy={noisy:F2}  |{bar,-20}|{marker}");
                    }
                }
            }

            foreach (var c in cast)
            {
                Console.WriteLine($"    {NeedsBar(c)}");
            }

            Console.WriteLine();
        }

        Console.WriteLine(new string('-', 70));
        Console.WriteLine($"  {Ending(world)}");
    }

    static string Bar(double value, int width)
    {
        int f = Math.Clamp((int)(value * width), 0, width);
        return new string('#', f) + new string('.', width - f);
    }

    static string NeedsBar(Character c)
    {
        var bars = c.Needs.Select(n =>
            $"{n.Label}[{Bar(n.Value, 8)}]");
        return $"{c.Name,-12}   {string.Join("  ", bars)}";
    }
}
