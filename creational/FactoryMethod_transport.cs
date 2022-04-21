using System;
using System.Text;

namespace DesignPatterns_CSharp.creational
{
    //This class has been made static since this does not need to be aware of instance variables
    public static class Logistics
    {
        public static string PlanDelivery(TransportCreator transportCreator)
        {
            Console.WriteLine("Logistics : the plan for delivery is being generated\n");
            ITransport transport = transportCreator.CreateTransport();
            return transport.Deliver();
        }
        
    }

    public static class LogisticsCalculator
    {
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
        public void ClientCode(int transportDistance)
        {
            Console.WriteLine($"Client: Getting the delivery plan..\n");
            //The responsibility to get the best Transport creator is on the client code
            TransportCreator bestTransportCreator = LogisticsCalculator.GetBestTransportCreator(transportDistance);
            //the creator class is injected in the method of logistics class rather than the constructor : Can the logistics class be made static
            Console.WriteLine(Logistics.PlanDelivery(bestTransportCreator));
        }
    }
}