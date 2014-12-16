using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SDAS.Views
{
    public class WaterTextBoxControl : TextBox
    {
        private bool mIsDefaultText;
        private bool IsDefaultText
        {
            get
            {
                return mIsDefaultText;
            }
            set
            {
                mIsDefaultText = value;
                if (mIsDefaultText)
                {
                    this.Text = DefaultText;
                    this.Foreground = Brushes.Gray;
                }
                else
                {
                    this.Text = "";
                    this.Foreground = Brushes.Black;
                }
            }
        }

        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }
        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register("DefaultText", typeof(string), typeof(WaterTextBoxControl), new PropertyMetadata(""));


        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (IsDefaultText)
            {
                this.IsDefaultText = false;
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            IsDefaultText = true;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            if (this.Text.Trim() == "")
            {
                this.IsDefaultText = true;
            }
         
        }
    }
}
