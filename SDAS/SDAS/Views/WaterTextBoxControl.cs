using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SDAS.Views
{
    public class WaterTextBoxControl : TextBox
    {


        private bool isDefaultText = true;
        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register("DefaultText", typeof(string), typeof(WaterTextBoxControl), new PropertyMetadata(""));


        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (isDefaultText)
            {
                this.Text = "";
                this.isDefaultText = false;
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.Text = DefaultText;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            if (this.Text.Trim() == "")
            {
                this.Text = DefaultText;
                this.isDefaultText = true;
            }
         
        }
    }
}
