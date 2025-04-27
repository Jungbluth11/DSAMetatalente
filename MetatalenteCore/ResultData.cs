namespace Metatalente.Core
{
    public struct ResultData
    {
        public string Name { get; set; }
        public (int[] quantityData, string[] quantityStrings) Quantity { get; set; }

        public void IncreaseData(int[] quantityData)
        {
            for (int i = 0; i < quantityData.Length; i++)
            {
                Quantity.quantityData[i] += quantityData[i];
            }
        }
    }
}