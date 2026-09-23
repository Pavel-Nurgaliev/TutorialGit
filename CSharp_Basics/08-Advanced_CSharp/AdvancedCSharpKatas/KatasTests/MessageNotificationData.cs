using MessageNotification;
using System;
using System.Collections.Generic;
using System.Text;

namespace KatasTests
{
    internal static class MessageNotificationData
    {
        public const string WelcomeMessage =
            "welcome to CompanyName, we glad to notice you are subscribed now";

        public const string FormattedWelcomeMessage =
            "Welcome to CompanyName, we glad to notice you are subscribed now.";
        public const string EmailRecipient = "me@x.com";
        public const string SmsRecipient = "+7123";

        public static IEnumerable<TestCaseData> Messages
        {
            get
            {
                yield return new TestCaseData(
                        WelcomeMessage,
                        new EmailNotifier(EmailRecipient),
                        $"EmailNotifier to {EmailRecipient}: {FormattedWelcomeMessage}")
                    .SetName("EmailNotifier_Send_WritesFormattedMessage");

                yield return new TestCaseData(
                        WelcomeMessage,
                        new SmsNotifier(SmsRecipient),
                        $"SmsNotifier to {SmsRecipient}: {FormattedWelcomeMessage}")
                    .SetName("SmsNotifier_Send_WritesFormattedMessage");
            }
        }
    }
}
