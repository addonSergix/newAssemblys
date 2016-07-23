using System.Linq;
using Azir_Creator_of_Elo;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace Azir_Free_elo_Machine
{

    class JumpLogic
    {
        AzirMain azir;
        Obj_AI_Minion soldier;

        public JumpLogic(AzirMain azir)
        {
            this.azir = azir;
        }
        private static Vector3 startPos = new Vector3();

        public bool checkSpells()
        {
            if(azir.Spells.Q.IsReady())
                if(azir.Spells.E.IsReady())
                    if (azir.Spells.W.IsReady())
                        return true;
            return false;
        }
        public void updateLogic(Vector3 position)
        {
            var mx =  azir.Menu.GetMenu.Item("FMJ").GetValue<bool>();
            var closet = getClosestSolider(position, azir);
         
            if (checkSpells()&&(closet==null))//&&R.IsReady())
            {

                    azir.Spells.W.Cast(HeroManager.Player.Position.Extend(position, 450));
        
               

            }
            if (closet == null)
                return;
            if (azir.Spells.E.IsReady())
            {
                startPos =azir.Hero.ServerPosition;
                azir.Spells.E.CastOnUnit(closet);
            }
            var playDisToSoli = azir.Hero.Distance(closet);
            if (azir.Hero.IsDashing() && playDisToSoli < 95)
            {
                if (mx)
                {
                    if (azir.Spells.Q.IsReady() )
                        azir.Spells.Q.Cast(azir.Hero.ServerPosition.Extend(position,1234));
                }
                else
                {
                    if (azir.Spells.Q.IsReady() && position.Distance(azir.Hero.ServerPosition)>=200)
                        azir.Spells.Q.Cast(position);
                }
            }

        }
        public static Obj_AI_Minion getClosestSolider(Vector3 pos,AzirMain azir)
        {
            return azir.soldierManager.Soldiers.Where(sol => !sol.IsDead).OrderBy(sol => sol.Distance(pos, true) - ((sol.IsMoving) ? 500 : 0)).FirstOrDefault();
        }
        public void fleeTopos(Vector3 position)
        {
            if (azir.Spells.W.IsReady() && azir.Spells.Q.IsReady() && azir.Spells.E.IsReady())//&&R.IsReady())
            {
                azir.Spells.W.Cast(HeroManager.Player.Position.Extend(position, 450));
                Utility.DelayAction.Add(Game.Ping + 150, () => azir.Spells.E.Cast(azir.soldierManager.Soldiers[azir.soldierManager.Soldiers.Count - 1].ServerPosition));
                Utility.DelayAction.Add(Game.Ping + 400, () => fleeq(position));
            }
        }
        public void fleeq(Vector3 position)
        {
            if (Vector2.Distance(HeroManager.Player.ServerPosition.To2D(), position.To2D()) < azir.Spells.Q.Range)
            {
                azir.Spells.Q.Cast(position);
            }
            else
            {
                azir.Spells.Q.Cast(HeroManager.Player.Position.Extend(position, 1150));
            }
        }

        public void insec(Obj_AI_Hero target)
        {
            // si esta en rango tira la r
            if (azir.Hero.Distance(target) <= azir.Spells.R.Range)
            {
           
                    var tower = ObjectManager.Get<Obj_AI_Turret>().FirstOrDefault(it => it.IsAlly && it.IsValidTarget(1000));

                    if (tower != null)
                    {
                        if (azir.Spells.R.Cast(tower.ServerPosition)) return;
                    }

                    if (azir.Spells.R.Cast(Game.CursorPos)) return;
                


            }
            else {
                // si no hace flee
                var pos = Game.CursorPos.Extend(target.Position, Game.CursorPos.Distance(target.Position)  +100);
                if (pos.Distance(azir.Hero.ServerPosition) <= 1150 + 350)
                {
                    fleeTopos(pos);
                }
                    }
    
        }
    }
}
