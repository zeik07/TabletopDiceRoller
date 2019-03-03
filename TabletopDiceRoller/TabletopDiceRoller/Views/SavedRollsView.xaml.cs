using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TabletopDiceRoller.Modules;

namespace TabletopDiceRoller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SavedRollsView : ContentPage
	{
        Roll roll = new Roll();        

        public SavedRollsView ()
		{
            InitializeComponent ();            
        }

        public void CustomOnAppearing(object sender, EventArgs e)
        {
            CustomRolls();
        }

        private async void CustomRolls()
        {
            var customRolls = await App.Database.GetItemsAsync();

            var customRollsDataTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                var nameLabel = new Label();
                var rollLabel = new Label();
                var menuButton = new Button();

                //name label
                nameLabel.SetBinding(Label.TextProperty, new Binding("RollName"));
                nameLabel.FontSize = 18;
                nameLabel.VerticalOptions = LayoutOptions.Center;
                nameLabel.FontAttributes = FontAttributes.Bold;
                //roll label
                rollLabel.SetBinding(Label.TextProperty, new Binding("RollDice"));
                rollLabel.FontSize = 18;
                rollLabel.VerticalOptions = LayoutOptions.Center;
                //menu button
                menuButton.Text = char.ConvertFromUtf32(0x25BC);
                menuButton.SetBinding(Button.CommandParameterProperty, new Binding("RollID"));
                menuButton.Clicked += (s, arg) => 
                {
                    DependencyService.Get<IPopUp>().PopUp((View)s, this);
                };

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Auto) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Auto) });

                grid.Children.Add(nameLabel, 1, 0);
                grid.Children.Add(rollLabel, 1, 1);
                grid.Children.Add(menuButton, 2, 0);
                Grid.SetRowSpan(menuButton, 2);
                grid.BackgroundColor = Color.BlueViolet;

                Frame frame = new Frame
                {
                    Content = grid,
                    Padding = 0,
                    Margin = new Thickness(5,5,5,5)
                };

                ViewCell viewCell = new ViewCell
                {
                    View = frame
                };
                viewCell.Tapped += ViewCell_Tapped;
                                
                return viewCell;
            });

            var listView = new ListView
            {
                ItemsSource = customRolls,
                ItemTemplate = customRollsDataTemplate,
                SelectionMode = ListViewSelectionMode.None,
                RowHeight = 70
            };

            var floatButton = new MyFloatButton
            {
                Margin = new Thickness(0, 0, 0, 0),
                HeightRequest = 56,
                WidthRequest = 56,
                HorizontalOptions = LayoutOptions.End
            };

            Content = new StackLayout
            {
                Children =
                {
                    new Label {Text = "zDice", FontSize = 40, HorizontalOptions = LayoutOptions.Center },
                    listView,
                    floatButton
                }
            };
        }

        private async void ViewCell_Tapped(object sender, EventArgs e)
        {
            bool isCrit = false;                       
            ViewCell view = (ViewCell)sender;
            RollItem item = (RollItem)view.View.BindingContext;
            if (item.CanCrit)
            {
                isCrit = await DisplayAlert(null, "Is this a critical hit?", "Yes", "No");
            } 
            roll.CustomRoll(item, isCrit);
        }

        public void RefreshView()
        {
            var viewModel = BindingContext;

            BindingContext = null;
            InitializeComponent();

            BindingContext = viewModel;
            CustomRolls();
        }

        public async void EditRoll(int id)
        {         
            RollItem item = await App.Database.GetItemAsync(id);
            await Navigation.PushAsync(new SaveView(item));
        }
    }
}