using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Controlpack
{
    public class BigButton : Control
    {
        public Image img;
        private Grid MainGrid;
        private TextBlock ContentText;
        Brush BgColor;
        public static readonly DependencyProperty ImgHeightDependency = DependencyProperty.Register("ImageHeight", typeof(double), typeof(BigButton));
        public static readonly DependencyProperty ImgWidthDependency = DependencyProperty.Register("ImageWidth", typeof(double), typeof(BigButton));
        public static readonly DependencyProperty ImgOpacityDependency = DependencyProperty.Register("ImageOpacity", typeof(double), typeof(BigButton));
        public static readonly DependencyProperty ImgMOOpacityDependency = DependencyProperty.Register("ImageMOOpacity", typeof(double), typeof(BigButton));
        public static readonly DependencyProperty ImgMLOpacityDependency = DependencyProperty.Register("ImageMLOpacity", typeof(double), typeof(BigButton));
        public static readonly DependencyProperty ImgSourceDependency = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(BigButton));
        public static readonly DependencyProperty ImgHrAlignmentDependency = DependencyProperty.Register("ImageHorizontalAlignment", typeof(HorizontalAlignment), typeof(BigButton));
        public static readonly DependencyProperty RectXRadiusDependency = DependencyProperty.Register("RadiuxX", typeof(double), typeof(BigButton));
        public static readonly DependencyProperty RectYRadiusDependency = DependencyProperty.Register("RadiuxY", typeof(double), typeof(BigButton));
        public static readonly DependencyProperty MouseEnterColorDependency = DependencyProperty.Register("MouseEnter Color", typeof(Brush), typeof(BigButton));
        public static readonly DependencyProperty DisplayTextDependency = DependencyProperty.Register("DisplayText", typeof(string), typeof(BigButton));
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
            MainGrid = GetTemplateChild("MainGrid") as Grid;
            ContentText = GetTemplateChild("ContentText") as TextBlock;
            Binding DisplayTextBinding = new Binding("DisplayText")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            ContentText.SetBinding(TextBlock.TextProperty, DisplayTextBinding);
            MouseEnter += this_MouseEnter;
            MouseLeave += this_MouseLeave;
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

            Binding ImageDefaultOpacityHorizontalAlignmentBinding = new Binding("ImageOpacity")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.OpacityProperty, ImageDefaultOpacityHorizontalAlignmentBinding);
            ImgMOOpacity = 0.6;
            BgColor = Background;
        }

        private void this_MouseLeave(object sender, MouseEventArgs e)
        {
            MainGrid.Background = BgColor;
        }

        private void this_MouseEnter(object sender, MouseEventArgs e)
        {
            MainGrid.Background = MouseEnterColor;
        }

        static BigButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BigButton), new FrameworkPropertyMetadata(typeof(BigButton)));
        }
    }
}
