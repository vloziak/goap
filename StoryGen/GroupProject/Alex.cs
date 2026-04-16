namespace AI4Dev.StoryGen;

class Alex : Character<ProjectWorld>
{
    public Need Panic   = new("PNC", 0.05, 0.03);
    public Need Boredom = new("BRD", 0.50, 0.14);
    public Need Hunger  = new("HNG", 0.20, 0.08);

    public override List<Need> Needs => [Panic, Boredom, Hunger];

    public Alex(ProjectWorld world) : base("Alex", world)
    {
        Actions.Add(new("write_code",
            narrations: [
                "Opens VS Code. Types furiously. Saves. Feels like a hero.",
                "Writes 12 lines of code. Deletes 8. Net progress: 4 lines.",
                "Copies a Stack Overflow answer. Edits variable names. Ships it.",
            ],
            // Exponential on pressure: only codes when really panicking
            score: () =>
            {
                double pressure = Math.Max(Panic.Value,
                                  Math.Max(World.Urgency, World.AlexNagged));
                return Curve.Exponential(pressure, power: 1.5)
                     * Curve.Inverse(Hunger.Value * 0.3);
            },
            effect: () =>
            {
                Panic.Value -= 0.15;
                Boredom.Value += 0.20;
                World.AddProgress(Rand(0.05));
                World.Tension -= 0.08;
                World.AlexNagged = 0;
            }));

        Actions.Add(new("scroll_memes",
            narrations: [
                "\"One more meme...\" It's been 40 minutes.",
                "Sends Dana a meme. Dana does not laugh.",
                "Opens Reddit. Closes Reddit. Opens Reddit again.",
                "Watches a 10-min video essay about fonts. Why.",
            ],
            // Linear on boredom, but killed by urgency (can't meme when project is on fire)
            score: () => Curve.Linear(Boredom.Value, slope: 0.75)
                       * Curve.Inverse(Curve.Step(World.Urgency, threshold: 0.40) * 0.9),
            effect: () =>
            {
                Boredom.Value -= 0.35;
                Panic.Value += 0.04;
                World.Tension += 0.08;
            }));

        Actions.Add(new("make_noodles",
            narrations: [
                "Boils water. Adds noodles. Eats standing up, staring at the wall.",
                "Makes instant ramen. Drops an egg in it. Calls it fine dining.",
            ],
            // Step + linear: ignores hunger below 0.45, then scales
            score: () => Curve.Step(Hunger.Value, threshold: 0.45)
                       * Hunger.Value * 0.80,
            effect: () =>
            {
                Hunger.Value -= 0.45;
                Boredom.Value -= 0.05;
            }));

        Actions.Add(new("panic_text",
            narrations: [
                "\"BRO WE'RE COOKED\" -- Dana: \"speak for yourself\"",
                "Texts the group chat: \"anyone else not started?\" Silence.",
                "\"Dana how much is left\" -- Dana: \"YOUR part is left\"",
            ],
            // Step on pressure: only panics past 0.40
            score: () =>
            {
                double pressure = Math.Max(Panic.Value,
                                  Math.Max(World.Urgency, World.AlexNagged));
                return Curve.Step(pressure, threshold: 0.40)
                     * Curve.Linear(pressure, slope: 0.55);
            },
            effect: () =>
            {
                Panic.Value -= 0.08;
                World.Tension += 0.05;
                World.AlexNagged = 0;
            }));

        Actions.Add(new("reorganize_desktop",
            narrations: [
                "\"I code better with a clean desktop.\" Spends 20 min on icons.",
                "Creates a folder called 'project_final_v2_REAL'. It's empty.",
            ],
            // Boredom-driven but suppressed by urgency
            score: () => Curve.Linear(Boredom.Value, slope: 0.50)
                       * Curve.Inverse(World.Urgency * 0.8),
            effect: () =>
            {
                Boredom.Value -= 0.25;
                Panic.Value += 0.06;
                World.Tension += 0.04;
            }));

        Actions.Add(new("energy_drink",
            narrations: [
                "Cracks a Monster. Hands start shaking. Focus TBD.",
                "Third energy drink tonight. Heart rate: questionable.",
            ],
            // Logarithmic on hunger (diminishing returns) + panic boost
            score: () => Curve.Logarithmic(Hunger.Value, steepness: 2) * 0.35
                       + Curve.Step(Panic.Value, threshold: 0.30) * 0.18,
            effect: () =>
            {
                Hunger.Value -= 0.20;
                Boredom.Value -= 0.10;
            }));
    }
}
