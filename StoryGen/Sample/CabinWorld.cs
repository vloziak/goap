namespace AI4Dev.StoryGen;

// ═══════════════════════════════════════════════════════════════
//  A cozy cabin in the woods. One person lives here.
//
//  TASK 2: Add a pet (dog or cat) that lives in the cabin too.
//          The pet has its own hunger. If PetHunger gets too high,
//          the person feels guilty.
//
//  TASK 3: Add a "feed_pet" action to the Person that uses PetHunger.
//          The pet should also have its own actions.
// ═══════════════════════════════════════════════════════════════

class CabinWorld : World
{
    private double coziness = 0.5;

    /// How cozy is the cabin? Drops when no one tends to it.
    public double Coziness
    {
        get => coziness;
        set => coziness = Math.Clamp(value, 0, 1);
    }

    // TASK 2: Uncomment when you add a pet.
    // public double PetHunger { get; set; }

    public CabinWorld() : base(totalTicks: 10) { }

    public override void OnTick()
    {
        Coziness -= 0.05; // cabin gets messier over time
    }

    public override Dictionary<string, double> DisplayValues => new()
    {
        ["coziness"] = Coziness,
        // TASK 2: Add ["pet_hunger"] = PetHunger here.
    };
}
