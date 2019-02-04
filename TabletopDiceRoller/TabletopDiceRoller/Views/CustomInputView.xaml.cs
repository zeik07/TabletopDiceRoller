using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using TabletopDiceRoller.Modules;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabletopDiceRoller
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomInputView : ContentView
	{
        Roll roll = new Roll();
        DataTable dt = new DataTable();

        public CustomInputView ()
		{
			InitializeComponent ();
		}        

        private void OnCustomSave(object sender, EventArgs e)
        {
            Error.Text = "";
            if (RollName.Text == null || CustomRollInput.Text == null)
            {
                Error.Text = "Missing Field";
            }
            else
            {
                RollItem rollItem = new RollItem();
                rollItem.Name = RollName.Text;
                rollItem.Roll = CustomRollInput.Text;
                App.Database.SaveItemAsync(rollItem);
            }
        }

        private void OnCustomRoll(object sender, EventArgs e)
        {
            string error = "Improper format. e.g. 8d6+1d4+1";
            string input = CustomRollInput.Text;
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
                            diceResult[i] = roll.RollDie(diceValue).ToString();
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
                roll.DisplayRoll(input, sum.ToString(), outputRoll);
            }
            catch
            {
                roll.DisplayRoll(input, "Error", outputRoll);
            }
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