using UnityEngine;

namespace Common.Timers
{
    public class Stopwatch
    {
        private float _startTime;
        public Stopwatch() { }

        bool started = false;

        public float ElapsedSeconds { get { return started ? Time.time - _startTime : 0; } }

        private void Start()
        {
            Reset();
        }

        public void Reset()
        {
            started = true;
            _startTime = Time.time;
        }

        public static Stopwatch CreateAndStart()
        {
            var result = new Stopwatch();
            result.Start();
            return result;
        }
    }
}