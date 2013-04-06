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
using Loader;
using Backend;

namespace RogueFeature.Fontend
{
    /// <summary>
    /// Interaction logic for UCMainGrid.xaml
    /// </summary>
    public partial class UCMainGrid : UserControl
    {
        private Image imgUser;
        private const uint BlockSize = 52;

        public UCMainGrid(Map map)
        {
            InitializeComponent();
            this.CreateGrid(map.rows, map.columns);
            /*
             foreach(Grid grid in loader.lstGrids)
             {
             * this.CreateGrid(grid.rowSize, grid.colSize, grid.Objects);
             * 
             }*/
            //
        }

        private void SetupImages()
        {
            imgUser = new Image();
            imgUser.Source = null;
        }

        private void 

        private void CreateGrid(uint rowSize, uint colSize)
        {
           /* img = new StackPanel();
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            img.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            img.Background = Brushes.Red;
            gridMain.Children.Add(img);*/
            for (int i = 0; i < rowSize; ++i)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(BlockSize);
                gridMain.RowDefinitions.Add(row);
            }

            for (int i = 0; i < colSize; ++i)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(BlockSize);
                gridMain.ColumnDefinitions.Add(col);
            }
        }

        private void gridMain_KeyDown(object sender, KeyEventArgs e)
        {
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
