namespace Metatalente.Core
{
    public struct ResultData
    {
        public string Name { get; set; }
        public (int[] quantityData, string[] quantityStrings) quantity { get; set; }

        public void IncreaseData(int[] quantityData)
        {
            for (int i = 0; i < quantityData.Length; i++)
            {
                quantity.quantityData[i] += quantityData[i];
            }
        }
    }
}