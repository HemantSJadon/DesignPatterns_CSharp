using System;

namespace DesignPatterns_CSharp.creational
{
    public sealed class SingletonConcept
    {
        private static SingletonConcept _instance;
        private static readonly object _lock = new Object();
        public  int _dummyValue {get; private set;}
        private SingletonConcept()
        {
        }
        public static SingletonConcept GetInstance()
        {
            if(_instance is null)
            {
                lock (_lock)
                {
                    if(_instance is null)
                    {
                        Console.WriteLine("SingletonConcept: No class instance exists yet.");
                        _instance = new SingletonConcept(); 
                        Console.WriteLine("SingletonConcept: creating and saving a new instance");
                    }
                    else
                        Console.WriteLine("SingletonConcept: Returning the already created instance... ");
                }
            }
            else
                Console.WriteLine("SingletonConcept: Returning the already created instance... ");
            return _instance;
        }
        public void SomeInstanceOperation()
        {
            Console.WriteLine("Some instance operation performed on this singleton class.");
        }

    }
    public class Client_Singleton_conc
    {
        public SingletonConcept ClientCode()
        {
            //Create a new object of SingletonConcept class
            return SingletonConcept.GetInstance();
        }
    }
}