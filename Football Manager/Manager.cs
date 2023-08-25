using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Manager
{
    public class Manager
    {
        private Team teamAssigned;
        private String? firstName;
        private String? lastName;
        private double rating;
        private double money;

        public Manager()
        {
        }

        public Manager(String firstName, String lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public Team TeamAssigned
        {
            get { return teamAssigned; }
            set { teamAssigned = value; }
        }

        public String? FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public String? LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        // Is used but currently does nothing
        public double Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        public double Money
        {
            get { return money; }
            set { money = value; }
        }

        public void AddMoney(double money)
        {
            Money += money;
        }

    }

}
