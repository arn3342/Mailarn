using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controlpack
{
    public class TwoDetailDataCell : Control
    {
        static TwoDetailDataCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TwoDetailDataCell), new FrameworkPropertyMetadata(typeof(TwoDetailDataCell)));
        }
        #region Declaring variables
        TextBlock Text1, Text2;
        #endregion

        #region Declaring Properties
        public delegate void ClickedEvent(int UID, int CampID);
        public event ClickedEvent Clicked;
        public static readonly DependencyProperty DisplayText1Property = DependencyProperty.Register("DisplayText1", typeof(string), typeof(TwoDetailDataCell), new PropertyMetadata("Display Text 1"));
        public static readonly DependencyProperty DisplayText2Property = DependencyProperty.Register("DisplayText2", typeof(string), typeof(TwoDetailDataCell), new PropertyMetadata("Display Text 2"));
        public static readonly DependencyProperty UserIdProperty = DependencyProperty.Register("UserID", typeof(int), typeof(TwoDetailDataCell));
        public static readonly DependencyProperty CampIdProperty = DependencyProperty.Register("CampID", typeof(int), typeof(TwoDetailDataCell));
        [Description("Gets or sets the items of the dropdown menu of the control."), Category("Common")]
        public string DisplayText1
        {
            get { return GetValue(DisplayText1Property) as string; }
            set { SetValue(DisplayText1Property, value); }
        }
        [Description("Gets or sets the items of the dropdown menu of the control."), Category("Common")]
        public string DisplayText2
        {
            get { return GetValue(DisplayText2Property) as string; }
            set { SetValue(DisplayText2Property, value); }
        }
        public int UserID
        {
            get { return Convert.ToInt32(GetValue(UserIdProperty)); }
            set { SetValue(UserIdProperty, value); }
        }
        public int CampID
        {
            get { return Convert.ToInt32(GetValue(CampIdProperty)); }
            set { SetValue(CampIdProperty, value); }
        }
        #endregion
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            #region Initializing Controls, Bindings & events
            Text1 = (TextBlock)GetTemplateChild("Text1");
            Binding DisplayText1Binding = new Binding("DisplayText1")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            Text1.SetBinding(TextBlock.TextProperty, DisplayText1Binding);
            Text1.MouseDown += CampClicked;
            Text2 = (TextBlock)GetTemplateChild("Text2");
            Binding DisplayText2Binding = new Binding("DisplayText2")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            Text2.SetBinding(TextBlock.TextProperty, DisplayText2Binding);
            #endregion
        }

        private void CampClicked(object sender, MouseButtonEventArgs e)
        {
            ClickedEvent cl = Clicked;
            cl?.Invoke(UserID, CampID);
        }
    }
}
