namespace AI4Dev.Goap;

class GoalSelector {
    private readonly float threshold;

    public GoalSelector(float threshold = 0.2f)
    {
        this.threshold = threshold;
    }

    public Goal? SelectGoalFor(Person person, List<Goal> goals)
    {
        Goal? bestGoal = null;
        float bestUrgency = -1f;

        foreach (var goal in goals)
        {
            var urgency = goal.Urgency(person);

            if (urgency > bestUrgency)
            {
                bestUrgency = urgency;
                bestGoal = goal;
            }
        }

        if (bestUrgency < threshold)
        {
            return null;
        }

        return bestGoal;
    }
}