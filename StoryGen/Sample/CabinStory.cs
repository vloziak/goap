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
            // TASK 2: Add your second character here.
        ];
    }

    protected override string Ending(CabinWorld world)
    {
        if (world.Coziness > 0.5)
        {
            return "ENDING: A peaceful day. The cabin is warm and tidy.";
        }
        return "ENDING: The cabin is a mess, but you survived.";

        // TASK 3: Add more endings based on your second character's world state.
    }
}
