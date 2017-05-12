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

namespace learnWPF.controlls
{
    /// <summary>
    /// Interaction logic for MazeControl.xaml
    /// </summary>
    public partial class MazeControl : UserControl
    {
        MazeRectangle[,] rectArray;
        int rows;
        int cols;
        //string order;
        public static  DependencyProperty UserControlProperty = DependencyProperty.Register("Order",
            typeof(string), typeof(MazeControl));
        public MazeControl()
        {
            InitializeComponent();
            int offset = -1;
            //String str = "#001*10001010101110101010000010111111101000001000111010101110001010001011111110100000000011111111111";
            //this.rows = rows;
            //this.cols = cols;
            rectArray = new MazeRectangle[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    MazeRectangle myRectangle = new MazeRectangle(new Rectangle());
                    Rectangle currentRec = myRectangle.MazeRect;
                    currentRec.Height = 30;
                    currentRec.Width = 30;
                    rectArray[i, j] = myRectangle;
                    myCanvas.Children.Add(rectArray[i, j].MazeRect);
                    Canvas.SetLeft(rectArray[i, j].MazeRect, (i * 30));
                    Canvas.SetTop(rectArray[i, j].MazeRect, (j * 30));
                }

            }
        }
        public int Rows
        {
            get
            {
                return this.rows;
            }
            set
            {
                this.rows = value;
            }
        }
        public int Cols
        {
            get
            {
                return this.cols;
            }
            set
            {
                this.cols = value;
            }
        }

        public string Order
        {
            get
            {
                string s = "";
                foreach (MazeRectangle l in rectArray)
                    s += l.State;
                return s;
            }
            set
            {
                int i=0;
                string s = value;
                foreach (MazeRectangle l in rectArray)
                {
                    i++;
                    l.State = s[i];
                    if (l.State == '0')
                    {
                        l.MazeRect.Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                    }
                    else if (l.State == '1')
                    {
                        l.MazeRect.Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                    }
                    else if (l.State == '*')
                    {
                        l.MazeRect.Fill = new SolidColorBrush(System.Windows.Media.Colors.Yellow);
                    }else
                    {
                        l.MazeRect.Fill = new SolidColorBrush(System.Windows.Media.Colors.Blue);
                    }
                    //if (s[i] == ' ')
                    //    tiles[i].Background = Brushes.Gray;
                    //else
                    //    tiles[i].Background = Brushes.White;
                }
            }
        }
    }

    public class MazeRectangle
    {
        char state;
        Rectangle rectangle;
        public MazeRectangle(Rectangle rectangle)
        {
            //this.state = state;
            this.rectangle = rectangle;
        }
        public Rectangle MazeRect
        {
            get
            {
                return rectangle;
            }

        }
        public char State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }


    }
}

