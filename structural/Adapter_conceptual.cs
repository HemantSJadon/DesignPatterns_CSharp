using System;

namespace DesignPatterns_CSharp.structural
{
    public interface ITarget
    {
        public void GetRequest();
    }
    public class ConcreteTarget : ITarget
    {
        public void GetRequest()
        {
            Console.WriteLine("concrete target request");
        }
    }
    //this may be an existing class of a 3rd party lib or a class with high dependency which 
    //is not practical to change this
    public class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "specific request";
        }
    }
    //This adapter wraps the adaptee object and provides it with a new interface 
    //so existing client classes can use this object even though the original inteface is incompatible
    public class Adapter : ITarget
    {
        private Adaptee adaptee;
        public Adapter(Adaptee adaptee)
        {
            this.adaptee = adaptee;
        }
        public void GetRequest()
        {
            Console.WriteLine($"This is {this.adaptee.GetSpecificRequest()}");
        }
    }
    public class Client_adapter_conc
    {
        public void ClientCode()
        {
            //The client is calling the specific target obj
            //
            ITarget concreteTargetObj = new ConcreteTarget();
            concreteTargetObj.GetRequest();

            Adaptee adaptee = new Adaptee();
            ITarget adapter = new Adapter(adaptee);
            adapter.GetRequest();
        }
    }
}