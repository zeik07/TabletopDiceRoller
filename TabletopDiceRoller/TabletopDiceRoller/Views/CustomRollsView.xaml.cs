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

        public void CustomOnAppearing(object sender, EventArgs e)
        {
            CustomRolls();
        }

        public async void CustomRolls()
        {
            var customRolls = await App.Database.GetItemsAsync();

            var customRollsDataTemplate = new DataTemplate(() =>
            {
                var title = new Label();
                var grid = new Grid();
                var rollButton = new Button();
                var nameLabel = new Label();
                var rollLabel = new Label();
                var deleteButton = new Button();
                
                //title label
                title.Text = "Tabletop Dice Roller";
                title.FontSize = 40;
                title.HorizontalOptions = LayoutOptions.Center;
                //roll button
                rollButton.Text = "Roll";
                rollButton.Clicked += OnRollClick;
                rollButton.SetBinding(Button.CommandParameterProperty, "Roll");
                //name label
                nameLabel.SetBinding(Label.TextProperty, "Name");
                nameLabel.FontSize = 16;
                nameLabel.VerticalOptions = LayoutOptions.Center;
                //roll label
                rollLabel.SetBinding(Label.TextProperty, "Roll");
                rollLabel.FontSize = 16;
                rollLabel.VerticalOptions = LayoutOptions.Center;
                //delete button
                deleteButton.Text = "X";
                deleteButton.Clicked += OnCustomDelete;
                deleteButton.SetBinding(Button.CommandParameterProperty, "ID");

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2.75, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.75, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) });
                grid.Children.Add(nameLabel);
                grid.Children.Add(rollLabel, 1, 0);
                grid.Children.Add(rollButton, 2, 0);
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
            CustomRolls();
        }

        public void OnRollClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            roll.CustomRoll(button.CommandParameter.ToString());
        }
    }
}