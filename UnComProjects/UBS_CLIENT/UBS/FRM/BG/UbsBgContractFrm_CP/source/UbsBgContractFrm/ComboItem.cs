namespace UbsBusiness
{
    internal class ComboItem
    {
        public readonly long Id;
        public readonly string Text;

        public ComboItem(long id, string text)
        {
            Id = id;
            Text = text ?? string.Empty;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}