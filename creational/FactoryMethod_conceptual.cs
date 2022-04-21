using System;

namespace DesignPatterns_CSharp.creational
{
    public abstract class Creator
    {
        public abstract IProduct FactoryMethod();
        public string SomeOperation()
        {
            IProduct product = FactoryMethod();
            string result = "Creator: The same creator's code has worked with " + product.Operation();
            return result;
        }
    }
    public class ConcreteCreator1 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct1();
        }
    }
    public class ConcreteCreator2 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct2();
        }
    }

    public class ConcreteProduct1 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct1}";
        }
    }
    public class ConcreteProduct2 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct2}";
        }
    }
    public interface IProduct
    {
        string Operation();
    }
    public class Client_factoryMtd_Conc
    {
        public void ClientCode(Creator creator)
        {
            Console.WriteLine( $"Client: I am not sure about the creator's class but this still works\n {creator.SomeOperation()}");
        }
    }
}