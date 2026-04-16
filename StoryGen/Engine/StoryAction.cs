namespace AI4Dev.StoryGen;

/// <summary>
/// Something a character can do.
///
/// score:  () => double    — how much do I want to do this right now? (0 to 1)
/// effect: () => void      — what happens when I do it?
///
/// No parameters! The lambdas capture the character's fields directly.
/// Write "Panic.Value" not "n["panic"].Value".
/// Write "World.Urgency" not "proj.Urgency".
/// </summary>
class StoryAction(string name, string[] narrations, Func<double> score, Action effect)
{
    public string Name { get; } = name;

    public string PickNarration() => narrations[Random.Shared.Next(narrations.Length)];

    public double Score() => Math.Clamp(score(), 0, 1);

    public void Apply() => effect();
}
