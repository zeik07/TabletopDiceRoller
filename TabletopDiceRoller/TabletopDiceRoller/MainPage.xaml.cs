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
            var value = sender.CommandParameter.ToString();
            string die = ("d" + (Convert.ToInt32(value) - 1).ToString());
            DisplayAlert(die , Roll(value).ToString(), " ");            
        }

        private int Roll(string value)
        {
            Random rnd = new Random(Environment.TickCount);
            int rolled = rnd.Next(1, Convert.ToInt32(value));
            return rolled;
        }
    }
}
