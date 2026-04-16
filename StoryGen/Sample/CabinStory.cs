namespace AI4Dev.StoryGen;

// ═══════════════════════════════════════════════════════════════
//  TASKS:
//    1. Add a third need + action to Person (Boredom + read_book)
//    2. Add a second character (e.g. a pet) with its own needs
//       and 2 actions. Add shared state to CabinWorld.
//    3. Connect them: add an action to Person that reacts to the
//       second character's world state. Update the ending.
//    4. Create your own story from scratch in a new folder:
//       your own World, 2+ Characters, 3+ actions each,
//       at least 3 different endings. Use Curve helpers.
// ═══════════════════════════════════════════════════════════════

class CabinStory : Story<CabinWorld>
{
    public override string Title => "A Day in the Cabin";

    protected override CabinWorld CreateWorld()
    {
        return new CabinWorld();
    }

    protected override List<Character> CreateCast(CabinWorld world)
    {
        return [
            new Person(world),
            new Pet(world),
            // TASK 2: Add your second character here.
        ];
    }

    protected override string Ending(CabinWorld world)
    {
        if (world.Coziness > 0.6 && world.PetHunger < 0.4)
        {
            return "ENDING: A peaceful day. The cabin is warm and tidy.";
        }

        if (world.Coziness < 0.005)
        {
            return "ENDING: The cabin is a disaster zone. Something clearly went wrong today.";
        }

        if (world.Coziness > 0.45 && world.PetHunger < 0.75)
        {
            return "ENDING: Not perfect, but cozy enough to call it a decent day.";
        }

        if (world.PetHunger > 0.9)
        {
            return "ENDING: The pet stares at you with deep betrayal. Some mistakes cannot be undone.";
        }

        return "ENDING: Everyone survived, which is already a kind of success.";

        // TASK 3: Add more endings based on your second character's world state.
    }
}
