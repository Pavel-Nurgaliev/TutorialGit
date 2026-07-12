namespace TraficLightStateMachine
{
    public static class LightHelper
    {
        public static Light Next(Light current)
        {
            return current switch
            {
                Light.Red => Light.Green,
                Light.Green => Light.Yellow,
                Light.Yellow => Light.Red,
                _ => throw new ArgumentOutOfRangeException(nameof(current)),
            };
        }
    }
}
