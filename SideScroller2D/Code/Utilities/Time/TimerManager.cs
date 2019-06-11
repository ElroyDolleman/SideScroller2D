using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller2D.Code.Utilities.Time
{
    static class TimerManager
    {
        static List<Timer> timers = new List<Timer>();

        public static void Update()
        {
            for (int i = 0; i < timers.Count; i++)
            {
                timers[i].Update();

                if (timers[i].Done)
                {
                    timers.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void SetTimeOut(float time, Action onDone)
        {
            timers.Add(new Timer(time, onDone));
        }

        public static void AddTimer(Timer timer)
        {
            timers.Add(timer);
        }
    }
}
