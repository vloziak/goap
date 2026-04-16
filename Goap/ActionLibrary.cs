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
    
    public static readonly GoapAction GoToBed = new (
            "GoToBed",
            pre: new WorldState { [Fact.HasBed] = true },
            effects: new WorldState { [Fact.IsAtBed] = true });
    
    public static readonly GoapAction Sleep = new (
        "Sleep",
        pre: new WorldState { [Fact.IsAtBed] = true, [Fact.IsTired] = true },
        effects: new WorldState { [Fact.IsSleeping] = true });
    
    public static readonly GoapAction WakeUp = new (
        "WakeUp",
        pre: new WorldState { [Fact.IsSleeping] = true },
        effects: new WorldState { [Fact.IsSleeping] = false, [Fact.IsTired] = false });
    
    public static readonly GoapAction PassOutOnTheFloor = new (
        "PassOutOnTheFloor",
        pre: new WorldState { },
        effects: new WorldState { [Fact.IsSick] = true, [Fact.IsTired] = false });
    
    public static readonly GoapAction GoToShower = new (
        "GoToShower",
        pre: new WorldState { [Fact.HasShower] = true },
        effects: new WorldState { [Fact.IsAtShower] = true });
    
    public static readonly GoapAction TakeShower = new (
        "TakeShower",
        pre: new WorldState { [Fact.IsAtShower] = true, [Fact.IsClean] = false },
        effects: new WorldState { [Fact.IsClean] = true });
    
    public static readonly GoapAction GoToToilet = new (
        "GoToToilet",
        pre: new WorldState { [Fact.HasToilet] = true },
        effects: new WorldState { [Fact.IsAtToilet] = true });
    
    public static readonly GoapAction UseToilet = new (
        "UseToilet",
        pre: new WorldState { [Fact.IsAtToilet] = true },
        effects: new WorldState { [Fact.IsClean] = true });
    
}