using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabletopDiceRoller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomRollsView : ContentPage
	{
        Dictionary<int, KeyValuePair<string, string>> rolls = new Dictionary<int, KeyValuePair<string, string>>();

        public CustomRollsView ()
		{
			InitializeComponent ();
		}

        public async void CustomRoll(object sender, EventArgs e)
        {
            var customRolls = await App.Database.GetItemsAsync();
            foreach (var roll in customRolls)
            {
                if (roll.Name == null || roll.Roll == null)
                {
                    if (roll.Name == null && roll.Roll == null)
                    {
                        rolls.Add(roll.ID, new KeyValuePair<string, string>(roll.ID.ToString(), roll.ID.ToString()));
                    }
                    else if (roll.Roll == null)
                    {
                        rolls.Add(roll.ID, new KeyValuePair<string, string>(roll.Name, roll.ID.ToString()));
                    }
                    else
                    {
                        rolls.Add(roll.ID, new KeyValuePair<string, string>(roll.ID.ToString(), roll.Roll));
                    }
                }
                else
                {
                    rolls.Add(roll.ID, new KeyValuePair<string, string>(roll.Name, roll.Roll));
                }
            }
            CustomRollsList.ItemsSource = rolls.Values.Select(kvp => kvp.Value);
        }

        private void CustomDrop(object sender, EventArgs e)
        {
            CustomRollsList.ItemsSource = null;
            rolls.Clear();
        }

        private void OnCustomDelete(object sender, EventArgs e)
        {
            //await App.Database.DeleteItemAsync(CustomRollsList.SelectedItem);
            //testoutput.Text = rolls[(string)CustomRollsList.SelectedItem];
        }
    }
}