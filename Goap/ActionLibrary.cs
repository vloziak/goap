namespace AI4Dev.Goap;

static class ActionLibrary
{
    public static readonly GoapAction GoToFridge = new(
        "GoToFridge",
        pre: new WorldState(),
        effects: new WorldState { [Fact.IsAtFridge] = true });

    public static readonly GoapAction TakeRawFood = new(
        "TakeRawFood",
        pre: new WorldState { [Fact.IsAtFridge] = true },
        effects: new WorldState { [Fact.HasRawFood] = true });

    public static readonly GoapAction GoToStove = new(
        "GoToStove",
        pre: new WorldState(),
        effects: new WorldState { [Fact.IsAtStove] = true });

    public static readonly GoapAction CookFood = new(
        "CookFood",
        pre: new WorldState { [Fact.IsAtStove] = true, [Fact.HasRawFood] = true },
        effects: new WorldState { [Fact.HasCookedFood] = true, [Fact.HasRawFood] = false });

    public static readonly GoapAction EatCookedFood = new(
        "EatCookedFood",
        pre: new WorldState { [Fact.HasCookedFood] = true },
        effects: new WorldState { [Fact.IsHungry] = false });

    public static readonly GoapAction OrderDelivery = new(
        "OrderDelivery",
        pre: new WorldState { [Fact.HasMoney] = true },
        effects: new WorldState { [Fact.HasFood] = true });

    public static readonly GoapAction EatFood = new(
        "EatFood",
        pre: new WorldState { [Fact.HasFood] = true },
        effects: new WorldState { [Fact.IsHungry] = false });

    public static readonly GoapAction EatRawFood = new(
        "EatRawFood",
        pre: new WorldState { [Fact.HasRawFood] = true },
        effects: new WorldState { [Fact.IsHungry] = false, [Fact.IsSick] = true });
}