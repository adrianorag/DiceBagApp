using System;
using Xamarin.Forms;

namespace DiceBagApp.Helpers
{
    class CustomSwitch : Switch
    {
       
        public static readonly BindableProperty ItemProperty = BindableProperty.Create(nameof(Item), typeof(object), typeof(CustomSwitch), null);
        public object Item
        {
            get { return (object)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

    }
    
}
