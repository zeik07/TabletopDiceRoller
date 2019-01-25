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

        private void OnButtonClick(Button sender, EventArgs e)
        {
            Output.Text = "";
            var value = sender.CommandParameter.ToString();
            Random rnd = new Random();
            int roll = rnd.Next(1, Convert.ToInt32(value));
            Output.Text = roll.ToString();
        }
    }
}
