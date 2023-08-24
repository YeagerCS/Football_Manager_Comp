using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace Football_Manager
{
    public class Player
    {
        private string name;
        private int rating;
        private int shirtNumber;
        private String position;
        private int price;
        private int goals;
        private bool bought;
        private bool multiupgrade;
        public string DisplayString { get; set; }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        public bool Multiupgrade
        {
            get { return multiupgrade; }
            set { multiupgrade = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        public int ShirtNumber
        {
            get { return shirtNumber; }
            set { shirtNumber = value; }
        }

        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool Bought
        {
            get { return bought; }
            set { bought = value; }
        }

        public int Goals
        {
            get { return goals; }
            set { goals = value; }
        }

        public void addGoals(int goals)
        {
            Goals += goals;
        }

        public Player()
        {
            multiupgrade = false;
        }

        public Player(string name, int rating, int shirtNumber, string position, int price)
        {
            Name = name;
            Rating = rating;
            ShirtNumber = shirtNumber;
            Position = position;
            Price = price;
            if (Multiupgrade == true)
            {
                DisplayString = shirtNumber + " " + name + " " + rating + " " + position + " Ω";
            }
            else
            {
                DisplayString = shirtNumber + " " + name + " " + rating + " " + position;
            }
        }

        public Player(string name, int rating, int shirtNumber, string position, int price, bool multiupgrade)
        {
            Name = name;
            Rating = rating;
            ShirtNumber = shirtNumber;
            Position = position;
            Price = price;
            Multiupgrade = multiupgrade;
            if (Multiupgrade)
            {
                DisplayString = shirtNumber + " " + name + " " + rating + " " + position + " Ω";
            }
            else
            {
                DisplayString = shirtNumber + " " + name + " " + rating + " " + position;
            }
        }
        
        public void ReloadDisplay()
        {
            if (Multiupgrade)
            {
                DisplayString = shirtNumber + " " + name + " " + rating + " " + position + " Ω";
            }
            else
            {
                DisplayString = shirtNumber + " " + name + " " + rating + " " + position;
            }
        }
    }
}
