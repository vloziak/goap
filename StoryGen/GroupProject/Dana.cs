namespace AI4Dev.StoryGen;

class Dana : Character<ProjectWorld>
{
    public Need Panic   = new("PNC", 0.15, 0.03);
    public Need Burnout = new("BRN", 0.10, 0.11);
    public Need Anger   = new("ANG", 0.00, 0.06);

    public override List<Need> Needs => [Panic, Burnout, Anger];

    public Dana(ProjectWorld world) : base("Dana", world)
    {
        Actions.Add(new("write_code",
            narrations: [
                "Writes a clean function with comments and tests. Commits.",
                "Refactors the entire module. Adds error handling. Pushes.",
                "Implements the feature properly. Writes documentation.",
            ],
            score: () =>
            {
                double pressure = Math.Max(Panic.Value, World.Urgency);
                // Sigmoid on burnout: works fine until burnout > 0.75, then drops sharply
                return pressure * 0.85 * Curve.Sigmoid(Curve.Inverse(Burnout.Value), steepness: 8, midpoint: 0.3);
            },
            effect: () =>
            {
                Panic.Value -= 0.12;
                Burnout.Value += 0.18;
                World.AddProgress(Rand(0.08));
                World.Tension -= 0.05;
            }));

        Actions.Add(new("nag_alex",
            narrations: [
                "\"Alex. ALEX. Have you pushed anything yet?\"",
                "Sends Alex a passive-aggressive screenshot of the commit log.",
                "\"I'm not carrying this project alone.\" (She is.)",
            ],
            // Step: only nags when tension crosses 0.30
            score: () => Curve.Step(World.Tension, threshold: 0.30)
                       * (World.Tension * 0.70 + Anger.Value * 0.30),
            effect: () =>
            {
                Anger.Value -= 0.20;
                World.Tension -= 0.10;
                World.AlexNagged = 0.60;
            }));

        Actions.Add(new("take_break",
            narrations: [
                "Steps away. Makes tea. Stares out the window for 5 minutes.",
                "Puts on lo-fi beats. Closes eyes. Breathes.",
                "Goes for a short walk around the block. Comes back calmer.",
            ],
            // Exponential on burnout: ignores low burnout, urgent when high
            // Inverse urgency: won't break when project is on fire
            score: () => Curve.Exponential(Burnout.Value, power: 2)
                       * Curve.Inverse(World.Urgency) * 0.80,
            effect: () =>
            {
                Burnout.Value -= 0.35;
                Anger.Value -= 0.05;
            }));

        Actions.Add(new("review_code",
            narrations: [
                "Reviews the code. Finds 3 bugs. Fixes them silently.",
                "\"This function returns nothing.\" Alex: \"it returns vibes\"",
            ],
            // Bell on burnout: reviews when moderately tired (sweet spot)
            score: () => Curve.Step(World.Progress, threshold: 0.20)
                       * Curve.Bell(Burnout.Value, center: 0.35, width: 0.20)
                       * 0.50,
            effect: () =>
            {
                Anger.Value += 0.08;
                Burnout.Value += 0.08;
                World.AddProgress(Rand(0.03));
            }));

        Actions.Add(new("vent_to_friend",
            narrations: [
                "Texts a friend: \"never doing group projects again.\"",
                "Calls her roommate. Rants for 8 minutes. Feels 2% better.",
            ],
            // Logarithmic on anger: diminishing returns from venting
            score: () => Curve.Step(Anger.Value, threshold: 0.35)
                       * Curve.Logarithmic(Anger.Value, steepness: 3) * 0.55,
            effect: () =>
            {
                Anger.Value -= 0.20;
                Burnout.Value -= 0.10;
            }));

        Actions.Add(new("do_alex_part",
            narrations: [
                "Gives up waiting. Does Alex's section herself. Seething.",
                "\"Fine. I'll do it myself.\" Opens Alex's empty file. Types angrily.",
            ],
            // Exponential on urgency: only when things are really bad
            score: () => Curve.Exponential(World.Urgency, power: 2)
                       * Curve.Step(World.Urgency, threshold: 0.40)
                       * Curve.Inverse(World.Progress) * 0.85,
            effect: () =>
            {
                Burnout.Value += 0.22;
                Anger.Value += 0.12;
                World.AddProgress(Rand(0.10));
                World.Tension += 0.10;
            }));
    }
}
