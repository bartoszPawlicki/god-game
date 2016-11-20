using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public delegate void GameEndEventHandler(object sender, Winner w);
    public delegate void DealDamageEventHandler(object sender, int damage);
}
