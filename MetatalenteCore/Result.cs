namespace Metatalente.Core
{
    public struct Result
    {
        public string PointsLeft { get; }
        public string DiceResult { get; }
        public string TextResult { get; }

        public Result(string pointsLeft, string diceResult, string textResult)
        {
            PointsLeft = pointsLeft ?? throw new ArgumentNullException(nameof(pointsLeft));
            DiceResult = diceResult ?? throw new ArgumentNullException(nameof(diceResult));
            TextResult = textResult ?? throw new ArgumentNullException(nameof(textResult));
        }
    }
}