using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;
using RogueFeature.Backend;
using RogueFeature.Fontend;
using System.Windows.Input;

namespace RogueFeature
{
    public class GameHandle
    {
        public XMLLoader xmlLoader { private set; get; }
        private Core currentCore;
        private List<Core> lstCores = new List<Core>();
        public UCMainGrid ucMainGrid { private set; get; }

        public GameHandle()
        {
            xmlLoader = new XMLLoader();
            currentCore = null;
            ucMainGrid = new UCMainGrid();
            foreach (Map map in xmlLoader.lstMaps)
            {
                Core c = new Core(map);
                c.Computed += new Core.ComputeComplete(GameHandle_Computed);
                lstCores.Add(c);
            }
        }

        private void GameHandle_Computed(object sender, ComputedEventArgs e)
        {

        }

        public void GoToGrid(uint id)
        {
            this.currentCore = (from c in lstCores
                                   where c.Map.ID == id
                                   select c).FirstOrDefault();
            if (this.currentCore == null)
            {
                DebugLogging.DebugLogger.LogIt("$E Invalid Map ID: " + id);
                return;
            }

            ucMainGrid.SetupUIMainGrid(this.currentCore);
        }

        public Dictionary<uint, string> GetMaps()
        {
            Dictionary<uint, string> result = new Dictionary<uint,string>();
            foreach(Map map in xmlLoader.lstMaps)
            {
                result.Add(map.ID, map.Name);
            }
            return result;
        }

        public void Process(object sender, EventArgs e)
        {
            if (this.currentCore == null) return;

            //currentCore.Computed += new Core.ComputeComplete(currentCore_Computed);

        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            ucMainGrid.KeyDown(sender, e);
        }
    }
}
