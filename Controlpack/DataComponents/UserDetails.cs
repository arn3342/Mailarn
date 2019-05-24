using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controlpack
{
    public class UserDetails : Control
    {
        static UserDetails()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UserDetails), new FrameworkPropertyMetadata(typeof(UserDetails)));
        }
        TextBlock DisplayText1txt, DisplayText2txt, FirstChartxt;
        DropDownButton DropDownBtn;
        public static readonly DependencyProperty DisplayText1Dependency = DependencyProperty.Register("DisplayText1", typeof(string), typeof(UserDetails), new UIPropertyMetadata("Display Text 1"));
        public static readonly DependencyProperty DisplayText2Dependency = DependencyProperty.Register("DisplayText2", typeof(string), typeof(UserDetails), new UIPropertyMetadata("Display Text 2"));
        public string DisplayText1
        {
            get
            {
                return GetValue(DisplayText1Dependency) as string;
            }
            set
            {
                SetValue(DisplayText1Dependency, value);
            }
        }
        public string DisplayText2
        {
            get
            {
                return GetValue(DisplayText2Dependency) as string;
            }
            set
            {
                SetValue(DisplayText2Dependency, value);
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DisplayText1txt = GetTemplateChild("DisplayText1txt") as TextBlock;
            DisplayText2txt = GetTemplateChild("DisplayText2txt") as TextBlock;
            DropDownBtn = (DropDownButton)GetTemplateChild("DropDownBtn");
            FirstChartxt = GetTemplateChild("FirstChartxt") as TextBlock;
            Binding DisplayText1Binding = new Binding("DisplayText1")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            DisplayText1txt.SetBinding(TextBlock.TextProperty, DisplayText1Binding);
            DropDownBtn.MouseDown += ShowOptions;
            Binding DisplayText2Binding = new Binding("DisplayText2")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            DisplayText2txt.SetBinding(TextBlock.TextProperty, DisplayText2Binding);
            if (DisplayText1txt.Text.Length != 0)
            {
                FirstChartxt.Text = DisplayText1txt.Text.ToString().Substring(0, 1).ToUpper();
            }
        }

        private void ShowOptions(object sender, MouseButtonEventArgs e)
        {
            #region Opening Context Menu
            var cm = ContextMenuService.GetContextMenu(sender as DependencyObject);
            if (cm ==null)
            {
                return;
            }
            cm.Placement = PlacementMode.Bottom;
            cm.PlacementTarget = sender as UIElement;
            cm.IsOpen = true;
            #endregion

            #region

            #endregion
        }
    }
}
