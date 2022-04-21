using System;
using System.Text;

namespace DesignPatterns_CSharp.creational
{
    public class Logistics
    {
        protected TransportCreator transportCreator;
        public Logistics(TransportCreator creator)
        {
            this.transportCreator = creator;
        }
        public string PlanDelivery(int distance)
        {
            Console.WriteLine("Logistics : the plan for delivery is being generated\n");
            // TransportMethod bestTransport = LogisticsCalculator.GetBestTransport(distance);
            ITransport transport = this.transportCreator.CreateTransport(distance);
            // ITransport transport = this.transportCreator.CreateTransport(bestTransport);
            return transport.Deliver();
        }
        
    }

    public static class LogisticsCalculator
    {
        public static TransportMethod GetBestTransport(int distance)
        {
            Console.WriteLine("Calculating best transport option... \n");
            if(distance <= 5000)
                return TransportMethod.Road;
            else if(distance <= 10000)
                return TransportMethod.Air;
            else
                return TransportMethod.Water;
            
        }

    }
    public  class TransportCreator
    {
        public ITransport CreateTransport(int distance)
        {
            ITransport transport = null;
            TransportMethod bestTransport = LogisticsCalculator.GetBestTransport(distance);
            if(bestTransport == TransportMethod.Road)
                transport = GetATruck();
            else if(bestTransport == TransportMethod.Air)
                transport = GetAPlane();
            else
                transport = GetAShip();
            return transport;
        }
        public ITransport CreateTransport(TransportMethod bestTransportMethod)
        {
            ITransport transport = null;
            if(bestTransportMethod == TransportMethod.Road)
                transport = GetATruck();
            else if(bestTransportMethod == TransportMethod.Air)
                transport = GetAPlane();
            else
                transport = GetAShip();
            return transport;
        }
        private static Truck GetATruck()
        {
            return new Truck();
        }
        private static Ship GetAShip()
        {
            return new Ship();
        }
        public static Airplane GetAPlane()
        {
            return new Airplane();
        }
    }    
    public enum TransportMethod
    {
        Road,
        Water,
        Air
    }
    public interface ITransport
    {
        string Deliver();
    }
    public class Ship : ITransport
    {
        public string Deliver()
        {
            return "Transport: To be delivery by a ship through sea in a container.";
        }
    }

    public class Airplane : ITransport
    {
        public string Deliver()
        {
            return "Transport: To be Delivered on a plane in air in a box.";
        }
    }
    public class Truck : ITransport
    {
        public string Deliver()
        {
            return "Transport: To be Delivered by a truck on road in a box.";
        }
    }
    public class Client_FactoryMtd_transport
    {
        public void ClientCode(Logistics logistics)
        {
            Console.WriteLine($"Client: Getting the delivery plan..\n");
            Console.WriteLine(logistics.PlanDelivery(8900));
        }
    }
}