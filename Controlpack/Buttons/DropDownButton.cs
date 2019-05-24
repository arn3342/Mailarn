using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class DropDownButton : Control
    {
        #region declarations
        Grid MainBody;
        ContextMenu DropDownMenu;
        StackPanel OptionStack;
        ResourceDictionary rcd;
        RotateTransform Rotate;
        Path Arrow;
        bool OptionExists;
        #endregion
        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
        }
        public DropDownButton()
        {
            SetCurrentValue(MenuOptionsProperty, new List<TextBlock>());
        }
        #region Declaring Properties
        public static readonly DependencyProperty ArrowDirectionProperty = DependencyProperty.Register("ArrowDirection", typeof(Direction), typeof(DropDownButton));
        public static readonly DependencyProperty MouseEnterColorProperty = DependencyProperty.Register("MouseEnterColor", typeof(Brush), typeof(DropDownButton), new UIPropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty ArrowColorProperty = DependencyProperty.Register("ArrowColor", typeof(Brush), typeof(DropDownButton), new UIPropertyMetadata(Brushes.Gray));
        public static readonly DependencyProperty MenuOptionsProperty =DependencyProperty.Register(nameof(MenuOptions), typeof(List<TextBlock>), typeof(DropDownButton));
        [Description("Gets or sets the items of the dropdown menu of the control."), Category("Common")]       
        public List<TextBlock> MenuOptions
        {
            get { return (List<TextBlock>)GetValue(MenuOptionsProperty); }
            set { SetValue(MenuOptionsProperty, value); }
        }
        [Description("Gets or sets the mouse hover color of the control."), Category("Brush")]
        public Brush MouseEnterColor
        {
            get {return GetValue(MouseEnterColorProperty) as Brush; }
            set {SetValue(MouseEnterColorProperty, value); }
        }
        [Description("Gets or sets the arrow color of the control."), Category("Brush")]
        public Brush ArrowColor
        {
            get
            {
                return GetValue(ArrowColorProperty) as Brush;
            }
            set
            {
                SetValue(ArrowColorProperty, value);
            }
        }
        [Description("Gets or sets the arrow direction of the control."), Category("Layout")]
        public Direction ArrowDirection
        {
            get
            {
                return (Direction)GetValue(ArrowDirectionProperty);
            }
            set
            {
                SetValue(ArrowDirectionProperty, value);
            }
        }
        #endregion
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            rcd = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/Controlpack;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute)
            };

            #region Initializing Controls, Bindings & events
            MainBody = (Grid)GetTemplateChild("MainBody");
            Rotate = (RotateTransform)GetTemplateChild("Rotate");
            Arrow = (Path)GetTemplateChild("Arrow");
            DropDownMenu = (ContextMenu)GetTemplateChild("DropDownMenu");
            this.MouseEnter += ChangeBackgroundColorOnHover;
            this.MouseLeave += ChangeBackgroundColorOnLeave;
            MainBody.MouseDown += ShowOptions;
            #endregion

            Binding ArrowColorBinding = new Binding("ArrowColor")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };    
            Arrow.SetBinding(Shape.StrokeProperty, ArrowColorBinding);

            #region Setting arrow direction
            if (ArrowDirection.Right)
            {
                Rotate.Angle = 0;
            }
            else if (ArrowDirection.Left)
            {
                Rotate.Angle = 180;
            }
            else if (ArrowDirection.Up)
            {
                Rotate.Angle = -90;
            }
            else if (ArrowDirection.Down)
            {
                Rotate.Angle = 90;
            }
            #endregion
        }
        
        #region Contextmenu Management
        private void ShowOptions(object sender, MouseButtonEventArgs e)
        {
            #region Opening Context Menu
            var cm = ContextMenuService.GetContextMenu(sender as DependencyObject);
            if (cm == null)
            {
                return;
            }
            cm.Placement = PlacementMode.Bottom;
            cm.PlacementRectangle = new Rect(-200 + Width , Height, 0, 0);
            cm.PlacementTarget = sender as UIElement;
            cm.IsOpen = true;
            #endregion

            #region Populating Context Menu
            if (!OptionExists)
            {
                var template = DropDownMenu.Template;
                OptionStack = (StackPanel)template.FindName("OptionStack", DropDownMenu);
                foreach (TextBlock tb in MenuOptions)
                {
                    tb.Style = (Style)rcd["DropDownOptions"];
                    OptionStack.Children.Add(tb);
                }
                OptionExists = true;
            }
            #endregion
        }
        #endregion
        #region Color Change Events
        private void ChangeBackgroundColorOnLeave(object sender, MouseEventArgs e)
        {
           this.Background = Brushes.Transparent;
        }
        private void ChangeBackgroundColorOnHover(object sender, MouseEventArgs e)
        { 
           this.Background = MouseEnterColor;
        }
        #endregion
        
        #region structs
        public struct Direction
        {
            public readonly bool Right;
            public readonly bool Left;
            public readonly bool Up;
            public readonly bool Down;
            public Direction(bool right, bool left, bool up, bool down)
            {
                Right = right; Left = left; Up = up; Down = down;
            }
        }
        #endregion
    }
}
