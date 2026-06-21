namespace ConditionStatementPractice
{
    internal struct DemeritPoint
    {
        public DemeritPoint(int speedOverLimit)
        {
            int divisor = 5;

            Amount = (speedOverLimit + divisor - 1) / divisor;
        }

        public int Amount { get; private set; }
    }
}