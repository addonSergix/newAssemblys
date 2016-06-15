using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyzeAssembly
{
    class Menu
    {
        private LeagueSharp.Common.Menu _drawSettingsMenu, _laneclearMenu, _jungleclearMenu, _harrashMenu;
      public LeagueSharp.Common.Menu menu;
        public Orbwalking.Orbwalker orb;
        public Menu()
        {
            loadMenu();
        }
        public void loadMenu()
        {

            menu = new LeagueSharp.Common.Menu("Ryze", "Ryze", true);
            var orbWalkerMenu = new LeagueSharp.Common.Menu("Orbwalker", "Orbwalker");
      orb = new Orbwalking.Orbwalker(orbWalkerMenu);
            var TargetSelectorMenu = new LeagueSharp.Common.Menu("TargetSelector", "TargetSelector");
            loadLaneClear();
            loadDrawings();
          //  loadJungleClear();
          //  loadHarassh();

            TargetSelector.AddToMenu(TargetSelectorMenu);
            menu.AddSubMenu(orbWalkerMenu);        //ORBWALKER
            menu.AddSubMenu(TargetSelectorMenu);   //TS
                                                   // menu.AddSubMenu(itemMenu);
            //menu.AddSubMenu(_harrashMenu);
           menu.AddSubMenu(_laneclearMenu);        //LANECLEAR
           // menu.AddSubMenu(_jungleclearMenu);      //JUNGLECLEAR
            menu.AddSubMenu(_drawSettingsMenu);     //DRAWS
            menu.AddToMainMenu();
        }
        public void loadHarassh()
        {

            _harrashMenu = new LeagueSharp.Common.Menu("Harrash", "Harrash");
            {
                _harrashMenu.AddItem(new MenuItem("QH", "Use Q in Harrash").SetValue(true));
            }
        }
    
        public void loadLaneClear()
        {
           _laneclearMenu = new LeagueSharp.Common.Menu("Laneclear", "Laneclear");
            {
                _laneclearMenu.AddItem(new MenuItem("QL", "Use Q in Laneclear").SetValue(true));
                _laneclearMenu.AddItem(new MenuItem("WL", "Use W in Laneclear").SetValue(true));
               _laneclearMenu.AddItem(new MenuItem("EL", "Use E in Laneclear").SetValue(true));
                _laneclearMenu.AddItem(new MenuItem("RL", "Use R in Laneclear").SetValue(true));
            }
      
        }
        public void loadJungleClear()
        {
         _jungleclearMenu = new LeagueSharp.Common.Menu("Jungleclear", "Jungleclear");
            {
                _jungleclearMenu.AddItem(new MenuItem("QJ", "Use Q in JungleClear").SetValue(true));
                _jungleclearMenu.AddItem(new MenuItem("WJ", "Use W in JungleClear").SetValue(true));
                _jungleclearMenu.AddItem(new MenuItem("EJ", "Use E in JungleClear").SetValue(true));
                _jungleclearMenu.AddItem(new MenuItem("RJ", "Use R in JungleClear").SetValue(true));

            }
        }
        public void loadDrawings()
        {
          _drawSettingsMenu = new LeagueSharp.Common.Menu("Draw Settings", "Draw Settings");
            {
                _drawSettingsMenu.AddItem(new MenuItem("Draw Q Range", "Draw Q Range").SetValue(true));
                _drawSettingsMenu.AddItem(new MenuItem("Draw W Range", "Draw W Range").SetValue(true));
                _drawSettingsMenu.AddItem(new MenuItem("Draw E Range", "Draw E Range").SetValue(true));
             
            }
        }

    }
}
