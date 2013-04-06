using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RogueFeature
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.CreateGrid();
        }
        Grid g;
        StackPanel img;
        private void CreateGrid()
        {
            img = new StackPanel();
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            img.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            img.Background = Brushes.Red;

            g= new Grid();
            g.Background = Brushes.Black;
            
            g.Children.Add(img);
            winMain.Content = g;
            for (int i = 0; i < 20; ++i)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(20);
                g.RowDefinitions.Add(row);

                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(20);
                g.ColumnDefinitions.Add(col);
                
            }
        }

        private int x = 0;
        private int y = 0;
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            int tempX = x;
            int tempY = y;

            switch (e.Key)
            {
                case Key.Down:
                    tempY++;
                    break;
                case Key.Up:
                    tempY--;
                    break;
                case Key.Right:
                    tempX++;
                    break;
                case Key.Left:
                    tempX--;
                    break;
            }

            if (tempX < g.RowDefinitions.Count && tempX >= 0)
            {
                x = tempX;
            }

            if (tempY < g.ColumnDefinitions.Count && tempY >= 0)
            {
                y = tempY;
            }
            Grid.SetColumn(this.img, x);
            Grid.SetRow(this.img, y);
        }
    }
}
