using System;
using System.Collections.Generic;
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
    ///     <MyNamespace:SmallButtons/>
    ///
    /// </summary>
    public class SmallButtons : Control
    {
        static SmallButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SmallButtons), new FrameworkPropertyMetadata(typeof(SmallButtons)));
        }
        public Image img;
        private DockPanel MainRectangle;
        private TextBlock TextToolTip;
        public static readonly DependencyProperty ImgHeightDependency = DependencyProperty.Register("ImageHeight", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty ImgWidthDependency = DependencyProperty.Register("ImageWidth", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty ImgOpacityDependency = DependencyProperty.Register("ImgOpacity", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty ImgMOOpacityDependency = DependencyProperty.Register("ImgMOOpacity", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty ImgMLOpacityDependency = DependencyProperty.Register("ImgMLOpacity", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty ImgSourceDependency = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(SmallButtons));
        public static readonly DependencyProperty ToolTipTextDependency = DependencyProperty.Register("ToolTipText", typeof(string), typeof(SmallButtons));
        public static readonly DependencyProperty ImgHrAlignmentDependency = DependencyProperty.Register("ImageHorizontalAlignment", typeof(HorizontalAlignment), typeof(SmallButtons));
        public static readonly DependencyProperty RectXRadiusDependency = DependencyProperty.Register("RadiuxX", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty RectYRadiusDependency = DependencyProperty.Register("RadiuxY", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty MouseEnterColorDependency = DependencyProperty.Register("MouseEnter Color", typeof(Brush), typeof(SmallButtons), new UIPropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty AllowToolTipDependency = DependencyProperty.Register("Allow ToolTip", typeof(bool), typeof(SmallButtons));
        private BrushConverter bb = new BrushConverter();
        public HorizontalAlignment ImgHorizontalAlignment
        {
            get
            {
                return (HorizontalAlignment)GetValue(ImgHrAlignmentDependency);
            }
            set
            {
                SetValue(ImgHrAlignmentDependency, value);
            }
        }
        public bool DisallowDefaultToolTip
        {
            get
            {
                return Convert.ToBoolean(GetValue(AllowToolTipDependency));
            }
            set
            {
                SetValue(AllowToolTipDependency, value);
            }
        }
        public Brush MouseEnterColor
        {
            get
            {
                return GetValue(MouseEnterColorDependency) as Brush;
            }
            set
            {
                SetValue(MouseEnterColorDependency, value);
            }
        }
        public string ToolTipText
        {
            get
            {
                return System.Convert.ToString(GetValue(ToolTipTextDependency));
            }
            set
            {
                SetValue(ToolTipTextDependency, value);
            }
        }
        public double RadiusX
        {
            get
            {
                return Convert.ToDouble(GetValue(RectXRadiusDependency));
            }
            set
            {
                SetValue(RectXRadiusDependency, value);
            }
        }
        public double ImgOpacity
        {
            get
            {
                return Convert.ToDouble(GetValue(ImgOpacityDependency));
            }
            set
            {
                SetValue(ImgOpacityDependency, value);
            }
        }
        public double ImgMOOpacity
        {
            get
            {
                return Convert.ToDouble(GetValue(ImgMOOpacityDependency));
            }
            set
            {
                SetValue(ImgMOOpacityDependency, value);
            }
        }
        public double ImgMLOpacity
        {
            get
            {
                return Convert.ToDouble(GetValue(ImgMLOpacityDependency));
            }
            set
            {
                SetValue(ImgMLOpacityDependency, value);
            }
        }
        public double RadiusY
        {
            get
            {
                return Convert.ToDouble(GetValue(RectYRadiusDependency));
            }
            set
            {
                SetValue(RectYRadiusDependency, value);
            }
        }

        public double ImgHeight
        {
            get
            {
                return Convert.ToDouble(GetValue(ImgHeightDependency));
            }
            set
            {
                SetValue(ImgHeightDependency, value);
            }
        }

        public double ImgWidth
        {
            get
            {
                return Convert.ToDouble(GetValue(ImgWidthDependency));
            }
            set
            {
                SetValue(ImgWidthDependency, value);
            }
        }
        public ImageSource ImgSource
        {
            get
            {
                return (ImageSource)GetValue(ImgSourceDependency);
            }
            set
            {
                SetValue(ImgSourceDependency, value);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            MainRectangle = GetTemplateChild("MainRectangle") as DockPanel;
            MainRectangle.MouseEnter += MainGrid_MouseEnter;
            MainRectangle.MouseLeave += MainGrid_MouseLeave;

            if (DisallowDefaultToolTip == false)
            {
                TextToolTip = GetTemplateChild("ToolTip") as TextBlock;
                Binding ToolTipTextBinding = new Binding("ToolTipText")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                };
                TextToolTip.SetBinding(TextBlock.TextProperty, ToolTipTextBinding);
            }
            img = GetTemplateChild("Image") as System.Windows.Controls.Image;
            Binding ImageHeightBinding = new Binding("ImageHeight")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.HeightProperty, ImageHeightBinding);

            Binding ImageWidthBinding = new Binding("ImageWidth")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.WidthProperty, ImageWidthBinding);

            Binding ImageSourceBinding = new Binding("ImageSource")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.SourceProperty, ImageSourceBinding);

            Binding ImageHorizontalAlignmentBinding = new Binding("ImageHorizontalAlignment")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.HorizontalAlignmentProperty, ImageHorizontalAlignmentBinding);
            ImgHorizontalAlignment = HorizontalAlignment.Stretch;

            Binding ImageDefaultOpacityHorizontalAlignmentBinding = new Binding("ImageOpacity")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.OpacityProperty, ImageDefaultOpacityHorizontalAlignmentBinding);
            ImgMOOpacity = 0.6;
        }
        private void MainGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            ImgOpacity = ImgMOOpacity;
            MainRectangle.Background = MouseEnterColor;
        }
        private void MainGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            ImgOpacity = ImgMLOpacity;
            MainRectangle.Background = Brushes.Transparent;
        }
    }
}
