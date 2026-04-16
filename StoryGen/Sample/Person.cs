namespace AI4Dev.StoryGen;

// ═══════════════════════════════════════════════════════════════
//  A person living alone in a cabin.
//  Currently only eats and sleeps.
//
//  TASK 1: Add a third need (Boredom) and a third action (read_book).
//          - Boredom grows at 0.10 per tick, starts at 0.3
//          - read_book: score based on Boredom.Value
//                       effect: reduces boredom, slightly increases tiredness
//          Run with Debug = true to see how it competes with eat/sleep.
//
//  TASK 3: Add a "feed_pet" action.
//          - score: high when World.PetHunger is high
//          - effect: reduces World.PetHunger, slightly increases own Hunger
// ═══════════════════════════════════════════════════════════════

class Person : Character<CabinWorld>
{
    public Need Hunger    = new("HNG", 0.3, 0.18);
    public Need Tiredness = new("TRD", 0.2, 0.15);
    // TASK 1: Add  public Need Boredom = new("BRD", 0.3, 0.10);

    public override List<Need> Needs => [Hunger, Tiredness];
    // TASK 1: Change to => [Hunger, Tiredness, Boredom];

    public Person(CabinWorld world) : base("Person", world)
    {
        Actions.Add(new("eat",
            narrations: [
                "Heats up leftover soup. Eats by the window.",
                "Makes a sandwich. Simple but satisfying.",
                "Opens a can of beans. It's honest food.",
            ],
            score: () => Hunger.Value * 0.85,
            effect: () =>
            {
                Hunger.Value -= 0.25;
                World.Coziness -= 0.05; // dishes pile up
            }));

        Actions.Add(new("sleep",
            narrations: [
                "Crawls into bed. Falls asleep instantly.",
                "Naps on the couch. Wakes up confused but rested.",
                "Dozes off in the armchair by the fire.",
            ],
            score: () => Tiredness.Value > 0.4
                ? Tiredness.Value * 0.90
                : 0.05,
            effect: () =>
            {
                Tiredness.Value -= 0.30;
                World.Coziness += 0.05; // resting keeps things calm
            }));

        // TASK 1: Add a third action here.
        //
        // Actions.Add(new("read_book",
        //     narrations: [
        //         "Picks up a novel. Reads three chapters without blinking.",
        //         "Flips through an old cookbook. Learns nothing useful.",
        //     ],
        //     score: () => ???,
        //     effect: () =>
        //     {
        //         ???
        //     }));

        // TASK 3: Add a "feed_pet" action here.
        //
        // Actions.Add(new("feed_pet",
        //     narrations: [
        //         "Fills the pet's bowl. Gets a grateful look.",
        //         "Shares some leftovers with the pet.",
        //     ],
        //     score: () => ???,
        //     effect: () =>
        //     {
        //         ???
        //     }));
    }
}
