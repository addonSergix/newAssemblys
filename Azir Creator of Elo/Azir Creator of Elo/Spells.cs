
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace Azir_Creator_of_Elo
{
    class Spells
    {
        private Spell _q, _w, _e, _r;
        public Spell Q { get { return _q; } }
        public Spell W { get { return _w; } }
        public Spell E { get { return _e; } }
        public Spell R { get { return _r; } }
        public Spells()
        {
            _q = new Spell(SpellSlot.Q, 825);


            _w = new Spell(SpellSlot.W, 450);
            _e = new Spell(SpellSlot.E, 1250);
            _r = new Spell(SpellSlot.R, 450);

            _q.SetSkillshot(0, 70, 1600, false, SkillshotType.SkillshotCircle);
            _e.SetSkillshot(0, 100, 1700, false, SkillshotType.SkillshotLine);
            _r.SetSkillshot(0.5f, 0, 1400, false, SkillshotType.SkillshotLine);
            //  ignite = ObjectManager.Player.GetSpellSlot("SummonerDot");
        }

        internal void flyToInsec(Vector3 pos, AzirMain azir, Obj_AI_Hero ts)
        {
            throw new NotImplementedException();
        }
        
        public void flyToinsec(Vector3 position, AzirMain azir,Obj_AI_Hero ts)
        {
            if (W.IsReady() && Q.IsReady() && E.IsReady())//&&R.IsReady())
            {
                W.Cast(HeroManager.Player.Position.Extend(position, 450));
                Utility.DelayAction.Add(Game.Ping + 150, () => E.Cast(azir.soldierManager.Soldiers[azir.soldierManager.Soldiers.Count - 1].ServerPosition));
                Utility.DelayAction.Add(Game.Ping + 200, () => Q.Cast(HeroManager.Player.Position.Extend(position, 1150)));
                Utility.DelayAction.Add(Game.Ping + 400, () => azir.Hero.IssueOrder(GameObjectOrder.MoveTo,position.Extend(HeroManager.Player.Position,300)));
               Utility.DelayAction.Add(Game.Ping + 800, () => azir.Spells.R.Cast(position.Extend(ts.Position, 300)));

            }
        }
        public void castQ(AzirMain azir,Obj_AI_Hero target,bool useQ,int nSoldiersToQ)
        {
            if (!azir.soldierManager.SoldiersAttacking(azir) && azir.soldierManager.ActiveSoldiers.Count >= nSoldiersToQ)
            {
                if (target.isRunningOfYou())
                {
                    var pos = Prediction.GetPrediction(target, 1f).UnitPosition;
                    if (pos.Distance(HeroManager.Player.ServerPosition) <= azir.Spells.Q.Range)
                        if(useQ)
                        azir.Spells.Q.Cast(pos);
                }
                else
                {
                    var pred = azir.Spells.Q.GetPrediction(target);
                    if (pred.Hitchance >= HitChance.High)
                    {
                        if (useQ)
                            azir.Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
        }

    }
}
