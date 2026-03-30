using AI4Dev.Goap;

var goal = new WorldState { [Fact.IsHungry] = false };

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
    });

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
    });

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
    },
    actionCosts: new()
    {
        [ActionLibrary.GoToFridge] = 1,
        [ActionLibrary.TakeRawFood] = 1,
        // no GoToStove, no CookFood, no OrderDelivery
        [ActionLibrary.EatRawFood] = 3, // only option left
    });

var planner = new GoapPlanner();

Output.Separator("ALEX — lazy, has money");
Output.PrintPlan(alex, planner.Plan(alex, goal));

Output.Separator("MARIA — no money, loves cooking");
Output.PrintPlan(maria, planner.Plan(maria, goal));

Output.Separator("SAM — desperate");
Output.PrintPlan(sam, planner.Plan(sam, goal));