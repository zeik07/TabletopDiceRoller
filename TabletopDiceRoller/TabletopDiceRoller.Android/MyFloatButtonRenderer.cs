using System;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Views;
using TabletopDiceRoller;
using TabletopDiceRoller.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyFloatButton), typeof(MyFloatButtonRenderer))]
namespace TabletopDiceRoller.Droid
{
    public class MyFloatButtonRenderer : ViewRenderer<MyFloatButton, FloatingActionButton>
    {
        private FloatingActionButton fab;

        public MyFloatButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<MyFloatButton> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                fab = new FloatingActionButton(Context)
                {
                    LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent),
                    Clickable = true
                };
                fab.SetImageResource(Resource.Drawable.round_add_black_48);                
                SetNativeControl(fab);
            }

            if (e.NewElement != null)
            {
                fab.Click += Fab_Click;
            }

            if (e.OldElement != null)
            {
                fab.Click -= Fab_Click;
            }
        }

        private async void Fab_Click(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SaveView());
        }
    }
}