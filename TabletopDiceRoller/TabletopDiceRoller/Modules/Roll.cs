using System;
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
            await App.Current.MainPage.Navigation.PushModalAsync(popUpPage, false);
        }

        public void CustomRoll(string textInput)
        {
            string error = "Improper format. e.g. 8d6+1d4+1";
            string input = textInput;
            string reg = @"([\+\-])";
            string[] rolled = Regex.Split(input, reg);
            string outputRoll = "";
            foreach (string r in rolled)
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
                        for (int i = 0; i < Convert.ToInt32(diceRoll[0]); i++)
                        {
                            diceResult[i] = RollDie(diceValue).ToString();
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
                    outputRoll += r;
                }
            }
            try
            {
                int sum = (int)dt.Compute(outputRoll, "");
                DisplayRoll(input, sum.ToString(), outputRoll);
            }
            catch
            {
                DisplayRoll(input, "Error", outputRoll);
            }
        }
    }
}
