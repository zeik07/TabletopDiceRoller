using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabletopDiceRoller
{ 
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SaveView : ContentPage
    {
        public SaveView ()
		{            
            InitializeComponent ();
            RollName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);            
        }

        public SaveView(string roll)
        {
            InitializeComponent();
            RollName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
            Roll.Text = roll;
        }

        public SaveView(string roll, string name, int id)
        {
            InitializeComponent();
            RollName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
            Roll.Text = roll;
            RollName.Text = name;
            Save.CommandParameter = id;
        }

        private async void OnFocus(object sender, FocusEventArgs e)
        {
            Roll.Unfocus();
            var page = new EditView();
            MessagingCenter.Subscribe<EditView, string>(this, "RollInput", (send, arg) => {
                Roll.Text = arg;
            });
            await Navigation.PushAsync(page);
        }

        private void OnCustomSave(object sender, EventArgs e)
        {
            Error.Text = "";
            RollItem rollItem = new RollItem
            {
                Name = RollName.Text,
                Roll = Roll.Text
            };
            if (RollName.Text == null)
            {
                Error.Text = "Missing Field";
            }
            else
            {
                if (Save.CommandParameter != null)
                {
                    rollItem.ID = Convert.ToInt32(Save.CommandParameter);
                }
                App.Database.SaveItemAsync(rollItem);
                Navigation.PopAsync();
            }
            Navigation.PopAsync();
        }
    }
}