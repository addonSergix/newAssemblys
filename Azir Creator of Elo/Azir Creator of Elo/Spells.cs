
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
            //revisar entero
            if (target.Distance(HeroManager.Player) <= azir.Spells.Q.Range)
            {
                if (target.isRunningOfYou())
                {
                    var predpos = Prediction.GetPrediction(target, 0.5f);
                    if (azir.Hero.Distance(predpos.CastPosition) <= azir.Spells.Q.Range)
                    {
                        azir.Spells.Q.Cast((predpos.CastPosition));
                    }
                }
                else
                {
                    var predq = Q.GetPrediction(target);
                    if (predq.Hitchance >= HitChance.High)
                    {
                        Q.Cast(predq.CastPosition);
                    }
                }
            }
        }
    }
}
