using System;

namespace DesignPatterns_CSharp.structural
{
    public class RoundHole
    {
        public double Radius {get; private set;}
        public RoundHole(double radius)
        {
            this.Radius = radius;
        }
        public bool Fits(IRoundPeg peg)
        {
            return this.Radius >= peg.Radius;
        }
    }
    public interface IRoundPeg
    {
        double Radius { get; }
    }

    public class RoundPeg : IRoundPeg
    {
        public RoundPeg(double radius)
        {
            this.Radius = radius;
        }
        public double Radius { get;}
    }
    public class SquarePeg
    {
        public double Width { get; private set; }
        public SquarePeg(double width)
        {
            this.Width = width;
        }
    }
    //How do I check whether the square peg fits the hole or not, need to use an adapter square > round
    public class SquarePegAdapter : IRoundPeg
    {
        private SquarePeg peg;
        public SquarePegAdapter(SquarePeg peg)
        {
            this.peg = peg;
        }

        public double Radius {
            get
            {
                return this.peg.Width* Math.Sqrt(2.0)/2.0;
            }
        }
    }
    public class Client_adapter_holepeg
    {
        public void ClientCode()
        {
            RoundHole r_hole = new RoundHole(6);
            RoundPeg r_peg = new RoundPeg(5);
            Console.WriteLine($"Does the round peg fits the hole: {r_hole.Fits(r_peg)}");
            SquarePeg s_peg = new SquarePeg(10);
            //to check whether this square peg would fit in the round hole, we will need
            // the adapter
            SquarePegAdapter s_peg_adapter = new SquarePegAdapter(s_peg);
            Console.WriteLine($"Would the square peg fits the round hole through adapter:{r_hole.Fits(s_peg_adapter)}");


        }
    }
}