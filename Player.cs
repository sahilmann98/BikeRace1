using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRace
{
    /*
     * The Player class
     */
    public class Player
    {
        public String name;
        private int bettingBike;
        private int bettingAmount;
        public int totalAmount = 50;
        public Player(String name, int bettingBike, int bettingAmount)
        {
            this.name = name;
            this.bettingBike = bettingBike;
            this.bettingAmount = bettingAmount;
        }

        /*
         * Following are settor and gettor functions
         */
        public void setBike(int b)
        {
            this.bettingBike = b;
        }

        public int getBike()
        {
            return bettingBike;
        }

        public void setAmount(int amt)
        {
            this.bettingAmount = amt;
        }

        /*
         * deductBetAmount() deducts the bet amount from total balance
         */
        public void deductBetAmount()
        {
            totalAmount = totalAmount - bettingAmount;
        }

        /*
         * declareWinner() awards the player with the bet amount
         */
        public void declareWinner()
        {
            totalAmount = totalAmount+bettingAmount*2;
        }
    }
}
