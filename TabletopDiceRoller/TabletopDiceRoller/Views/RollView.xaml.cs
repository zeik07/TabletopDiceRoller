using System;
using TabletopDiceRoller.Modules;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabletopDiceRoller
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RollView : ContentPage
	{
        Roll roll = new Roll();

        public RollView ()
		{
			InitializeComponent ();
        }

        private async void OnCustomSave(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SaveView(CustomRollInput.Text));
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