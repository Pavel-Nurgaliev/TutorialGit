using MessageNotification;

namespace KatasTests
{
    internal class MessageNotificationTests
    {
        [Test]
        public void EmailNotifier_Send_WritesFormattedMessage()
        {
            AssertNotifierOutput(
                new EmailNotifier(MessageNotificationData.EmailRecipient),
                MessageNotificationData.WelcomeMessage,
                $"EmailNotifier to {MessageNotificationData.EmailRecipient}: {MessageNotificationData.FormattedWelcomeMessage}");
        }

        [Test]
        public void SmsNotifier_Send_WritesFormattedMessage()
        {
            AssertNotifierOutput(
                new SmsNotifier(MessageNotificationData.SmsRecipient),
                MessageNotificationData.WelcomeMessage,
                $"SmsNotifier to {MessageNotificationData.SmsRecipient}: {MessageNotificationData.FormattedWelcomeMessage}");
        }

        private static void AssertNotifierOutput(
            INotifier notifier,
            string message,
            string expected)
        {
            var originalOutput = Console.Out;

            try
            {
                using var consoleOutput = new StringWriter();
                Console.SetOut(consoleOutput);

                notifier.Send(message);

                string actual = consoleOutput.ToString().Trim();

                Assert.That(actual, Is.EqualTo(expected));
            }
            finally
            {
                Console.SetOut(originalOutput);
            }
        }
    }
}
