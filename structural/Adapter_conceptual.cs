using System;

namespace DesignPatterns_CSharp.structural
{
    public class ConcreteTarget
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
        public void GetSpecificRequest()
        {
            Console.WriteLine("specific request");
        }
    }
    public class Client_adapter_conc
    {
        public void ClientCode()
        {
            ConcreteTarget concreteTargetObj = new ConcreteTarget();
            concreteTargetObj.GetRequest();
        }
    }
}