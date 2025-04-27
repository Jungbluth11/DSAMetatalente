namespace Metatalente.Core
{
    public readonly struct Result(string pointsLeft, string diceResult, string textResult)
    {
        public string PointsLeft { get; } = pointsLeft ?? throw new ArgumentNullException(nameof(pointsLeft));
        public string DiceResult { get; } = diceResult ?? throw new ArgumentNullException(nameof(diceResult));
        public string TextResult { get; } = textResult ?? throw new ArgumentNullException(nameof(textResult));
    }
}