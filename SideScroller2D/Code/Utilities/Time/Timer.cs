using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller2D.Code.Utilities.Time
{
    class Timer
    {
        public delegate void OnDone();

        public virtual float Time { get; protected set; }

        public bool Done { get { return Time == 0; } }

        private Action onDone;

        public Timer(float time, Action onDone)
        {
            this.Time = time;
            this.onDone = onDone;
        }

        public virtual void Update()
        {
            if (Done)
                return;

            Time -= ElapsedTime.Seconds;

            if (Time <= 0)
            {
                Time = 0;

                onDone.Invoke();
            }
        }
    }
}
