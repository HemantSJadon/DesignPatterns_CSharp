using System;

namespace DesignPatterns_CSharp.structural
{
    public class RoundHole
    {
        public int Radius {get; private set;}
        public RoundHole(int radius)
        {
            this.Radius = radius;
        }
        public bool Fits(RoundPeg peg)
        {
            return this.Radius >= peg.Radius;
        }
    }

    public class RoundPeg
    {
        public int Radius {get; private set;}
        public RoundPeg(int radius)
        {
            this.Radius = radius;
        }
    }
    public class Client_adapter_holepeg
    {
        public void ClientCode()
        {
            RoundHole r_hole = new RoundHole(6);
            RoundPeg r_peg = new RoundPeg(5);
            Console.WriteLine($"Does the round peg fits the hole: {r_hole.Fits(r_peg)}");
        }
    }
}