using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
namespace Controlpack
{
    public class ExtendedTextbox : Control
    {
        TextBox MainText;
        string tx;
        public static readonly DependencyProperty DisplayTextDependency = DependencyProperty.Register("DisplayText", typeof(string), typeof(ExtendedTextbox));
        public static readonly DependencyProperty TextDependency = DependencyProperty.Register("Text", typeof(string), typeof(ExtendedTextbox), new PropertyMetadata(""));
        public static readonly DependencyProperty MaxLengthDependency = DependencyProperty.Register("MaxLength", typeof(int), typeof(ExtendedTextbox), new PropertyMetadata(1000));
        public static readonly DependencyProperty PasswordDependency = DependencyProperty.Register("IsPasswordEnabled", typeof(bool), typeof(ExtendedTextbox));
        public string DisplayText
        {
            get
            {
                return System.Convert.ToString(GetValue(DisplayTextDependency));
            }
            set
            {
                SetValue(DisplayTextDependency, value);
            }
        }
        public string Text
        {
            get
            {
                return System.Convert.ToString(GetValue(TextDependency));
            }
            set
            {
                SetValue(TextDependency, value);
            }
        }
        public int MaxLength
        {
            get
            {
                return (int)(GetValue(MaxLengthDependency));
            }
            set
            {
                SetValue(MaxLengthDependency, value);
            }
        }
        public bool IsPasswordEnabled
        {
            get
            {
                return Convert.ToBoolean(GetValue(PasswordDependency));
            }
            set
            {
                SetValue(DisplayTextDependency, value);
            }
        }
        static ExtendedTextbox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedTextbox), new FrameworkPropertyMetadata(typeof(ExtendedTextbox)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            tx = DisplayText;
            MainText = GetTemplateChild("MainText") as TextBox;
            //MainText.Text = Text;
            MainText.LostFocus += MainText_LostFocus;
            MainText.GotFocus += MainText_GotFocus;
            MainText.TextChanged += MainText_TextChanged;
            Binding TextBinding = new Binding("Text")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            MainText.SetBinding(TextBox.TextProperty, TextBinding);
            Text = DisplayText;
            Binding MaxLengthBinding = new Binding("MaxLength")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            MainText.SetBinding(TextBox.MaxLengthProperty, MaxLengthBinding);
            if (IsPasswordEnabled == true)
            {
                MainText.FontFamily = new FontFamily("ms-appx:///Assets/PassDot.ttf#PassDot");
            }
        }
        void MainText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MainText.Text == "")
            {
                MainText.Text = tx;
            }
        }
        void MainText_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MainText.Text == DisplayText)
            {
                MainText.Text = "";
            }
        }
        void MainText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = MainText.Text;
        }
    }
}
