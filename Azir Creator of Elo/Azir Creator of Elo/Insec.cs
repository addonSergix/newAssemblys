using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using LeagueSharp.Common.Data;
using SharpDX;
using System.Text.RegularExpressions;
using Color = System.Drawing.Color;
using Azir_Creator_of_Elo;

namespace Azir_Free_elo_Machine
{
    class Insec
    {
        Azir_Creator_of_Elo.AzirMain azir;
        public  Insec(AzirMain azir)
        {
            this.azir = azir;
            Game.OnUpdate += Game_OnUpdate;
            Game.OnWndProc += Game_OnWndProc;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            if (!azir.Menu.GetMenu.Item("inseckey").GetValue<KeyBind>().Active)
            {

                return;
            }
            var target = TargetSelector.GetSelectedTarget();
            /*     var posWs = GeoAndExten.GetWsPosition(target.Position.To2D()).Where(x => x != null);
                 foreach (var posW in posWs)
                 {

                 }*/
            if (Clickposition == new Vector3(0, 0, 0))
            {
                if (target != null)
                {
                    if (target.IsVisible && target.IsValid)
                    {
                        var pos = target.ServerPosition.Extend(Game.CursorPos, -300);
                        Render.Circle.DrawCircle(pos, 100, System.Drawing.Color.GreenYellow);
                    }
                }
            }
            else
            {
                Render.Circle.DrawCircle(Clickposition, 100, System.Drawing.Color.GreenYellow);
            }

        }
        Vector3 Clickposition;
        private void Game_OnWndProc(WndEventArgs args)
        {
     
       /*     if (args.Msg == (uint)WindowsMessages.KEY)
            {
                if (Clickposition == new Vector3(0,0,0))
                    Clickposition = Game.CursorPos;
                else
                    Clickposition = new Vector3(0,0,0);
            }*/

    }
        Obj_AI_Minion soldier;
        private void Game_OnUpdate(EventArgs args)
        {
            var insecPoint = new Vector3(0, 2, 3);
            if (Clickposition == new Vector3(0, 0, 0))
                insecPoint = Game.CursorPos;
            else
                insecPoint = Clickposition;
     
            if (!azir.Menu.GetMenu.Item("inseckey").GetValue<KeyBind>().Active)
            {
                soldier = null;
                return;
            }
            azir.Orbwalk(Game.CursorPos);
 
            if (!insecPoint.IsValid())
                return;
            var target = TargetSelector.GetSelectedTarget();
            if (!target.IsValidTarget() || target.IsZombie)
                return;
            if (azir.Hero.Distance(target) <=    azir.Spells.R.Range&&!azir.Hero.IsDashing())
            {
                if (Clickposition == new Vector3(0, 0, 0))
                {
                    var tower = ObjectManager.Get<Obj_AI_Turret>().FirstOrDefault(it => it.IsAlly && it.IsValidTarget(1000));

                    if (tower != null)
                    {
                        if (azir.Spells.R.Cast(tower.ServerPosition)) return;
                    }

                    if (azir.Spells.R.Cast(Game.CursorPos)) return;
                }
                else
                {
                    azir.Spells.R.Cast(Clickposition);
                }



            }

            var pos = target.ServerPosition.Extend(Game.CursorPos, -300);
            if (pos.Distance(azir.Hero.ServerPosition) <= 1100)
            {

                if (soldier == null)
                    soldier = azir.soldierManager.ActiveSoldiers
                .Where(x => x.Distance(pos) <= 900)
                .OrderBy(x => x.Position.Distance(target.Position)).FirstOrDefault();
                if (soldier == null)
                {
                    castWOnAngle(HeroManager.Player.ServerPosition.To2D(), target.ServerPosition.To2D(), 45);
                    return;
                }
                if (soldier != null)
                {
                    azir.Spells.E.Cast(soldier.Position);
                }
                if (!azir.Spells.E.IsReady())
                {
                    if (azir.Hero.Distance(soldier.ServerPosition) <= 150)
                    {
                        azir.Spells.Q.Cast(pos);
                    }

                }

            }
  

        }

        private void castWOnAngle(Vector2 playerPos, Vector2 targetPos, float ag)
        {
            var posW = playerPos.Extend(targetPos, azir.Spells.W.Range);
            if(!RotatePoint(posW, playerPos, ag).IsWall())
            azir.Spells.W.Cast(RotatePoint(posW, playerPos, ag));
        }
        public  Vector2 RotatePoint( Vector2 pointToRotate, Vector2 centerPoint, float angleInRadians)
        {
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Vector2
            {
                X =
                    (float)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (float)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }
    }
}
