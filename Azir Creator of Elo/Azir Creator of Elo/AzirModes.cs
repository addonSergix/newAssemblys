using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azir_Creator_of_Elo
{
    class AzirModes : Modes
    {
        public JumpLogic jump;
        public AzirModes(AzirMain azir)
        {
            jump = new JumpLogic(azir);
        }
        public override void Update(AzirMain azir)
        {

            base.Update(azir);
       
      

            if (azir.Menu.GetMenu.Item("fleekey").GetValue<KeyBind>().Active)
            {
                Jump(azir);
            }

            if (azir.Menu.GetMenu.Item("inseckey").GetValue<KeyBind>().Active)
            {
                Insec(azir);
            }
        }
        public void Insec(AzirMain azir)
        {
            var ts = TargetSelector.GetTarget(azir.Spells.Q.Range, TargetSelector.DamageType.Magical);
            if (ts != null)
            {

                if(azir.Spells.R.IsReady())
                jump.insec(ts);
              
                    }
        }
        public  void Jump(AzirMain azir)
        {
            jump.updateLogic(Game.CursorPos);

        }

        public override void Harash(AzirMain azir)
        {

            var wCount = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Ammo;
            var useQ = azir.Menu.GetMenu.Item("HQ").GetValue<bool>();
            var useW = azir.Menu.GetMenu.Item("HW").GetValue<bool>();
            var savew = azir.Menu.GetMenu.Item("HW2").GetValue<bool>();
            var nSoldiersToQ = azir.Menu.GetMenu.Item("hSoldiersToQ").GetValue<Slider>().Value;
            base.Harash(azir);
            var target = TargetSelector.GetTarget(900, TargetSelector.DamageType.Magical);
            if (target != null)
            {
 
         
                if (target.Distance(azir.Hero.ServerPosition) < 450)
                {
                    var pred = azir.Spells.W.GetPrediction(target);
                    if (pred.Hitchance >= HitChance.Medium)
                    {
                        if (savew && (wCount == 0))
                        {

                        }
                        else
                        {
                            if (useW)
                                azir.Spells.W.Cast(pred.CastPosition);
                        }
                    }
                }
                else
                {
                    if (azir.Spells.Q.Level > 0 && azir.Spells.Q.IsReady())
                        if((!savew &&(savew&& (wCount > 0))))
                        if (useW)
                            azir.Spells.W.Cast(azir.Hero.Position.Extend(target.ServerPosition, 450));
                }
                azir.Spells.castQ(azir, target, useQ, nSoldiersToQ);
             
            }
        }
        public override void Laneclear(AzirMain azir)
        {
            var useQ = azir.Menu.GetMenu.Item("LQ").GetValue<bool>();
            var useW = azir.Menu.GetMenu.Item("LW").GetValue<bool>();
            base.Laneclear(azir);
            var minion = MinionManager.GetMinions(azir.Spells.Q.Range, MinionTypes.All, MinionTeam.Enemy, MinionOrderTypes.MaxHealth).FirstOrDefault();
            if (minion != null)
            {
                if (azir.Spells.W.IsInRange(minion))
                {
                    var pred = azir.Spells.W.GetPrediction(minion);
                    if (pred.Hitchance >= HitChance.High)
                    {
                        if (useW)
                            azir.Spells.W.Cast(pred.CastPosition);
                    }
                    if (azir.soldierManager.SoldiersAttacking(azir) == false && azir.soldierManager.ActiveSoldiers.Count > 0)
                    {
                        pred = azir.Spells.Q.GetPrediction(minion);
                        if (pred.Hitchance >= HitChance.High)
                        {
                            if (useQ)
                                azir.Spells.Q.Cast(pred.CastPosition);
                        }
                    }
                }
            }
        }
        public override void Jungleclear(AzirMain azir)
        {
            var useW = azir.Menu.GetMenu.Item("JW").GetValue<bool>();
            base.Jungleclear(azir);
            var minion = MinionManager.GetMinions(azir.Spells.Q.Range, MinionTypes.All, MinionTeam.Neutral, MinionOrderTypes.MaxHealth).FirstOrDefault();
            if (minion != null)
            {
                if (azir.Spells.W.IsInRange(minion))
                {
                    var pred = azir.Spells.W.GetPrediction(minion);
                    if (pred.Hitchance >= HitChance.High)
                    {
                        if (useW)
                            azir.Spells.W.Cast(pred.CastPosition);
                    }
             
                    
                }
            }
        }

        public override void Combo(AzirMain azir)
        {

            var useQ= azir.Menu.GetMenu.Item("CQ").GetValue<bool>();
            var useW = azir.Menu.GetMenu.Item("CW").GetValue<bool>();
            var nSoldiersToQ = azir.Menu.GetMenu.Item("SoldiersToQ").GetValue<Slider>().Value;
            base.Combo(azir);
            var target  = TargetSelector.GetTarget(900, TargetSelector.DamageType.Magical);
            if (target != null)
            { 
                if (target.Distance(azir.Hero.ServerPosition) < 450)
                {
                    if (target.isRunningOfYou())
                    {
                        var pos = Prediction.GetPrediction(target, 0.8f).UnitPosition;
                        azir.Spells.W.Cast(pos);
                    }
                    else
                    {
                        var pred = azir.Spells.W.GetPrediction(target);
                        if (pred.Hitchance >= HitChance.Medium)
                        {
                            if (useW)
                                azir.Spells.W.Cast(pred.CastPosition);
                        }
                    }
                }
                else
                {
                    if(azir.Spells.Q.Level>0&& azir.Spells.Q.IsReady())
                        if(useW)
                    azir.Spells.W.Cast(azir.Hero.Position.Extend(target.ServerPosition, 450));
                }
                azir.Spells.castQ(azir,target,useQ,nSoldiersToQ);
             
            }
   
        }
    }
}
