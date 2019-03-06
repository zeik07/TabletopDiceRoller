using System;
using System.Collections.Generic;
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
            ProfileName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
            FolderName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
            RollName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);            
        }

        public SaveView(string roll)
        {
            InitializeComponent();
            ProfileName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
            FolderName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
            RollName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
            Roll.Text = roll;
        }

        public SaveView(RollItem item)
        {
            InitializeComponent();
            RollName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
            Roll.Text = item.RollDice;
            RollName.Text = item.RollName;
            CanSaveToggle.IsToggled = item.CanSave;
            CanCritToggle.IsToggled = item.CanCrit;
            Save.CommandParameter = item.RollID;
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
            if (ProfileName.Text is null || FolderName.Text is null || RollName.Text is null || Roll.Text is null)
            {
                Error.Text = "Missing Field";
                return;
            }
            Error.Text = "";
            
            RollItem rollItem = new RollItem
            {
                RollName = RollName.Text,
                RollDice = Roll.Text,
                CanSave = CanSaveToggle.IsToggled,
                CanCrit = CanCritToggle.IsToggled,
                Folder = FolderName.Text,
                Profile = ProfileName.Text
            };
            
            HasLevels hasLevels = new HasLevels
            {
                BaseRoll = null
            };

            if (Save.CommandParameter != null)
            {
                rollItem.RollID = Convert.ToInt32(Save.CommandParameter);
            }

            App.Database.SaveItemAsync(rollItem);
            Navigation.PopAsync();
        }

        public void CritToggled(object sender, EventArgs e)
        {
            if (CanCritToggle.IsToggled == true)
            {
                if (CanSaveToggle.IsToggled == true)
                {
                    CanSaveToggle.IsToggled = false;
                }
            }
        }

        public void SaveToggled(object sender, EventArgs e)
        {
            if (CanSaveToggle.IsToggled == true)
            {
                if(CanCritToggle.IsToggled == true)
                {
                    CanCritToggle.IsToggled = false;
                }
            }
        }
    }
}