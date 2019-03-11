using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TabletopDiceRoller
{
    class RollsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RollTemplate { get; set; }
        public DataTemplate LabelTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item.GetType() == typeof(ContainerItem) ? LabelTemplate : RollTemplate;
        }
    }
}
