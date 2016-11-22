using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class CooldownBehaviour : MonoBehaviour
    {
        public float RemainingCooldown
        {
            get
            {
                return (float)((_cooldownStopwatch.ElapsedMilliseconds + 10) / _cooldownTimer.Interval * 100);
            }
        }
        public int Cooldown
        {
            set
            {
                _cooldownTimer.Interval = value;
            }
        }

        public void InitializeCooldown(float interval)
        {
            _cooldownTimer.Interval = interval;
            _cooldownTimer.Elapsed += _cooldownTimer_Elapsed;
            StartCooldown();
        }

        protected void StartCooldown()
        {
            _cooldownTimer.Start();
            _cooldownStopwatch.Reset();
            _cooldownStopwatch.Start();
        }
        void Start()
        {
            GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var item in _players)
            {
                if (item != gameObject)
                    _playerColliding.Add(item, false);
            }
        }

        private void _cooldownTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _cooldownStopwatch.Stop();
        }

        private Dictionary<GameObject, bool> _playerColliding = new Dictionary<GameObject, bool>();

        private Timer _cooldownTimer = new Timer() { AutoReset = false };
        private Stopwatch _cooldownStopwatch = new Stopwatch();
    }
}
