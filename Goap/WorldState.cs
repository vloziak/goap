namespace AI4Dev.Goap;

class WorldState : Dictionary<Fact, bool>
{
    public WorldState() { }
    public WorldState(WorldState other) : base(other) { }

    public bool Satisfies(WorldState required)
    {
        foreach (var kv in required)
        {
            if (!TryGetValue(kv.Key, out var val) || val != kv.Value)
            {
                return false;
            }
        }
        return true;
    }

    public override string ToString() => "{" + 
        string.Join(", ", this.Select(kv => $"{kv.Key}:{kv.Value}")) + "}";
}