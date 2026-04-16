namespace AI4Dev.StoryGen;

/// <summary>
/// Base class for shared world state.
///
/// Inherit and add typed fields for your story:
///   class ProjectWorld : World { public double Progress; public double Tension; }
///   class ShipWorld : World    { public double Hull; public double Oxygen; }
///
/// The engine only needs TotalTicks and TimeLeft.
/// Everything else is your story's domain.
/// </summary>
abstract class World(int totalTicks)
{
    public int Tick { get; set; }
    public int TotalTicks { get; } = totalTicks;

    /// 1.0 at the start, 0.0 at the last tick.
    public double TimeLeft
    {
        get { return 1.0 - (double)Tick / TotalTicks; }
    }

    /// Called each tick before characters act. Override to decay/grow world values.
    public virtual void OnTick() { }

    /// Override to return the values you want displayed each tick.
    /// Key = label, Value = 0..1.
    public abstract Dictionary<string, double> DisplayValues { get; }
}
