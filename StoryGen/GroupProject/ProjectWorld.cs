namespace AI4Dev.StoryGen;

class ProjectWorld : World
{
    double _progress;
    double _tension = 0.1;
    double _alexNagged;

    public double Progress
    {
        get { return _progress; }
        set { _progress = Math.Clamp(value, 0, 1); }
    }

    public double Tension
    {
        get { return _tension; }
        set { _tension = Math.Clamp(value, 0, 1); }
    }

    public double AlexNagged
    {
        get { return _alexNagged; }
        set { _alexNagged = Math.Clamp(value, 0, 1); }
    }

    /// High when progress is low and time is running out.
    public double Urgency
    {
        get { return Math.Clamp((1 - Progress) * (1 - TimeLeft), 0, 1); }
    }

    public ProjectWorld() : base(totalTicks: 15) { }

    public void AddProgress(double amount)
    {
        Progress += amount;
    }

    public override Dictionary<string, double> DisplayValues => new()
    {
        ["progress"] = Progress,
        ["tension"] = Tension,
    };
}
