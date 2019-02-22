using Android.Support.V7.Widget;
using Android.Views;
using System;
using TabletopDiceRoller.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency (typeof (PopUp_Android))]
namespace TabletopDiceRoller.Droid
{
    class PopUp_Android : IPopUp
    {
        public PopUp_Android() { }

        public void PopUp(Xamarin.Forms.View view, object v)
        {
            var menu = new PopupMenu(MainActivity.Context , (View)view.GetRenderer()) {};
            menu.Inflate(Resource.Menu.popup_menu);
            menu.MenuItemClick += (s, e) => 
            {
                if (e.Item.ToString() == "Delete")
                {
                    OnCustomDelete(view, e, v);
                }
                else if (e.Item.ToString() == "Edit")
                {
                    OnCustomEdit(view, e, v);
                }
            };
            menu.Show();
        }

        private async void OnCustomEdit(object sender, EventArgs e, object v)
        {
            Xamarin.Forms.Button button = (Xamarin.Forms.Button)sender;
            int id = Convert.ToInt32(button.CommandParameter);
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SaveView(id));
        }

        private async void OnCustomDelete(object sender, EventArgs e, object v)
        {
            Xamarin.Forms.Button button = (Xamarin.Forms.Button)sender;
            RollItem delRoll = new RollItem
            {
                ID = Convert.ToInt32(button.CommandParameter.ToString())
            };
            await App.Database.DeleteItemAsync(delRoll);
            SavedRollsView test = (SavedRollsView)v;
            test.RefreshView();
        }
    }
}