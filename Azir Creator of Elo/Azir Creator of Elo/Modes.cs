using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azir_Creator_of_Elo
{
    class Modes
    {
        public virtual void Update(AzirMain azir)
        {


            switch (azir._menu.Orb.ActiveMode)
            {
                case LeagueSharp.Common.Orbwalking.OrbwalkingMode.Combo:
                    Combo(azir);
                    break;
                case LeagueSharp.Common.Orbwalking.OrbwalkingMode.Mixed:
                    Harash(azir);
                    break;
                case LeagueSharp.Common.Orbwalking.OrbwalkingMode.LaneClear:
                   Laneclear(azir);
                    Jungleclear(azir);
                    break;
            }
      

        }
        public virtual void Jungleclear(AzirMain azir)
        {

        }

        public virtual void Laneclear(AzirMain azir)
        {
        
        }

        public virtual void Harash(AzirMain azir)
        {
        
        }

        public virtual void Combo(AzirMain azir)
        {
          
      

        }
    }
}
