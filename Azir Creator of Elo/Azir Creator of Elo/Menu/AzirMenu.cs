﻿using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azir_Creator_of_Elo
{
    class AzirMenu : Menu
    {
        private LeagueSharp.Common.Menu _drawSettingsMenu, _jumpMenu,_comboMenu, _harashMenu, _laneClearMenu, _JungleClearMenu;
        public AzirMenu(String name,AzirMain  azir) : base(name)
        {
            loadMenu(azir);
            closeMenu();
        }
        public override void loadMenu(AzirMain azir)
        {
            base.loadMenu(azir);
            loadLaneClearMenu();
            loadHarashMenu();
            loadComboMenu();
            loadJungleClearMenu();
            loadDrawings();
            loadJumps();
        }

        public override void closeMenu()
        {
            // add menus

            base.closeMenu();
            base.GetMenu.AddSubMenu(_comboMenu);
            base.GetMenu.AddSubMenu(_harashMenu);
            base.GetMenu.AddSubMenu(_jumpMenu);
            base.GetMenu.AddSubMenu(_laneClearMenu);
            base.GetMenu.AddSubMenu(_JungleClearMenu);
            base.GetMenu.AddSubMenu(_drawSettingsMenu);
            base.GetMenu.AddToMainMenu();

        }
        public void loadDrawings()
        {
            _drawSettingsMenu = new LeagueSharp.Common.Menu("Drawings", "Draw Settings");
            {
                _drawSettingsMenu.AddItem(new MenuItem("dsl", "Draw Soldier Line").SetValue(true));
                _drawSettingsMenu.AddItem(new MenuItem("dcr", "Draw Control range").SetValue(true));
                _drawSettingsMenu.AddItem(new MenuItem("dfr", "Draw Flee range").SetValue(true));
            }
        }
        public  void loadComboMenu()
        {
            _comboMenu = new LeagueSharp.Common.Menu("Combo Menu", "Combo Menu");
            {
                _comboMenu.AddItem(new MenuItem("SoldiersToQ", "Soldiers to Q").SetValue(new Slider(1, 1, 3)));
                _comboMenu.AddItem(new MenuItem("CQ", "Use Q").SetValue(true));
                _comboMenu.AddItem(new MenuItem("CW", "Use W").SetValue(true));
                _comboMenu.AddItem(new MenuItem("CR", "Use R killeable").SetValue(true));
            }
        }
        public void loadLaneClearMenu()
        {
            _laneClearMenu = new LeagueSharp.Common.Menu("Laneclear Menu", "Laneclear Menu");
            {
                _laneClearMenu.AddItem(new MenuItem("LQ", "Use Q").SetValue(true));
                _laneClearMenu.AddItem(new MenuItem("LW", "Use W").SetValue(true));
                _laneClearMenu.AddItem(new MenuItem("LWM", "Minions at W range to cast").SetValue(new Slider(3, 1, 6)));
                _laneClearMenu.AddItem(new MenuItem("LQM", "Soldiers to Q ").SetValue(new Slider(1, 1, 3)));

            }
        }
        public void loadJungleClearMenu()
        {
            _JungleClearMenu = new LeagueSharp.Common.Menu("JungleClear Menu", "JungleClear  Menu");
            {
                _JungleClearMenu.AddItem(new MenuItem("JW", "Use W").SetValue(true));
                _JungleClearMenu.AddItem(new MenuItem("JQ", "Use W").SetValue(true));
            }
        }
        public void loadHarashMenu()
        {
            _harashMenu = new LeagueSharp.Common.Menu("Harash Menu", "Harash Menu");
            {
                _harashMenu.AddItem(new MenuItem("hSoldiersToQ", "Soldiers to Q").SetValue(new Slider(1, 1, 3)));
                _harashMenu.AddItem(new MenuItem("HQ", "Use Q").SetValue(true));
                _harashMenu.AddItem(new MenuItem("HW", "Use W").SetValue(true));
                _harashMenu.AddItem(new MenuItem("HW2", "Save on 1 w for flee").SetValue(true));
            }
        }
        public void loadJumps()
        {
            _jumpMenu = new LeagueSharp.Common.Menu("Key Menu", "Key Menu");
            {
              _jumpMenu.AddItem(new MenuItem("fleekey", "Jump key").SetValue(new KeyBind('Z', KeyBindType.Press)));
              _jumpMenu.AddItem(new MenuItem("inseckey", "Insec key").SetValue(new KeyBind('T', KeyBindType.Press)));
              _jumpMenu.AddItem(new MenuItem("FMJ", "Max Range Jump Only").SetTooltip("Cast only jump to max range at flee").SetValue(true));
            }
        }
    }
    }
