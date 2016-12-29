using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate void GameEndEventHandler(object sender, Winner w);
    public delegate void DealDamageEventHandler(object sender, int damage);
    public delegate void NewRoundEventHandler(object sender, short roundNumber);
    public delegate void TempleDestroyedEventHandler(object sender, Transform transform);
    public delegate void PlayerFallEventHandler(object sender, GameObject player);
    public delegate void EnableSpecialAbility(object sender, int specialAbility);
}
