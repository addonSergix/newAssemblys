using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azir_Free_elo_Machine;

namespace Azir_Creator_of_Elo
{
    class Menu
    {
        private AzirWalker walker;
        private LeagueSharp.Common.Menu menu;
        public LeagueSharp.Common.Menu GetMenu
        {
            get { return menu; }
        }
        private string _menuName;
        private Orbwalking.Orbwalker orb;
        public Orbwalking.Orbwalker Orb
        {
            get { return orb; }
        }
        LeagueSharp.Common.Menu _orbWalkerMenu, _targetSelectorMenu;
        public Menu(string menuName)
        {
            this._menuName = menuName;
        }
        public virtual void loadMenu(AzirMain azir)
        {
            menu = new LeagueSharp.Common.Menu(_menuName, _menuName, true);
        _orbWalkerMenu = new LeagueSharp.Common.Menu("Orbwalker", "Orbwalker");
            orb = new AzirWalker(_orbWalkerMenu, azir);
            _targetSelectorMenu = new LeagueSharp.Common.Menu("TargetSelector", "TargetSelector");
        }
        public virtual void closeMenu()
        {
            TargetSelector.AddToMenu(_targetSelectorMenu);
            menu.AddSubMenu(_orbWalkerMenu);        //ORBWALKER
            menu.AddSubMenu(_targetSelectorMenu);
          
        }
        public virtual void loadComboMenu()
        {

        }
    }
}
