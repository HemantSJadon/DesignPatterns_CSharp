using System;
using System.Collections.Generic;
using DesignPatterns_CSharp.structural;
using creationalPatterns =  DesignPatterns_CSharp.creational;

namespace DesignPatterns_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Decorator pattern example conceptual
            // TestDecoratorPattern_conceptual();

            //2. Decorator pattern example notifier
            // TestDecoratorPattern_notifier();

            //3. Factory Method conceptual example
            // TestFactoryMethod_conceptual();

            //4. Factory Method transport example
            TestFactoryMethod_transport();

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
        static void TestFactoryMethod_conceptual()
        {
            creationalPatterns.Client_factoryMtd_Conc client = new creationalPatterns.Client_factoryMtd_Conc();
            creationalPatterns.ConcreteCreator1 creator1 = new creationalPatterns.ConcreteCreator1();
            Console.WriteLine("App launched with Concrete creator 1");
            client.ClientCode(creator1);
            Console.WriteLine("");
            creationalPatterns.ConcreteCreator2 creator2 = new creationalPatterns.ConcreteCreator2();
            Console.WriteLine("App launched with concrete creator 2");
            client.ClientCode(creator2);

        }
        static void TestFactoryMethod_transport()
        {
            creationalPatterns.Client_FactoryMtd_transport client = new creationalPatterns.Client_FactoryMtd_transport();
            int transportDistance = 4400;
            client.ClientCode(transportDistance);
        }
    }

}
