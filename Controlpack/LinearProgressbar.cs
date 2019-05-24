using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Controlpack
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Controlpack"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Controlpack;assembly=Controlpack"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:LinearProgressbar/>
    ///
    /// </summary>
    public class LinearProgressbar : Control
    {
        static LinearProgressbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearProgressbar), new FrameworkPropertyMetadata(typeof(LinearProgressbar)));
        }
        #region Control declarations
        StackPanel MainStack;
        TextBlock CaptionText;
        Rectangle LoadingRect;
        #endregion

        #region Property declarations & set up
        public static readonly DependencyProperty HasDropShadowDependency = DependencyProperty.Register("HasDropShadow", typeof(bool), typeof(ProgressBar));
        public static readonly DependencyProperty WaitingLineColorDependency = DependencyProperty.Register("WaitingLineColor", typeof(Brush), typeof(ProgressBar), new UIPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 255, 69, 176))));
        public static readonly DependencyProperty ForeColorDependency = DependencyProperty.Register("ForeColor", typeof(Brush), typeof(ProgressBar), new UIPropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty DisplayTextDependency = DependencyProperty.Register("DisplayText", typeof(string), typeof(ProgressBar));
        [Description("Adds or removes dropshadow effect from the control."), Category("Layout")]
        public bool HasDropShadow
        {
            get
            {
                return Convert.ToBoolean(GetValue(HasDropShadowDependency));
            }
            set
            {
                SetValue(HasDropShadowDependency, value);
            }
        }
        [Description("Gets or sets the color of the waiting/progress bar."), Category("Brush")]
        public Brush WaitingLineColor
        {
            get
            {
                return GetValue(WaitingLineColorDependency) as Brush;
            }
            set
            {
                SetValue(WaitingLineColorDependency, value);
            }
        }
        [Description("Gets or sets the color of the waiting/progress bar."), Category("Brush")]
        public Brush ForeColor
        {
            get
            {
                return GetValue(ForeColorDependency) as Brush;
            }
            set
            {
                SetValue(ForeColorDependency, value);
            }
        }
        [Description("Gets or sets the display text of the control."), Category("Text")]
        public string DisplayText
        {
            get
            {
                return GetValue(DisplayTextDependency).ToString();
            }
            set
            {
                SetValue(DisplayTextDependency, value);
            }
        }
        #endregion
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            #region Control intialization & property binding
            MainStack = GetTemplateChild("MainStack") as StackPanel;
            CaptionText = GetTemplateChild("CaptionText") as TextBlock;
            LoadingRect = GetTemplateChild("LoadingRect") as Rectangle;
            Binding WaitingLineColorBinding = new Binding("WaitingLineColor")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            LoadingRect.SetBinding(Rectangle.FillProperty, WaitingLineColorBinding);
            Binding DisplayTextBinding = new Binding("DisplayText")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            CaptionText.SetBinding(TextBlock.TextProperty, DisplayTextBinding);
            Binding ForeColorBinding = new Binding("ForeColor")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            CaptionText.SetBinding(TextBlock.ForegroundProperty, ForeColorBinding);
            #endregion
            if (HasDropShadow)
            {
                MainStack.Effect = new DropShadowEffect
                {
                    Color = new Color { A = 182, R = 182, G = 182, B = 182 },
                    Direction = 315,
                    ShadowDepth = 5,
                    Opacity = 0.3,
                    BlurRadius = 15,
                    RenderingBias = RenderingBias.Performance
                };
            }
            MainLoad(null, null);
        }
        #region Animations
        private void MainLoad(object sender, EventArgs e)
        {
            LoadingRect.HorizontalAlignment = HorizontalAlignment.Left;
            CircleEase ease = new CircleEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            DoubleAnimation growAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(1),
                From = 0,
                To = 200,
                EasingFunction = ease
            };
            Storyboard.SetTarget(growAnimation, LoadingRect);
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath(Rectangle.WidthProperty));
            Storyboard sb = new Storyboard();
            sb.Completed += ReverseLoad;
            sb.Children.Add(growAnimation);
            sb.Begin();
        }
        private void MainLoad2(object sender, EventArgs e)
        {
            LoadingRect.HorizontalAlignment = HorizontalAlignment.Left;
            CircleEase ease = new CircleEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            DoubleAnimation growAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.5),
                From = 200,
                To = 0,
                EasingFunction = ease
            };
            Storyboard.SetTarget(growAnimation, LoadingRect);
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath(Rectangle.WidthProperty));
            Storyboard sb = new Storyboard();
            sb.Completed += MainLoad;
            sb.Children.Add(growAnimation);
            sb.Begin();
        }
        private void ReverseLoad(object sender, EventArgs e)
        {
            LoadingRect.HorizontalAlignment = HorizontalAlignment.Right;
            CircleEase ease = new CircleEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            DoubleAnimation growAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.5),
                From = 200,
                To = 0,
                EasingFunction = ease
            };
            Storyboard.SetTarget(growAnimation, LoadingRect);
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath(Rectangle.WidthProperty));
            Storyboard sb = new Storyboard();
            sb.Completed += ReverseLoad2;
            sb.Children.Add(growAnimation);
            sb.Begin();
        }
        private void ReverseLoad2(object sender, EventArgs e)
        {
            CircleEase ease = new CircleEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            DoubleAnimation growAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(1),
                From = 0,
                To = 200,
                EasingFunction = ease
            };
            Storyboard.SetTarget(growAnimation, LoadingRect);
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath(Rectangle.WidthProperty));
            Storyboard sb = new Storyboard();
            sb.Completed += MainLoad2;
            sb.Children.Add(growAnimation);
            sb.Begin();
        }
        #endregion
    }
}
