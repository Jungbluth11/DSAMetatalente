namespace Metatalente.Core;

public readonly record struct Result(string PointsLeft, string DiceResult, string TextResult)
{
    public string PointsLeft { get; } = PointsLeft ?? throw new ArgumentNullException(nameof(PointsLeft));
    public string DiceResult { get; } = DiceResult ?? throw new ArgumentNullException(nameof(DiceResult));
    public string TextResult { get; } = TextResult ?? throw new ArgumentNullException(nameof(TextResult));
}
