namespace AI4Dev.Goap;


class Goal {
    public string Name { get; }
    public WorldState DesiredState { get; }
    private readonly Func<Person, float> urgencyFunc;

    public Goal(string name, WorldState desiredState, Func<Person, float> urgencyFunc)
    {
        Name = name;
        DesiredState = desiredState;
        this.urgencyFunc = urgencyFunc;
    }

    public float Urgency(Person person)
    {
        var value = urgencyFunc(person);
        if (value < 0f) return 0f;
        if(value > 1f) return 1f;

        return value;
    }
}