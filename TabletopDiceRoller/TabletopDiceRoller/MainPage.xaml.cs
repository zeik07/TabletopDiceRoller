﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TabletopDiceRoller
{
    public partial class MainPage : TabbedPage
    {
        private Random rnd = new Random(Environment.TickCount);

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnButtonClick(Button button, EventArgs e)
        {
            string[] split = button.Text.Split('d');
            int value = Convert.ToInt32(split[1]);
            string die = button.Text;
            string roll = Roll(value).ToString();
            DisplayRoll(die, roll, roll);
        }        

        private int Roll(int value)
        {            
            int rolled = rnd.Next(1, (value + 1));
            return rolled;
        }

        private async void DisplayRoll(string die, string roll, string values)
        {
            var popUpPage = new DiceRoll(die, roll, values);
            await Navigation.PushModalAsync(popUpPage, false);      
        }

        private void OnCustomClick(object sender, EventArgs e)
        {
            string input = CustomRollInput.Text;
            string[] roll = input.Split('+', '-');
            string outputRoll = "";
            foreach (string r in roll)
            {
                if (r.Contains('d'))
                {
                    string[] diceRoll = new string[2];
                    
                    try
                    {                        
                        diceRoll = r.Split('d');
                        if (diceRoll.Length != 2)
                        {
                            throw new Exception();
                        }
                        string[] diceResult = new string[Convert.ToInt32(diceRoll[0])];
                        int diceValue = Convert.ToInt32(diceRoll[1]);
                        for (int i = 0; i < Convert.ToInt32(diceRoll[0]) ; i++)
                        {                            
                            diceResult[i] = Roll(diceValue).ToString();
                        }
                        string rollValue = "";
                        foreach (string result in diceResult)
                        {
                            if (rollValue == "")
                            {
                                rollValue += result;
                            }
                            else
                            {
                                rollValue += "+" + result;
                            }
                        }
                        outputRoll += rollValue + " ";
                    }
                    catch (Exception ex)
                    {
                        outputRoll = "Improper format. e.g. 8d6+1d4+1";
                        break;
                    }                    
                }
                else
                {
                    outputRoll += r + " ";
                }                
            }
            CustomOutput.Text = outputRoll;
        }

        private void OnCustomInput(Button button, EventArgs e)
        {
            if (button.Text == "Del")
            {
                CustomRollInput.Text = "";
            }
            else
            {
                CustomRollInput.Text += button.Text;
            }            
        }
    }
}
