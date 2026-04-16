namespace AI4Dev.StoryGen;

/// <summary>
/// Activation curves for score functions.
/// Each takes a value (0..1) and returns a shaped score (0..1).
///
/// Use in score lambdas:
///   score: () => Curve.Exponential(Hunger.Value, power: 2)
///   score: () => Curve.Sigmoid(Tiredness.Value, steepness: 10, midpoint: 0.5)
///
/// Linear:      constant growth.       ___/
/// Exponential: ignore until critical.  ____/
/// Logarithmic: diminishing returns.    /---
/// Sigmoid:     S-shaped threshold.     __/--
/// Step:        binary on/off.          __|--
/// Bell:        sweet spot, falls off.  _/\_
/// Inverse:     high when value is low. \___
/// </summary>
static class Curve
{
    /// Constant slope. Default is identity (output = input).
    ///   slope=1, offset=0  =>  0.5 -> 0.5
    ///   slope=2, offset=0  =>  0.3 -> 0.6
    public static double Linear(double x, double slope = 1, double offset = 0)
    {
        return Math.Clamp(slope * x + offset, 0, 1);
    }

    /// Stays low, then ramps up fast. Power > 1.
    ///   power=2  =>  0.3 -> 0.09,  0.7 -> 0.49,  0.9 -> 0.81
    ///   power=3  =>  0.3 -> 0.03,  0.7 -> 0.34,  0.9 -> 0.73
    public static double Exponential(double x, double power = 2)
    {
        return Math.Clamp(Math.Pow(x, power), 0, 1);
    }

    /// Rises fast at first, then plateaus. "Diminishing returns."
    ///   steepness=3  =>  0.1 -> 0.22,  0.5 -> 0.66,  0.9 -> 0.92
    public static double Logarithmic(double x, double steepness = 3)
    {
        return Math.Clamp(Math.Log(1 + steepness * x) / Math.Log(1 + steepness), 0, 1);
    }

    /// S-shaped. Low at start, steep in the middle, plateaus at top.
    ///   midpoint = where the curve hits 0.5
    ///   steepness = how sharp the transition is
    public static double Sigmoid(double x, double steepness = 10, double midpoint = 0.5)
    {
        return 1.0 / (1.0 + Math.Exp(-steepness * (x - midpoint)));
    }

    /// Binary. Returns 0 below threshold, 1 at or above.
    ///   threshold=0.5  =>  0.4 -> 0,  0.5 -> 1,  0.9 -> 1
    public static double Step(double x, double threshold = 0.5)
    {
        return x >= threshold ? 1.0 : 0.0;
    }

    /// Bell curve. Peaks at center, falls off on both sides.
    ///   center=0.5, width=0.2  =>  0.5 -> 1.0,  0.3 -> 0.61,  0.0 -> 0.0
    public static double Bell(double x, double center = 0.5, double width = 0.2)
    {
        return Math.Exp(-Math.Pow(x - center, 2) / (2 * width * width));
    }

    /// Inverse. High when value is low, low when value is high.
    ///   0.0 -> 1.0,  0.3 -> 0.7,  1.0 -> 0.0
    public static double Inverse(double x)
    {
        return Math.Clamp(1.0 - x, 0, 1);
    }
}
