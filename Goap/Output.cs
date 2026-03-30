namespace AI4Dev.Goap;

static class Output
{
    public static void PrintPlan(Person person, List<GoapAction> plan)
    {
        Console.WriteLine($"Result for [{person.Name}]:");
        if (plan == null)
        {
            Console.WriteLine("  No plan found.");
            return;
        }
        int total = 0;
        foreach (var a in plan)
        {
            var cost = person.CostOf(a);
            Console.WriteLine($"  → {a.Name} (cost {cost})");
            total += cost;
        }
        Console.WriteLine($"  Total cost: {total}");
    }

    public static void Separator(string title)
    {
        Console.WriteLine("\n" + new string('═', 55));
        Console.WriteLine($"  {title}");
        Console.WriteLine(new string('═', 55));
    }
}