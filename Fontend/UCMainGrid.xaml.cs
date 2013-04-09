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
using RogueFeature.Backend;
using RogueFeature.Backend.Units;

namespace RogueFeature.Fontend
{
    /// <summary>
    /// Interaction logic for UCMainGrid.xaml
    /// </summary>
    public partial class UCMainGrid : UserControl
    {
        private Image imgUser;
        private const uint BlockSize = 52;
        public Core currentCore { private set; get; }
        public UCMainGrid()
        {
            InitializeComponent();
        }

        private void SetupImages()
        {
            imgUser = new Image();
            imgUser.Source = null;
        }

        public void SetupUIMainGrid(Core core)
        {
            this.currentCore = core;
            this.CreateGrid(core.Map.rows, core.Map.columns);
            int xSize = core.Map.Points.GetUpperBound(0);
	        int ySize = core.Map.Points.GetUpperBound(1);
            for(int x = 0; x < xSize; ++x)
            {
                for(int y = 0; y < ySize; ++y)
                {
                    if (core.Map.Points[x, y] != null)
                    {
                        Image m = new Image();
                        m.Tag = core.Map.Points[x, y];
                        m.Source = ImageLib.GrabImage(core.Map.Points[x, y].ImgID);
       
                        m.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                        m.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                        gridMain.Children.Add(m);
                        Grid.SetColumn(m, y);
                        Grid.SetRow(m, x);

                        StackPanel s = new StackPanel();
                        s.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                        s.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                        s.Background = Brushes.Red;
                        Grid.SetColumn(s, y);
                        Grid.SetRow(s, x);
                      //  gridMain.Children.Add(s);
                        //Canvas.SetZIndex(m, 1);

                        foreach (Unit u in core.Map.Points[x, y].Units)
                        {
                            Image m2 = new Image();
                            m2.Tag = core.Map.Points[x, y];
                            m2.Source = ImageLib.GrabImage(u.imgPath);
                            Grid.SetColumn(m2, y);
                            Grid.SetRow(m2, x);
                            gridMain.Children.Add(m2);
                            Canvas.SetZIndex(m2, 1);
                        }
                    }
                }
            }
        }

        private void CreateGrid(uint rowSize, uint colSize)
        {
            gridMain.Background = Brushes.Pink;
            gridMain.RowDefinitions.Clear();
            gridMain.ColumnDefinitions.Clear();
            gridMain.Width = BlockSize * colSize;
            gridMain.Height = BlockSize * rowSize;
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

        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (this.currentCore == null) return;

            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    this.currentCore.PlayerMove(Direction.DOWN);
                    break;
                case Key.Up:
                case Key.W:
                    this.currentCore.PlayerMove(Direction.UP);
                    break;
                case Key.D:
                case Key.Right:
                    this.currentCore.PlayerMove(Direction.RIGHT);
                    break;
                case Key.Left:
                case Key.A:
                    this.currentCore.PlayerMove(Direction.LEFT);
                    break;
            }
        }
    }
}
