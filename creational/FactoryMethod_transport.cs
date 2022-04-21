using System;
using System.Text;

namespace DesignPatterns_CSharp.creational
{
    public class Logistics
    {
        public string PlanDelivery(int distance)
        {
            Console.WriteLine("Logistics : the plan for delivery is being generated\n");
            //using the factory-method pattern here without dependency injection as the calculation to get the best transport creator is being handled inside the logistics class
            TransportCreator bestTransportCreator = LogisticsCalculator.GetBestTransportCreator(distance);
            ITransport transport = bestTransportCreator.CreateTransport();
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
        public static TransportCreator GetBestTransportCreator(int distance)
        {
            Console.WriteLine("fetching the best transport option... \n");
            if(distance <= 5000)
                return new TruckCreator();
            else if(distance <= 10000)
                return new AirPlaneCreator();
            else
                return new ShipCreator();
        }

    }
    public abstract class TransportCreator
    {
        public abstract ITransport CreateTransport();
    }
    public class TruckCreator : TransportCreator
    {
        public override ITransport CreateTransport()
        {
            return new Truck();
        }
    }
    public class ShipCreator : TransportCreator
    {
        public override ITransport CreateTransport()
        {
            return new Ship();
        }
    }
    public class AirPlaneCreator : TransportCreator
    {
        public override ITransport CreateTransport()
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
        public void ClientCode(Logistics logistics, int transportDistance)
        {
            Console.WriteLine($"Client: Getting the delivery plan..\n");
            Console.WriteLine(logistics.PlanDelivery(transportDistance));
        }
    }
}