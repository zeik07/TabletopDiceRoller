using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TabletopDiceRoller.Modules;

namespace TabletopDiceRoller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomRollsView : ContentPage
	{
        Roll roll = new Roll();

        public CustomRollsView ()
		{
			InitializeComponent ();            
        }

        public async void CustomRolls(object sender, EventArgs e)
        {
            var customRolls = await App.Database.GetItemsAsync();

            var customRollsDataTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                var rollButton = new Button();
                var idLabel = new Label();
                var nameLabel = new Label();
                var rollLabel = new Label();
                var deleteButton = new Button();

                rollButton.Text = "Roll";
                rollButton.Clicked += OnRollClick;
                rollButton.SetBinding(Button.CommandParameterProperty, "Roll");
                nameLabel.SetBinding(Label.TextProperty, "Name");
                rollLabel.SetBinding(Label.TextProperty, "Roll");
                deleteButton.Text = "Delete";
                deleteButton.Clicked += OnCustomDelete;
                deleteButton.SetBinding(Button.CommandParameterProperty, "ID");

                grid.Children.Add(rollButton);
                grid.Children.Add(nameLabel, 1, 0);
                grid.Children.Add(rollLabel, 2, 0);
                grid.Children.Add(deleteButton, 3, 0);

                return new ViewCell { View = grid };
            });

            Content = new StackLayout
            {
                Children =
                {
                    new ListView { ItemsSource = customRolls, ItemTemplate = customRollsDataTemplate }                
                }                
            };            
        }

        private async void OnCustomDelete(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            RollItem delRoll = new RollItem();
            delRoll.ID = Convert.ToInt32(button.CommandParameter.ToString());
            await App.Database.DeleteItemAsync(delRoll);

            var viewModel = BindingContext;

            BindingContext = null;
            InitializeComponent();

            BindingContext = viewModel;
        }

        public void OnRollClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            roll.CustomRoll(button.CommandParameter.ToString());
        }
    }
}