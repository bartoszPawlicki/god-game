using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
    public class CooldownProvider
    {
        /// <summary>
        /// In 0 - 100 range
        /// </summary>
        public float Loading
        {
            get
            {
                float returnValue = 100 - (_originCooldown - _stopwatch.ElapsedMilliseconds) / _originCooldown * 100f;

                if (returnValue > 100)
                {
                    returnValue = 100;
                    _stopwatch.Stop();
                }
                else if (!_inUse && returnValue == 0)
                    returnValue = 100;

                return returnValue;
            }
        }

        public CooldownProvider(float cooldown)
        {
            _originCooldown = cooldown;
        }
        public void Use()
        {
            _inUse = true;
            _stopwatch.Reset();
        }
        public void Start()
        {
            _inUse = false;
            _stopwatch.Start();
        }

        private bool _inUse = false;
        private float _originCooldown;
        private Stopwatch _stopwatch = new Stopwatch();
    }
}
