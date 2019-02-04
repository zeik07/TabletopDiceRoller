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
            string input = CustomRollInput.Text;
            roll.CustomRoll(input);
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