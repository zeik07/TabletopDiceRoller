using System;
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
                RollName.Text = "";
                CustomRollInput.Text = "";
            }
        }     
        
        private void OnCustomRoll(object sender, EventArgs e)
        {
            string input = CustomRollInput.Text;
            roll.CustomRoll(input);
        }

        private void OnCustomDelete(object sender, EventArgs e)
        {
            CustomRollInput.Text = "";
        }

        private void OnCustomInput(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            CustomRollInput.Text += button.Text;
        }
    }
}