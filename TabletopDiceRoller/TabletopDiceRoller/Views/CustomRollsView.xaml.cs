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
                var grid = new Grid();
                var nameLabel = new Label();
                var rollLabel = new Label();
                var deleteButton = new Button();
                var menuButton = new Button();

                //name label
                nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
                nameLabel.FontSize = 16;
                nameLabel.VerticalOptions = LayoutOptions.Center;
                //roll label
                rollLabel.SetBinding(Label.TextProperty, new Binding("Roll"));
                rollLabel.FontSize = 16;
                rollLabel.VerticalOptions = LayoutOptions.Center;                
                //delete button
                deleteButton.Text = "X";
                deleteButton.Clicked += OnCustomDelete;
                deleteButton.SetBinding(Button.CommandParameterProperty, new Binding("ID"));
                //menu button
                menuButton.Text = char.ConvertFromUtf32(0x2193);
                menuButton.Clicked += OnMenuClick;

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) });

                grid.Children.Add(nameLabel);
                grid.Children.Add(rollLabel, 1, 0);
                grid.Children.Add(menuButton, 2, 0);
                grid.Children.Add(deleteButton, 3, 0);
                                
                ViewCell viewCell = new ViewCell
                {
                    View = grid                    
                };
                viewCell.Tapped += ViewCell_Tapped;

                return viewCell;
            });

            var listView = new ListView
            {
                ItemsSource = customRolls,
                ItemTemplate = customRollsDataTemplate,
                SelectionMode = ListViewSelectionMode.None,
                HasUnevenRows = true
            };

            Content = new StackLayout
            {
                Children =
                {
                    new Label {Text = "Tabletop Dice Roller", FontSize = 40, HorizontalOptions = LayoutOptions.Center },
                    listView                    
                }
            };
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell view = (ViewCell)sender;
            RollItem items = (RollItem)view.View.BindingContext;
            roll.CustomRoll(items.Roll);
        }

        private async void OnCustomDelete(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            RollItem delRoll = new RollItem
            {
                ID = Convert.ToInt32(button.CommandParameter.ToString())
            };
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