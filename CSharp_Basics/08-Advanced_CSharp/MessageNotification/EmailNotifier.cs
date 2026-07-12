namespace MessageNotification
{
    public class EmailNotifier : Notifier
    {
        public EmailNotifier(string recipient) : base(recipient) { }
        public override void Send(string message)
        {
            Console.WriteLine($"EmailNotifier to {Recipient}: {Format(message)}");
        }
    }
}
