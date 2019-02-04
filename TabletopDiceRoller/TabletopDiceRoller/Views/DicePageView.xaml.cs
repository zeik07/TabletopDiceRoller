using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TabletopDiceRoller.Modules;

namespace TabletopDiceRoller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DicePageView : ContentView
	{
        Roll roll = new Roll();

        public DicePageView ()
		{
			InitializeComponent ();
		}

        private void OnButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string[] split = button.Text.Split('d');
            int value = Convert.ToInt32(split[1]);
            string die = button.Text;
            string rolled = roll.RollDie(value).ToString();
            roll.DisplayRoll(die, rolled, rolled);
        }        
    }
}