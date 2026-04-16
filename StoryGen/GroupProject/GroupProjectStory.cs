namespace AI4Dev.StoryGen;

class GroupProjectStory : Story<ProjectWorld>
{
    public override string Title => "Group Project Night -- Deadline: 8 AM";

    protected override ProjectWorld CreateWorld()
    {
        return new ProjectWorld();
    }

    protected override List<Character> CreateCast(ProjectWorld world)
    {
        return [new Alex(world), new Dana(world)];
    }

    protected override string Ending(ProjectWorld world)
    {
        if (world.Progress >= 0.80) { return "ENDING: Submitted! Dana carried, Alex vibed. Grade: A-."; }
        if (world.Progress >= 0.65) { return "ENDING: \"It compiles, ship it.\" Last commit at 7:58 AM. Grade: B-."; }
        if (world.Progress >= 0.55) { return "ENDING: Half-baked. Dana writes an apology email. Grade: C."; }
        return "ENDING: Submitted a README and a prayer. Grade: D.";
    }
}
