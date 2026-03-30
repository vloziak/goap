namespace AI4Dev.Goap;

class Person
{
    public string Name { get; }

    public WorldState State { get; }

    // Person assigns their own cost to each action
    // Actions not in this dict are unavailable to this person
    public Dictionary<GoapAction, int> ActionCosts { get; }

    public Person(string name, WorldState state, Dictionary<GoapAction, int> actionCosts)
    {
        Name = name;
        State = state;
        ActionCosts = actionCosts;
    }

    public List<GoapAction> AvailableActions => ActionCosts.Keys.ToList();

    public int CostOf(GoapAction a) => ActionCosts[a];
}