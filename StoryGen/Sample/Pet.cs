namespace AI4Dev.StoryGen;

class Pet : Character<CabinWorld>
{
    public Need Cuddles = new("CUD", 0.3, 0.12);
    public Need Drama   = new("DRM", 0.35, 0.14);

    public override List<Need> Needs => [Cuddles, Drama];

    public Pet(CabinWorld world) : base("Pet", world)
    {
        Actions.Add(new("ask_for_cuddles",
            narrations: [
                "Climbs into their lap like this was always the plan.",
                "Demands affection with an intense unblinking stare.",
                "Headbutts their hand until cuddles begin.",
            ],
            score: () => Cuddles.Value * 0.85,
            effect: () =>
            {
                Cuddles.Value -= 0.30;
                World.Coziness += 0.05;
            }));

        Actions.Add(new("cause_drama",
            narrations: [
                "Knocks something off the table for emotional emphasis.",
                "Stares at the wall and acts like something terrible is there.",
                "Sprints across the cabin as if chased by invisible forces.",
            ],
            score: () => Drama.Value * 0.90,
            effect: () =>
            {
                Drama.Value -= 0.35;
                World.Coziness -= 0.06;
            }));

        Actions.Add(new("nap_dramatically",
            narrations: [
                "Falls asleep in the most inconvenient spot possible.",
                "Curls into a tiny tragic comma on the blanket.",
                "Takes a deeply theatrical nap by the fire.",
            ],
            score: () => 0.08 + (Drama.Value * 0.25),
            effect: () =>
            {
                Drama.Value -= 0.10;
                Cuddles.Value += 0.08;
                World.Coziness += 0.03;
            }));
        
        Actions.Add(new("destroy_something",
            narrations: [
                "Knocks a cup off the table just to make a point.",
                "Attacks the blanket like it started the argument.",
                "Sends a small object to the floor with perfect precision.",
            ],
            score: () => World.PetHunger > 0.7 ? World.PetHunger : 0.04,
            effect: () =>
            {
                World.Coziness -= 0.15;
                Drama.Value -= 0.12;
            }));
    }
}