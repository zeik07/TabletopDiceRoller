using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TabletopDiceRoller.Modules;
using System.Collections.Generic;

namespace TabletopDiceRoller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SavedRollsView : ContentPage
	{
        Roll roll = new Roll();
        DataTemplate rollTemplate;
        DataTemplate labelTemplate;

        public SavedRollsView ()
		{
            InitializeComponent ();
            Initiate();
        }

        public void CustomOnAppearing(object sender, EventArgs e)
        {
            CustomRolls();
        }

        private DataTemplate LabelTemplate()
        {
            labelTemplate = new DataTemplate(() =>
            {
                var nameLabel = new Label
                {
                    FontSize = 24,
                    VerticalOptions = LayoutOptions.Center
                };
                nameLabel.Margin = new Thickness(5, 5, 5, 5);
                nameLabel.SetBinding(Label.TextProperty, new Binding("Container"));

                ViewCell viewCell = new ViewCell
                {
                    View = nameLabel
                };

                return viewCell;
            });

            return labelTemplate;
        }

        private DataTemplate RollTemplate()
        {
            rollTemplate = new DataTemplate(() =>
            {
                var nameLabel = new Label
                {
                    FontSize = 18,
                    VerticalOptions = LayoutOptions.Center,
                    FontAttributes = FontAttributes.Bold
                };
                nameLabel.SetBinding(Label.TextProperty, new Binding("RollName"));

                var rollLabel = new Label
                {
                    FontSize = 18,
                    VerticalOptions = LayoutOptions.Center
                };
                rollLabel.SetBinding(Label.TextProperty, new Binding("RollDice"));

                var menuButton = new Button
                {
                    Text = char.ConvertFromUtf32(0x25BC)
                };
                menuButton.SetBinding(Button.CommandParameterProperty, new Binding("RollID"));
                menuButton.Clicked += (s, arg) =>
                {
                    DependencyService.Get<IPopUp>().PopUp((View)s, this);
                };

                var grid = new Grid();
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
                    Margin = new Thickness(5, 5, 5, 5)
                };

                ViewCell viewCell = new ViewCell
                {
                    View = frame
                };
                viewCell.Tapped += ViewCell_Tapped;

                return viewCell;
            });
            return rollTemplate;
        }

        private void Initiate()
        {
            //required to initialize ContainerItem so objects of ContainerItem created
            //in a loop are accessible
            ContainerItem initiate = new ContainerItem();
            var foo = initiate.Container;
        }

        private async void CustomRolls()
        {
            List<object> listRolls = new List<object>();

            var profiles = await App.Database.GetProfliesAsync();
            for (int p = 0; p < profiles.Count; p++)
            {
                RollItem profile = profiles[p];
                listRolls.Add(new ContainerItem { Container = profile.Profile });
                var folders = await App.Database.GetFoldersAsync(profile.Profile);
                for (int f = 0; f < folders.Count; f++)
                {
                    RollItem folder = folders[f];                    
                    listRolls.Add(new ContainerItem { Container = folder.Folder });
                    var rolls = await App.Database.GetItemsAsync(profile.Profile, folder.Folder);
                    foreach (var roll in rolls)
                    {
                        listRolls.Add(roll);
                    }                    
                }
            }

            var listView = new ListView
            {
                ItemsSource = listRolls,
                ItemTemplate = new RollsTemplateSelector { RollTemplate = RollTemplate(), LabelTemplate = LabelTemplate() },
                SelectionMode = ListViewSelectionMode.None,
                RowHeight = 70,
                HasUnevenRows = true
            };
            
            var floatButton = new MyFloatButton
            {
                Margin = new Thickness(0, 0, 0, 0),
                HeightRequest = 56,
                WidthRequest = 56,
                HorizontalOptions = LayoutOptions.End
            };

            var layout = new StackLayout
            {
                Children =
                {
                    new Label {Text = "zDice", FontSize = 40, HorizontalOptions = LayoutOptions.Center },
                    listView,
                    floatButton
                }                
            };

            Content = layout;
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