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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public struct HSV { public float h; public float s; public float v; }

        public MainWindow()
        {
            InitializeComponent();
            applyNewColor();            

            hu.IsEnabled = false;
            sa.IsEnabled = false;
            br.IsEnabled = false;

            R.IsEnabled = true;
            G.IsEnabled = true;
            B.IsEnabled = true;
        }

        public void applyNewColor()
        {
            var r = Convert.ToByte(R.Value);
            var g = Convert.ToByte(G.Value);
            var b = Convert.ToByte(B.Value);
            
            System.Drawing.Color color = System.Drawing.Color.FromArgb(255, r, g, b);
            
            Brush brush = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
            
            Rec.Fill = brush;

            var hue = color.GetHue();
            var sat = color.GetSaturation();
            var brightness = color.GetBrightness();

            ColorReverse(hue, sat, brightness);

            hu.Value = hue;
            sa.Value = sat; 
            br.Value = brightness;

            Hue.Text = hue.ToString();
            Saturation.Text = sat.ToString();   
            Brightness.Text = brightness.ToString();    


        }

        public void applyNewColorByHsv()
        {
            var color = ColorFromHSV(hu.Value, sa.Value, br.Value);

            Brush brush = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
            Rec.Fill = brush;

            ColorReverse(hu.Value, sa.Value, br.Value);

            R.Value = color.R;  
            G.Value = color.G;  
            B.Value = color.B;  

            Red.Text = R.Value.ToString();
            Green.Text = G.Value.ToString();    
            Blue.Text = B.Value.ToString(); 
        }

        public static Color ColorFromHSV(double hue, double saturation, double brightness)
        {            
            var c1 = 2 * brightness - 1;
            if (c1 < 0) c1 *= -1;
            var c = (1 - c1) * saturation;

            var h1 = hue / 60;

            var x1 = (h1 % 2) - 1;
            if (x1 < 0) x1 *= -1;
            var x = c * (1 - x1);

            var m = brightness - (c / 2);

            var cm = (c + m) * 255;
            var xm = (x + m) * 255;
            var mm = m * 255;

            if (h1 <= 0)
                return Color.FromArgb(255, 255, 255, 255);
            else if (h1 > 0 && h1 < 1)
                return Color.FromArgb(255, Convert.ToByte(cm), Convert.ToByte(xm), Convert.ToByte(mm));
            else if (h1 >= 1 && h1 < 2)
                return Color.FromArgb(255, Convert.ToByte(xm), Convert.ToByte(cm), Convert.ToByte(mm));
            else if (h1 >= 2 && h1 < 3)
                return Color.FromArgb(255, Convert.ToByte(mm),  Convert.ToByte(cm), Convert.ToByte(xm));
            else if (h1 >= 3 && h1 < 4)
                return Color.FromArgb(255, Convert.ToByte(mm), Convert.ToByte(xm), Convert.ToByte(cm));
            else if (h1 >= 4 && h1 < 5)
                return Color.FromArgb(255, Convert.ToByte(xm), Convert.ToByte(mm) , Convert.ToByte(cm));
            else if (h1 >= 5 && h1 < 6)
                return Color.FromArgb(255, Convert.ToByte(cm), Convert.ToByte(mm), Convert.ToByte(xm));

            return Color.FromArgb(255, 0, 0, 0);
        }     

        public void ColorReverse(double hue, double saturation, double brightness)
        {
            if (brightness <= 0.4)
            {
                brightness = (float)(brightness + 0.5);
            }
            else if (brightness >= 0.6)
            {
                brightness = (float)(brightness - 0.40);
            }

            if(hue == 0)
            {
                if( R.Value != G.Value || R.Value != B.Value)
                    hue = 1;    
            }
            

            var color = ColorFromHSV(hue, saturation, brightness);

            Brush brush = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));

            System.Drawing.Color corDrawing = System.Drawing.Color.FromArgb(255, color.R, color.G, color.B);

            HueI.Text = corDrawing.GetHue().ToString();
            huI.Value = corDrawing.GetHue();

            SaturationI.Text = corDrawing.GetSaturation().ToString();
            saI.Value = corDrawing.GetSaturation(); 

            BrightnessI.Text = corDrawing.GetBrightness().ToString();
            brI.Value = corDrawing.GetBrightness();


            RecInvert.Fill = brush;
        }

        private void R_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var isCheck = check.IsChecked == null ? false : check.IsChecked == true ? true : false;
            if (!isCheck)
            {
                Red.Text = (sender as Slider).Value.ToString();
                applyNewColor();
            }
            
        }

        private void G_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var isCheck = check.IsChecked == null ? false : check.IsChecked == true ? true : false;
            if (!isCheck)
            {
                Green.Text = (sender as Slider).Value.ToString();
                applyNewColor();
            }
        }

        private void B_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var isCheck = check.IsChecked == null ? false : check.IsChecked == true ? true : false;
            if (!isCheck)
            {
                Blue.Text = (sender as Slider).Value.ToString();
                applyNewColor();
            }
        }

        private void hu_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var isCheck = check.IsChecked == null? false: check.IsChecked == true? true : false;  
            if (isCheck)
            {
                Hue.Text = (sender as Slider).Value.ToString();
                applyNewColorByHsv();
            }
            
        }

        private void br_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var isCheck = check.IsChecked == null ? false : check.IsChecked == true ? true : false;
            if (isCheck)
            {
                Brightness.Text = (sender as Slider).Value.ToString();
                applyNewColorByHsv();
            }
        }

        private void sa_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var isCheck = check.IsChecked == null? false: check.IsChecked == true? true : false;
            if (isCheck)
            {
                Saturation.Text = (sender as Slider).Value.ToString();
                applyNewColorByHsv();
            }
        }

        private void check_Checked(object sender, RoutedEventArgs e)
        {
            hu.IsEnabled = true;
            sa.IsEnabled = true;
            br.IsEnabled = true;

            R.IsEnabled = false;
            G.IsEnabled = false; 
            B.IsEnabled = false; 
        }

        private void check_Unchecked(object sender, RoutedEventArgs e)
        {
            hu.IsEnabled = false;
            sa.IsEnabled = false;
            br.IsEnabled = false;

            R.IsEnabled = true;
            G.IsEnabled = true;
            B.IsEnabled = true;
        }

        private void btnAplicar_Click(object sender, RoutedEventArgs e)
        {
            var color = (Color)ColorConverter.ConvertFromString(colorInput.Text);
            R.Value = color.R;
            G.Value = color.G;
            B.Value = color.B;
            applyNewColor();
        }
    }
}
