namespace MessageNotification
{
    //abstract class = shared implementation + state for a family of related types. We use it for abandon copy-pasting methods between classes. We can inherit only one abstract class. Can consists some logic or implementations (in ctors, in methods that is not abstract)
    public abstract class Notifier : INotifier
    {
        protected Notifier(string recipient)
        {
            Recipient = recipient;
        }
        public string Recipient { get; private set; }
        protected string Format(string message)
        {
            return $"{message[0].ToString().ToUpper()}{message.Substring(1)}.";
        }
        public abstract void Send(string message);
    }
}
