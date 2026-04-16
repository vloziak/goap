using AI4Dev.Goap;

var goals = new List<Goal>
{
    new Goal(
        "SatisfyHunger",
        new WorldState { [Fact.IsHungry] = false },
        person => person.HungerNeed),

    new Goal(
        "GetRest",
        new WorldState { [Fact.IsTired] = false },
        person => person.RestNeed),

    new Goal(
        "MaintainHygiene",
        new WorldState { [Fact.IsClean] = false },
        person => person.HygieneNeed)
};

// ── Alex — lazy, has money, hates cooking ──────────────
// OrderDelivery is cheap for him, cooking is painful
var alex = new Person(
    name: "Alex (lazy, has money)",
    state: new WorldState
    {
        [Fact.IsHungry] = true,
        [Fact.HasMoney] = true,
        [Fact.HasRawFood] = false,
        [Fact.HasFood] = false,
        [Fact.HasCookedFood] = false,
        [Fact.IsAtFridge] = false,
        [Fact.IsAtStove] = false,
        [Fact.IsSick] = false,
        
        [Fact.IsTired] = true,
        [Fact.IsSleeping] = false,
        [Fact.IsAtBed] = false,
        [Fact.HasBed] = true,
        
        [Fact.IsClean] = false,
        [Fact.HasShower] = true,
        [Fact.HasToilet] = true,
        [Fact.IsAtToilet] = false,
        [Fact.IsAtShower] = false,
    },
    actionCosts: new()
    {
        [ActionLibrary.GoToFridge] = 3,
        [ActionLibrary.TakeRawFood] = 2,
        [ActionLibrary.GoToStove] = 3,
        [ActionLibrary.CookFood] = 8, // hates cooking
        [ActionLibrary.EatCookedFood] = 1,
        [ActionLibrary.OrderDelivery] = 1, // loves ordering
        [ActionLibrary.EatFood] = 1,
        [ActionLibrary.EatRawFood] = 5, // would never
        
        [ActionLibrary.GoToBed] = 1,
        [ActionLibrary.Sleep] = 1,
        [ActionLibrary.WakeUp] = 1,
        [ActionLibrary.PassOutOnTheFloor] = 10,
        
        [ActionLibrary.GoToToilet] = 2,
        [ActionLibrary.UseToilet] = 1,
        [ActionLibrary.GoToShower] = 20,
        [ActionLibrary.TakeShower] = 5,
        
    },
    hungerNeed: 0.8f,
    restNeed: 0.3f,
    hygieneNeed: 0.6f);

// ── Maria — no money, loves cooking ───────────────────
// OrderDelivery is unavailable (no money in state + not in her action list)
// Cooking is cheap and preferred
var maria = new Person(
    name: "Maria (no money, loves cooking)",
    state: new WorldState
    {
        [Fact.IsHungry] = true,
        [Fact.HasMoney] = false,
        [Fact.HasRawFood] = false,
        [Fact.HasFood] = false,
        [Fact.HasCookedFood] = false,
        [Fact.IsAtFridge] = false,
        [Fact.IsAtStove] = false,
        [Fact.IsSick] = false,
        
        [Fact.IsTired] = true,
        [Fact.IsSleeping] = false,
        [Fact.IsAtBed] = false,
        [Fact.HasBed] = true,
        
        [Fact.IsClean] = false,
        [Fact.HasShower] = true,
        [Fact.HasToilet] = true,
        [Fact.IsAtToilet] = false,
        [Fact.IsAtShower] = false,
    },
    actionCosts: new()
    {
        [ActionLibrary.GoToFridge] = 1,
        [ActionLibrary.TakeRawFood] = 1,
        [ActionLibrary.GoToStove] = 1,
        [ActionLibrary.CookFood] = 2, // loves cooking
        [ActionLibrary.EatCookedFood] = 1,
        // no OrderDelivery — no money, not in her action list
        [ActionLibrary.EatRawFood] = 5,
        
        [ActionLibrary.GoToBed] = 1,
        [ActionLibrary.Sleep] = 1,
        [ActionLibrary.WakeUp] = 1,
        [ActionLibrary.PassOutOnTheFloor] = 10,
        
        [ActionLibrary.GoToToilet] = 5,
        [ActionLibrary.UseToilet] = 2,
        [ActionLibrary.GoToShower] = 1,
        [ActionLibrary.TakeShower] = 2,
    },
    hungerNeed: 0.1f,
    restNeed: 0.25f,
    hygieneNeed: 0.5f);

// ── Sam — desperate, nothing works ────────────────────
// No money, stove broken (CookFood not available), only raw food path left
var sam = new Person(
    name: "Sam (desperate — no money, no stove)",
    state: new WorldState
    {
        [Fact.IsHungry] = true,
        [Fact.HasMoney] = false,
        [Fact.HasRawFood] = false,
        [Fact.HasFood] = false,
        [Fact.HasCookedFood] = false,
        [Fact.IsAtFridge] = false,
        [Fact.IsAtStove] = false,
        [Fact.IsSick] = false,
        
        [Fact.IsTired] = true,
        [Fact.IsSleeping] = false,
        [Fact.IsAtBed] = false,
        [Fact.HasBed] = false,
        
        [Fact.IsClean] = false,
        [Fact.HasShower] = false,
        [Fact.HasToilet] = true,
        [Fact.IsAtToilet] = false,
        [Fact.IsAtShower] = false,
    },
    actionCosts: new()
    {
        [ActionLibrary.GoToFridge] = 1,
        [ActionLibrary.TakeRawFood] = 1,
        // no GoToStove, no CookFood, no OrderDelivery
        [ActionLibrary.EatRawFood] = 3, // only option left
        
        [ActionLibrary.GoToBed] = 1,
        [ActionLibrary.Sleep] = 1,
        [ActionLibrary.WakeUp] = 1,
        [ActionLibrary.PassOutOnTheFloor] = 10,
        
        [ActionLibrary.GoToToilet] = 5,
        [ActionLibrary.UseToilet] = 2,
    },
    hungerNeed: 0.23f,
    restNeed: 0.7f,
    hygieneNeed: 0.9f);

var selector = new GoalSelector(0.2f);
var planner = new GoapPlanner();
Output.Separator("ALEX — lazy, has money");
Output.PrintPlan(maria, selector.SelectGoalFor(alex, goals));

Output.Separator("MARIA — no money, loves cooking");
Output.PrintPlan(maria, planner.Plan(maria, goal));

Output.Separator("SAM — desperate");
Output.PrintPlan(sam, planner.Plan(sam, goal));