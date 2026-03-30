namespace AI4Dev.Goap;

class GoapPlanner
{
    public List<GoapAction> Plan(Person person, WorldState goal, bool verbose = true)
    {
        var open = new List<PlanNode>();
        var closed = new List<string>();

        open.Add(new PlanNode(new WorldState(goal), new List<GoapAction>(), 0));

        if (verbose)
        {
            Console.WriteLine($"\n── Planner running for: {person.Name} ──");
            Console.WriteLine($"State : {person.State}");
            Console.WriteLine($"Goal  : {goal}\n");
        }

        int iter = 0;

        while (open.Count > 0)
        {
            open.Sort((a, b) => a.F.CompareTo(b.F));
            var node = open[0];
            open.RemoveAt(0);
            iter++;

            if (verbose)
            {
                Console.WriteLine($"[{iter}] goal={node.RemainingGoal}  g={node.Cost} h={node.H} f={node.F}");
            }

            if (person.State.Satisfies(node.RemainingGoal))
            {
                if (verbose) Console.WriteLine($"\n✓ Plan found in {iter} iterations!\n");
                return node.Plan;
            }

            var key = node.RemainingGoal.ToString();
            if (closed.Contains(key))
            {
                continue;
            }
            closed.Add(key);

            foreach (var action in person.AvailableActions)
            {
                if (!action.IsRelevant(node.RemainingGoal))
                {
                    continue;
                }

                var newGoal = action.RegressGoal(node.RemainingGoal, person.State);
                var newPlan = new List<GoapAction> { action };
                newPlan.AddRange(node.Plan);
                var newCost = node.Cost + person.CostOf(action);

                if (verbose)
                {
                    Console.WriteLine($"  → {action.Name} | newGoal={newGoal} | cost={newCost}");
                }

                open.Add(new PlanNode(newGoal, newPlan, newCost));
            }
        }

        if (verbose)
        {
            Console.WriteLine("\n✗ No plan found.\n");
        }
        return null;
    }
}

class PlanNode(WorldState goal, List<GoapAction> plan, int cost)
{
    public WorldState RemainingGoal { get; } = goal;

    public List<GoapAction> Plan { get; } = plan;

    public int Cost { get; } = cost;

    public int H => RemainingGoal.Count;

    public int F => Cost + H;

}