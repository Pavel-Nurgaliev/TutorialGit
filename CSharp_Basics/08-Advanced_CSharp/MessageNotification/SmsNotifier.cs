namespace MessageNotification
{
    public class SmsNotifier : Notifier
    {
        public SmsNotifier(string recipient) : base(recipient) { }
        public override void Send(string message)
        {
            Console.WriteLine($"SmsNotifier to {Recipient}: {Format(message)}");
        }
    }
}
