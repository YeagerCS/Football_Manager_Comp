using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Manager
{
    public class Calc
    {
        //Exponential Price Growth depending on Rating
        public int getPrice(double rating)
        {
            double basePrice = 60000;
            double exp = 6.5;

            //f(x) = 60'000 * (x/99)^6.5
            double temp = Math.Pow(rating / 99, exp);
            double calculatedPrice = basePrice * temp;
            int finalPrice = (int)Math.Round(calculatedPrice);
            return finalPrice;
        }

        public int GeneratePriceRemoval(double rating)
        { 
            rating = Math.Max(58, Math.Min(99, rating)); // Making sure ratings can't be lower or higher than 58 and 99

            double ratingRange = 99 - 58;
            double valueRange = 13000 - 1100;

            //f(x) = 1100 + ((rating - 58) / (99 - 58)) * (13000 - 1100)
            double normalizedRating = (rating - 58) / ratingRange; // Takes a number between 0 and 1 as multiplier for total price depending on the rating
            int generatedValue = (int)(1100 + normalizedRating * valueRange); // Takes the range of prices and multiplies with the normalized rating
                                                                              // In the end it adds the minimum price which is 1100

            return generatedValue;
        }

        public int getRating(int price)
        {
            double baseRating = 99;
            double exp = 1 / 6.5;

            //f^-1(x) = 99 * (x/60'000)^1/6.5
            double calculatedRating = baseRating * Math.Pow(price / 60000, exp);
            int finalRating = (int)Math.Round(calculatedRating);
            return finalRating;
        }
    }
}
