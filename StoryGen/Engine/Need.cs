namespace AI4Dev.StoryGen;

/// <summary>
/// A drive that grows each tick.
/// 0 = satisfied, 1 = desperate.
/// </summary>
class Need(string label, double initial, double growthPerTick)
{
    public string Label { get; } = label;
    public double Value { get; set; } = Math.Clamp(initial, 0, 1);
    public double GrowthPerTick { get; } = growthPerTick;

    public void Tick()
    {
        double noise = 0.7 + Random.Shared.NextDouble() * 0.6;
        Value = Math.Clamp(Value + GrowthPerTick * noise, 0, 1);
    }
}
