using System;

namespace DesignPatterns_CSharp.structural
{
    public abstract class Component
    {
        public abstract string Operation();
    }
    public class ConcreteComponent : Component
    {
        public override string Operation()
        {
            return "ConcreteComponent";
        }
    }
    public abstract class Decorator : Component
    {
        protected  Component component;
        public Decorator(Component component)
        {
            this.component = component;
        }
        public void SetComponent(Component component)
        {
            this.component = component;
        }
        public override string Operation()
        {
            return this.component != null ? this.component.Operation() :
             String.Empty;
        }
    }
    public class ConcreteDecoratorA : Decorator
    {
        public ConcreteDecoratorA(Component component) : base(component)
        {
        }
        public override string Operation()
        {
            return $"ConcreteDecoratorA({base.Operation()})";
        }
    }
    public class ConcreteDecoratorB : Decorator
    {
        public ConcreteDecoratorB(Component component) : base(component)
        {
        }
        public override string Operation()
        {
            return $"ConcreteDecoratorB({base.Operation()})";
        }
    }
    public class Client
    {
        public void ClientCode(Component component)
        {
            Console.WriteLine("Result:" + component.Operation());
        }
    }
}