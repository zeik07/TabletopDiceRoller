using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabletopDiceRoller
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DiceRoll : ContentPage
	{
		public DiceRoll (string die, string roll, string values)
		{
			InitializeComponent ();            
            TopLabel.Text = die;
            ContentLabel.Text = roll;
            BottomLabel.Text = values;
		}

        private async void PageTap(object sender, EventArgs e)
        {
            TopLabel.Text = "";
            ContentLabel.Text = "";
            BottomLabel.Text = "";
            await Navigation.PopModalAsync(false);
        }
    }
}