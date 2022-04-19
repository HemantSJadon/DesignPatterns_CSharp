using System;
using System.Collections.Generic;

namespace DesignPatterns_CSharp.structural
{
    public abstract class BaseNotifier
    {
        public abstract void Send();
    }
    public class Notifier : BaseNotifier
    {
        protected List<String> emails;
        public Notifier(List<string> emails)
        {
            this.emails = emails;
        }
        public override void Send()
        {
            Console.WriteLine("Notify by email:");
            foreach(string e in emails)
            {
                Console.WriteLine($"email sent to id: {e}");
            }
            Console.WriteLine("Done with email notifications");
        }
    }
    public abstract class NotificationDecorator : BaseNotifier
    {
        protected BaseNotifier notifier;
        public NotificationDecorator(BaseNotifier notifier)
        {
            this.notifier = notifier;
        }
        public override void Send()
        {
            this.notifier.Send();
        }
    }
    public class SMSDecorator : NotificationDecorator
    {
        protected List<long> phoneNumbers;
        public SMSDecorator(BaseNotifier notifier, List<long> phoneNumbers) : base(notifier)
        {
            this.phoneNumbers = phoneNumbers;
        }
        public override void Send()
        {
            base.Send();
            Console.WriteLine();
            SendSMS();
        }
        private void SendSMS()
        {
            Console.WriteLine("Notify by SMS");
            foreach(long number in phoneNumbers)
            {
                Console.WriteLine($"SMS sent to phone {number}");
            }
            Console.WriteLine("Done with SMS notification.");

        }
    }
    public class SlackDecorator : NotificationDecorator
    {
        protected List<string> slackHandlerUserNames;
        public SlackDecorator(BaseNotifier notifier, List<string> usernames) : base(notifier)
        {
            this.slackHandlerUserNames = usernames;
        }
        public override void Send()
        {
            base.Send();
            Console.WriteLine();
            SendSlackPings();
        }
        private void SendSlackPings()
        {
            Console.WriteLine("Notiby by Slack");
            foreach(string uname in slackHandlerUserNames)
            {
                Console.WriteLine($"user ({uname}) pinged on slack");
            }
            Console.WriteLine("Done with slack notification");
        }
    }
    public class NotifierClient
    {
        protected BaseNotifier notifier;
        public NotifierClient(BaseNotifier notifier)
        {
            this.notifier = notifier;
        }
        public void SetNotifier(Notifier notifier)
        {
            this.notifier = notifier;
        }
        public void ClientCode()
        {
            Console.WriteLine("time to notify.");
            notifier.Send();
        }
    }
}