using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace BikeRace
{
    public partial class BikeGame : Form
    {
        //Declaring all variables

        public Player p1;
        public Player p2;
        public Player p3;
        public ArrayList allPlayers;
        Player currentBettor;

        public BikeGame()
        {
            //Creating objects of the Player class

            p1 = new Player("Roe", 0, 0);
            p2 = new Player("Sam", 0, 0);
            p3 = new Player("Sahil", 0, 0);

            //Adding all players to an empty ArrayList

            allPlayers = new ArrayList();
            allPlayers.Add(p1);
            allPlayers.Add(p2);
            allPlayers.Add(p3);

            InitializeComponent();

            //Update Balance
            player1Balance.Text = p1.totalAmount.ToString();
            player2Balance.Text = p2.totalAmount.ToString();
            player3Balance.Text = p3.totalAmount.ToString();

            //Go button disabled until player puts a bet
            btnGo.Enabled = false;

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            //Moving all the bikes
            int timeTaken_2 = moveBike(pictureBox2);
            int timeTaken_3 = moveBike(pictureBox3);
            int timeTaken_4 = moveBike(pictureBox4);
            int timeTaken_5 = moveBike(pictureBox5);

            int[] timeArray = { timeTaken_2, timeTaken_3, timeTaken_4, timeTaken_5 };
            int smallest = timeTaken_2;

            int i = 0;
            while (i != timeArray.Length)
            {
                if (timeArray[i] < smallest)
                {
                    smallest = timeArray[i];
                }

                i++;
            }


            //Pushing a message box when the race finishes
            switch (Array.IndexOf(timeArray, smallest))
            {
                case 0:
                    MessageBox.Show("The Winner is Bike #1", "Race Finished!");
                    if (currentBettor.getBike() == 1)
                        currentBettor.declareWinner();
                    break;
                case 1:
                    MessageBox.Show("The Winner is Bike #2", "Race Finished!");
                    if (currentBettor.getBike() == 2)
                        currentBettor.declareWinner();
                    break;
                case 2:
                    MessageBox.Show("The Winner is Bike #3", "Race Finished!");
                    if (currentBettor.getBike() == 3)
                        currentBettor.declareWinner();
                    break;
                case 3:
                    MessageBox.Show("The Winner is Bike #4", "Race Finished!");
                    if (currentBettor.getBike() == 4)
                        currentBettor.declareWinner();
                    break;
            }

            //Disbale options to select the current bettor if he has no balance left
            if (currentBettor.totalAmount == 0)
            {
                switch (currentBettor.name)
                {
                    case "Roe":
                        radioButton1.Checked = false;
                        radioButton1.Enabled = false;
                        break;

                    case "Sam":
                        radioButton2.Checked = false;
                        radioButton2.Enabled = false;
                        break;

                    case "Sahil":
                        radioButton3.Checked = false;
                        radioButton3.Enabled = false;
                        break;
                }
            }

            //If the player has 0 balance, he is knocked out, thus remove from the ArrayList
            if (currentBettor.totalAmount == 0)
            {
                allPlayers.Remove(currentBettor);
            }

            //Enable Go Button
            btnGo.Enabled = false;

            //Reposition all the bikes to their initial point
            restorePosition(pictureBox2);
            restorePosition(pictureBox3);
            restorePosition(pictureBox4);
            restorePosition(pictureBox5);

            //Update balance of each player
            player1Balance.Text = p1.totalAmount.ToString();
            player2Balance.Text = p2.totalAmount.ToString();
            player3Balance.Text = p3.totalAmount.ToString();

            //If only one player is left in the ArrayList, he is the winner
            if (allPlayers.Count == 1)
            {
                Player player = (Player)allPlayers[0];
                MessageBox.Show(player.name.ToString() + " wins the game! Click OK to restart.", "Congratulations!");

                //Restart Game function from abstract class
                BikeRaceFunctions bikeRaceFunctions = new BikeRaceFunctions();
                bikeRaceFunctions.RestartGame();

            }


        }

        public int moveBike(PictureBox pictureBox)
        {
            int a = pictureBox1.Size.Width - 100;
            int totalDisplacement = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (totalDisplacement < a)
            {
                /*
                 * The bike moves randomly
                 */

                Random random = new Random();
                int displacement = random.Next(0, 15);

                pictureBox.Location = new Point(totalDisplacement / 2, pictureBox.Location.Y);
                pictureBox.Location = new Point(totalDisplacement - displacement, pictureBox.Location.Y);
                totalDisplacement = totalDisplacement + displacement;

                if (totalDisplacement >= a)
                {
                    stopwatch.Stop();
                    break;
                }
            }
            return (int)stopwatch.ElapsedMilliseconds;
        }

        private void restorePosition(PictureBox pictureBox)
        {
            pictureBox.Location = new Point(13, pictureBox.Location.Y);
        }

        private void btnPlace_Click(object sender, EventArgs e)
        {

            //Get user input
            int amt = Int32.Parse(bettingAmountField.Text);
            int mBike = Int32.Parse(numericUpDown1.Text);

            /*
             * Following conditionals check which player is selected
             * what is the bet amount, etc.
             * If the bet amount exeeds the player balance, the bet is not placed
             */

            if (radioButton1.Checked)
            {
                p1.setAmount(amt);
                p1.setBike(mBike);

                if (p1.totalAmount - amt >= 0)
                {
                    p1.deductBetAmount();
                    currentBettor = p1;

                    statusRoe.Text = p1.name + " placed a bet of $" + amt.ToString() + " on bike #" + p1.getBike();
                }
                else
                {
                    MessageBox.Show("Can't place this bet, set an amount lower than total balance", "Bet Not Placed");
                }
            }

            if (radioButton2.Checked)
            {
                p2.setAmount(amt);
                p2.setBike(mBike);

                if (p2.totalAmount - amt >= 0)
                {
                    p2.deductBetAmount();
                    statusSam.Text = p2.name + " placed a bet of $" + amt.ToString() + " on bike #" + p2.getBike();
                }
                else
                {
                    MessageBox.Show("Can't place this bet, set an amount lower than total balance", "Bet Not Placed");
                }

                currentBettor = p2;
            }

            if (radioButton3.Checked)
            {
                p3.setAmount(amt);
                p3.setBike(mBike);

                if (p3.totalAmount - amt >= 0)
                {
                    p3.deductBetAmount();
                    statusSahil.Text = p3.name + " placed a bet of $" + amt.ToString() + " on bike #" + p3.getBike();
                }
                else
                {
                    MessageBox.Show("Can't place this bet, set an amount lower than total balance", "Bet Not Placed");
                }

                currentBettor = p3;
            }

            //Go button is enabled when the bet is finalised
            btnGo.Enabled = true;

            if (allPlayers.Count == 1)
            {
                Player player = (Player)allPlayers[0];
                MessageBox.Show("Player " + player.name.ToString() + " wins the game!", "Congratulations!");
            }
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            //Exit game function from abstract class
            BikeRaceFunctions bikeRaceFunctions = new BikeRaceFunctions();
            bikeRaceFunctions.ExitGame();
        }
    }
}
