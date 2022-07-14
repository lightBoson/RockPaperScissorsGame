using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestProject")]

namespace RockPaperScissors
{
    public enum GamerType
    {
        Human,
        Computer
    }

    public interface IGamer
    {
        void introduce();
        public virtual string type() { return "IGamer"; }
    }

    internal class Gamer : IGamer
    {
        //Atributes 
        private string _name;
        private GamerType _type;
        private int _bestResult;


        //Methods
        public string getName() { return _name;}
        public int getBestResult() { return _bestResult;}
        public void setName(string newName) { _name = newName; }
        public GamerType getType() { return _type; }

        public void introduce() { Console.WriteLine("Gamer class"); }

        public string type() { return "Gamer"; }

        public Answer randomChoice() {
            Random random = new Random();
            return (Answer)random.Next(3); } // bierzemy 3 pierwsze bo nie mozemy zwrocic Unknown
        public Gamer(string name, GamerType type = GamerType.Computer)
        {
            _name = name;
            _type = type;

        }
    }
}
