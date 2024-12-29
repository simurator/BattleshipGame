using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class Timer
    {
        private DateTime? startTime;
        private DateTime? endTime;

        public void Start()
        {
            startTime = DateTime.Now;
            endTime = null;
        }

        public void Stop()
        {
            if (startTime != null)
            {
                endTime = DateTime.Now;
            }
        }

        public float GetElapsedTime()
        {
            if (startTime == null)
            {
                throw new InvalidOperationException("Timer has not been started.");
            }

            var end = endTime ?? DateTime.Now;
            return (float)(end - startTime.Value).TotalSeconds;
        }
    }
}
