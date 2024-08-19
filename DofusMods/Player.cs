using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DofusMods
{
    internal static class Player
    {
        private const string FightEntitiesName = "fightEntities";

        public static bool IsInFight()
        {
            GameObject fightEntities = GameObject.Find(FightEntitiesName);
            return fightEntities != null && fightEntities.activeSelf && fightEntities.transform.childCount > 0;
        }
    }
}
