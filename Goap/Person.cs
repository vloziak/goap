namespace AI4Dev.Goap;

class Person
{
    public string Name { get; }

    public WorldState State { get; }

    // Person assigns their own cost to each action
    // Actions not in this dict are unavailable to this person
    public Dictionary<GoapAction, int> ActionCosts { get; }
    
    public float HungerNeed { get; set; }
    public float RestNeed { get; set; }
    public float HygieneNeed { get; set; }
    

    public Person(string name, WorldState state, Dictionary<GoapAction, int> actionCosts, 
        float hungerNeed, float restNeed, float hygieneNeed)
    {
        Name = name;
        State = state;
        ActionCosts = actionCosts;
        HungerNeed = hungerNeed;
        RestNeed = restNeed;
        HygieneNeed = hygieneNeed;
    }

    public List<GoapAction> AvailableActions => ActionCosts.Keys.ToList();

    public int CostOf(GoapAction a) => ActionCosts[a];
}