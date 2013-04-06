using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Loader;

namespace RogueFeature
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        XMLLoader load = new XMLLoader("Loader/map.xml", "lvl1");
    }
}
