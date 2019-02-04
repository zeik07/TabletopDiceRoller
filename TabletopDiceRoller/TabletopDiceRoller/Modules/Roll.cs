using System;

namespace TabletopDiceRoller.Modules
{
    public class Roll
    {
        private Random rnd = new Random(Environment.TickCount);

        public int RollDie(int value)
        {
            int rolled = rnd.Next(1, (value + 1));
            return rolled;
        }

        public async void DisplayRoll(string die, string roll, string values)
        {
            var popUpPage = new DiceRollView(die, roll, values);
            await App.Current.MainPage.Navigation.PushModalAsync(popUpPage, false);
        }
    }
}
