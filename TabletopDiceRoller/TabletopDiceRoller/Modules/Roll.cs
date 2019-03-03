using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace TabletopDiceRoller.Modules
{
    public class Roll
    {
        private Random rnd = new Random(Environment.TickCount);
        DataTable dt = new DataTable();

        public int RollDie(int value)
        {
            int rolled = rnd.Next(1, (value + 1));
            return rolled;
        }

        public async void DisplayRoll(string die, string roll, string values)
        {
            var popUpPage = new DiceRollView(die, roll, values);
            await App.Current.MainPage.Navigation.PushPopupAsync(popUpPage, false);
        }

        public void CustomRoll(RollItem item, bool isCrit)
        {
            string regx = @"[\[A-Z\]]";
            string error = "Improper format. e.g. 8[d6]+[d4]+1";
            string outputRoll = "";
            string critInput = "";
            string input = item.RollDice;            
            string[] rolled = RollSplit(input);            
            foreach (string r in rolled)
            {
                if (r.Contains('d'))
                {
                    try
                    {
                        string[] diceRolled = Regex.Split(r, regx);
                        List<string> diceRoll = new List<string>();
                        foreach (string d in diceRolled)
                        {
                            if (d != "")
                            {
                                diceRoll.Add(d);
                            }
                        }

                        int rollCount = 1;
                        int dieToRoll = 0;
                        string[] die = new string[2];
                        if (diceRoll.Count == 1)
                        {
                            if (isCrit)
                            {
                                rollCount = 2;
                                if (critInput == "")
                                {
                                    critInput += String.Format(rollCount + diceRoll[0]);
                                }
                                else
                                {
                                    critInput += "+" + String.Format(rollCount + diceRoll[0]);
                                }
                            }
                            die = diceRoll[0].Split(new char[] {'d'}, StringSplitOptions.RemoveEmptyEntries);
                            dieToRoll = Convert.ToInt32(die[0]);
                        }
                        else if(diceRoll.Count == 2)
                        { 
                            die = diceRoll[1].Split(new char[] {'d'}, StringSplitOptions.RemoveEmptyEntries);
                            dieToRoll = Convert.ToInt32(die[0]);
                            rollCount = Convert.ToInt32(diceRoll[0]);
                            if (isCrit)
                            {
                                rollCount = rollCount * 2;
                                if (critInput == "")
                                {
                                    critInput += String.Format(rollCount + diceRoll[1]);
                                }
                                else
                                {
                                    critInput += "+" + String.Format(rollCount + diceRoll[1]);
                                }
                            }
                            //split at d
                            //set roll count to first number
                            //set die to second number
                        }
                        else
                        {
                            throw new Exception();
                        }

                        string[] diceResult = new string[rollCount];
                        string rollValue = "";

                        for (int i = 0; i < rollCount; i++)
                        {
                            diceResult[i] = RollDie(dieToRoll).ToString();
                        }
                        
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
                        outputRoll += rollValue;
                    }
                    catch
                    {
                        outputRoll = error;
                        break;
                    }
                }
                else
                {
                    if (isCrit)
                    {
                        critInput += r;
                    }
                    outputRoll += r;
                }
            }
            if (isCrit)
            {
                RollOutput(critInput, outputRoll, false);
            }
            else
            {
                RollOutput(input, outputRoll, item.CanSave);
            }                        
        }

        private void RollOutput(string input, string outputRoll, bool canSave)
        {
            try
            {
                int sum = (int)dt.Compute(outputRoll, "");
                if (sum < 0)
                {
                    sum = 0;
                }
                if (canSave)
                {
                    double save = sum / 2;
                    int saveSum = (int)Math.Floor(save);
                    DisplayRoll(input, (sum.ToString() + " | " + saveSum.ToString()), outputRoll);
                }
                else
                {
                    DisplayRoll(input, sum.ToString(), outputRoll);
                }                
            }
            catch
            {
                DisplayRoll(input, "Error", outputRoll);
            }
        }

        private string[] RollSplit(string input)
        {
            string regx = @"([\+\-])";
            string[] rolled;
            try
            {
                rolled = Regex.Split(input, regx);
            }
            catch
            {
                rolled = new string[] {input};
            }
            return rolled;
        }
    }
}
