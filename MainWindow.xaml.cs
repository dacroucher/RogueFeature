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
using System.Windows.Threading;

namespace RogueFeature
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameHandle handle;
        private DispatcherTimer tmrTime;
        public MainWindow()
        {
            InitializeComponent();
            handle = new GameHandle();
            spMain.Children.Add(handle.ucMainGrid);
            Dictionary<uint, string> lstMaps = handle.GetMaps();
            foreach (KeyValuePair<uint, string> map in lstMaps)
            {
                Button b = new Button();
                b.Tag = map.Key;
                b.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                b.Content = map.Value;
                b.Margin = new Thickness(5, 5, 0, 0);
                b.Click += new RoutedEventHandler(b_Click);
                spLevel.Children.Add(b);
            }
            this.CreateTimer();
        }

        private void CreateTimer()
        {
            //  DispatcherTimer setup
            tmrTime = new DispatcherTimer();
            tmrTime.Tick += new EventHandler(dispatcherTimer_Tick);
            tmrTime.Interval = new TimeSpan(0, 0, 0, 0, 20);
            tmrTime.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            handle.Process(sender, e);
            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void b_Click(object sender, RoutedEventArgs e)
        {
            handle.GoToGrid(uint.Parse((sender as Button).Tag.ToString()));
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            handle.KeyDown(sender, e);
        }


    }
}
