using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DesignPatterns_CSharp.structural;
using creationalPatterns =  DesignPatterns_CSharp.creational;
using behaviouralPatterns = DesignPatterns_CSharp.behavioural;

namespace DesignPatterns_CSharp
{
    class Program
    {
        private static readonly Object padlock = new Object();
        static void Main(string[] args)
        {
            //1. Decorator pattern example conceptual
            // TestDecoratorPattern_conceptual();

            //2. Decorator pattern example notifier
            // TestDecoratorPattern_notifier();

            //3. Factory Method conceptual example
            // TestFactoryMethod_conceptual();

            //4. Factory Method transport example
            // TestFactoryMethod_transport();

            //5. Singleton pattern conceptual example
            // TestSingletonPattern_conceptualNaive();
            // TestSingletonPattern_naiveMultithreading();

            
            // StringBuilder str = new StringBuilder();
            // Console.WriteLine(BuilderSuffixString(10,'a',str));
            
            //6. Command Pattern conceptual example
            // TestCommandPattern_conceptual();

            //7. Command Pattern editor example
            //TestCommandPattern_editor();
            
            //8. Adapter pattern conceptual example
            //TestAdapterPattern_conceptual();

            //9. Adapter pattern hole peg example
            TestAdapterPattern_holePeg();
            
            


        }
        static void FunCode()
        {
            string toPrint = "Hey Kancha, How you doing?";
            string theQuestion = "Wanna fuck right here?";
            String deleteLast = "\b \b";
            
            foreach(char c in toPrint)
            {
                Thread.Sleep(300);
                Console.Write(c);
                if(c == '?')
                {
                    for(int i = 0; i < 15; i++)
                    {
                        Thread.Sleep(300);
                        Console.Write(deleteLast);
                    }

                }
            }
            foreach(char c in theQuestion)
            {
                Thread.Sleep(300);
                Console.Write(c);
            }
        }

        private static string BuilderSuffixString(int runningCount, char character, StringBuilder stringToAppend)
        {
            if(runningCount > 10){
                stringToAppend.Append($"9{character}");
                runningCount -= 9;
                return BuilderSuffixString(runningCount, character,stringToAppend);
            }
            else
            {
                stringToAppend.Append(runningCount > 1 ? $"{runningCount}{character}" : $"{character}");
                return stringToAppend.ToString();
            }
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
        static void TestSingletonPattern_conceptualNaive()
        {
            creationalPatterns.Client_Singleton_conc clientOld  = new creationalPatterns.Client_Singleton_conc();
            int numClient= 3;
            var prevInstance  = clientOld.ClientCode();
            for(int i = 0; i < numClient; i++)
            {
                creationalPatterns.Client_Singleton_conc client = new creationalPatterns.Client_Singleton_conc();
                bool isSameInstance = prevInstance == client.ClientCode();
                Console.WriteLine($"Is same instance: {isSameInstance}");
            }
        }
        static void TestSingletonPattern_naiveMultithreading()
        {
            creationalPatterns.Client_Singleton_conc client = new creationalPatterns.Client_Singleton_conc();
            creationalPatterns.SingletonConcept prevInstance = null;
            List<Thread> threads = new List<Thread>();
            for(int i = 0; i < 15; i++)
            {
                Thread thread = new Thread(() => {
                    var currInstance = client.ClientCode();
                    lock (padlock)
                    {
                        if(prevInstance != null){
                        bool isSameInstance = prevInstance == currInstance;
                        Console.WriteLine($"Is Same Instance: {isSameInstance}");
                        }
                        else
                            Console.WriteLine("First Instance.");
                        prevInstance = currInstance;
                    }
                });
                threads.Add(thread);
            }
            foreach(var thread in threads)
                thread.Start();
            foreach(var thread in threads)
                thread.Join();

        }
        static void TestCommandPattern_conceptual()
        {
            behaviouralPatterns.Client_command_conc client = new behaviouralPatterns.Client_command_conc();
            client.ClientCode();
        }
        static void TestCommandPattern_editor()
        {
            behaviouralPatterns.Client_command_editor  client = new behaviouralPatterns.Client_command_editor();
            client.ClientCode();
        }
        static void TestAdapterPattern_conceptual()
        {
            var client = new Client_adapter_conc();
            client.ClientCode();
        }
        static void TestAdapterPattern_holePeg()
        {
            var client = new Client_adapter_holepeg();
            client.ClientCode();
        }
    }

}
