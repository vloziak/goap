namespace AI4Dev.Goap;

class GoapAction(string name, WorldState pre, WorldState effects)
{
    public string Name { get; } = name;

    public WorldState Pre { get; } = pre;

    public WorldState Effects { get; } = effects;

    public bool IsRelevant(WorldState goal)
    {
        foreach (var kv in Effects)
        {
            if (goal.TryGetValue(kv.Key, out var needed) && needed == kv.Value)
            {
                return true;
            }
        }
        return false;
    }

    public WorldState RegressGoal(WorldState goal, WorldState currentState)
    {
        var newGoal = new WorldState(goal);
        foreach (var kv in Effects)
        {
            if (newGoal.TryGetValue(kv.Key, out var v) && v == kv.Value)
            {
                newGoal.Remove(kv.Key);
            }
        }
        foreach (var kv in Pre)
        {
            if (!currentState.TryGetValue(kv.Key, out var v) || v != kv.Value)
            {
                newGoal[kv.Key] = kv.Value;
            }
        }
        return newGoal;
    }
}