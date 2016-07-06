
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

    

     
        public void castQ(AzirMain azir, Obj_AI_Hero target, bool useQ, int nSoldiersToQ)
        {
            if (target.isRunningOfYou())
            {
                if (azir.Spells.Q.IsKillable(target))
                {
                    var pred = azir.Spells.Q.GetPrediction(target);
                    if (pred.Hitchance >= HitChance.High)
                    {
                        if (useQ)
                            azir.Spells.Q.Cast(pred.CastPosition.Extend(target.ServerPosition,100));
                    }
                }
            }
            if (!azir.soldierManager.SoldiersAttacking(azir) && azir.soldierManager.ActiveSoldiers.Count >= nSoldiersToQ)
            {
                if (target.isRunningOfYou())
                {
                    var pos = Prediction.GetPrediction(target, 0.6f).UnitPosition;
                    if (pos.Distance(HeroManager.Player.ServerPosition) <= azir.Spells.Q.Range)
                        if (useQ)
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
