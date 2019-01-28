using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TabletopDiceRoller
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnButtonClick(Button button, EventArgs e)
        {
            var value = button.CommandParameter.ToString();
            string die = ("d" + (Convert.ToInt32(value) - 1).ToString());
            string roll = Roll(value).ToString();
            DisplayRoll(die, roll, roll);
        }

        private void PageTap(object sender, EventArgs e)
        {
            TopLabel.Text = "";
            ContentLabel.Text = "";
            BottomLabel.Text = "";
            PopUp.IsVisible = false;
        }

        private int Roll(string value)
        {
            Random rnd = new Random(Environment.TickCount);
            int rolled = rnd.Next(1, Convert.ToInt32(value));
            return rolled;
        }

        private void DisplayRoll(string die, string roll, string values)
        {
            PopUp.IsVisible = true;
            TopLabel.Text = die;
            ContentLabel.Text = roll;
            BottomLabel.Text = values;            
        }
    }
}
