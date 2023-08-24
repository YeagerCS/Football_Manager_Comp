using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Manager
{
    public class Team
    {
        private String name;
        public double rating;
        private double multiplier;
        private int wins;
        private int loses;
        private int draws;
        private int chemistry;


        //Chemistry is irrelevant at the moment
        public int getChemistry()
        {
            return this.chemistry;
        }

        public void setChemistry(int chemistry)
        {
            this.chemistry = chemistry;
        }

        public Team(String name, double rating, double multiplier)
        {
            this.name = name;
            this.rating = rating;
            this.multiplier = multiplier;
        }

        public Team()
        {
            this.wins = 0;
            this.draws = 0;
            this.loses = 0;
            this.name = "";
            this.rating = 0;
            this.multiplier = 1;
            this.chemistry = 0;
        }


        public void addWins()
        {
            Wins++;
        }


        public void addLoses()
        {
            Loses++;
        }


        public void addDraws()
        {
            Draws++;
        }

        public int Wins
        {
            get { return wins; }
            set { wins = value; }
        }

        public int Draws
        {
            get { return draws; }
            set { draws = value; }

        }

        public int Loses
        {
            get { return loses; }
            set { loses = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        //irrelevant
        public double Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        public int Chemistry
        {
            get { return chemistry; }
            set { chemistry = value; }
        }

    }

}
