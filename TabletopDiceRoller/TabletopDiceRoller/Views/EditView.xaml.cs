using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabletopDiceRoller
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditView : ContentPage
	{
        public string Input;
        
        public EditView ()
		{
			InitializeComponent ();
        }

        private void OnCustomInput(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            CustomRollInput.Text += button.Text;
            Input += button.Text;
        }

        private void OnCustomDelete(object sender, EventArgs e)
        {
            CustomRollInput.Text = "";
            Input = "";
        }

        protected override void OnDisappearing()
        {
            var save = new SaveView();
            MessagingCenter.Send<EditView, string>(this, "RollInput", Input);
        }
    }
}