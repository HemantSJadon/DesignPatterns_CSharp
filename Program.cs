using System;
using System.Collections.Generic;
using DesignPatterns_CSharp.structural;

namespace DesignPatterns_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Decorator pattern example conceptual
            // TestDecoratorPattern_conceptual();

            //2. Decorator pattern example notifier
            TestDecoratorPattern_notifier();

        }
        static void TestDecoratorPattern_conceptual()
        {
            
            Client client = new Client();

            var simple = new ConcreteComponent();
            Console.WriteLine("Client: I get a simple component");
            client.ClientCode(simple);
            Console.WriteLine();

            //as well as decorated ones

            ConcreteDecoratorA decorator1 = new ConcreteDecoratorA(simple);
            ConcreteDecoratorB decorator2 = new ConcreteDecoratorB(decorator1);
            Console.WriteLine("Client: Now I've got a decorated component.");
            client.ClientCode(decorator2);
            Console.WriteLine();
        }
        static void TestDecoratorPattern_notifier()
        {
            List<string> emails = new List<string>()
            {
                "a@gmail.com",
                "b@gmail.com",
                "c@gmail.com",
                "d@gmail.com",
                "e@gmail.com",
            };
            Notifier notifier = new Notifier(emails);
            List<long> phoneNumbers = new List<long>()
            {
                9999999911,
                9999999900,
                9898989900,
                9899119090
            };
            SMSDecorator smsdecorator = new SMSDecorator(notifier, phoneNumbers);
            List<string> slackHandlerUserNames = new List<string>()
            {
                "user12@",
                "krateKing@28",
                "jollyauly"
            };
            SlackDecorator slackDecorator = new SlackDecorator(smsdecorator, slackHandlerUserNames);
            NotifierClient nclient = new NotifierClient(slackDecorator);
            Console.WriteLine("Doing some work.");
            nclient.ClientCode();
        }
    }

}
