﻿using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azir_Creator_of_Elo
{
    static class Extensions
    {
        public static bool IsRunningOfYou(this Obj_AI_Hero target)
        {
            var pos = Prediction.GetPrediction(target, 0.5f).UnitPosition;
            return Vector2.Distance(HeroManager.Player.ServerPosition.To2D(), pos.To2D()) > Vector2.Distance(HeroManager.Player.ServerPosition.To2D(), target.ServerPosition.To2D());
        }
    }
}
