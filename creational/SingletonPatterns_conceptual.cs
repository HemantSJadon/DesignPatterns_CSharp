using System;

namespace DesignPatterns_CSharp.creational
{
    public sealed class SingletonConcept
    {
        private int dummyValue;
        private SingletonConcept()
        {
        }
        public static SingletonConcept GetInstance()
        {
            var instance = Nested.instance;
            instance.dummyValue = 1500;
            return instance;
        }
        public void SomeInstanceOperation()
        {
            Console.WriteLine("Some instance operation performed on this singleton class.");
        }
        private class Nested
        {
            static Nested()
            {

            }
            internal static readonly SingletonConcept instance = new SingletonConcept();
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