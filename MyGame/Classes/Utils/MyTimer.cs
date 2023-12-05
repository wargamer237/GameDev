using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtils
{
    internal class MyTimer
    {
        float m_DurationSec;
        float m_CurantTime;
        bool m_TimerIsRunning;
        float Duration
        {
            get { return m_DurationSec; }
            set { m_DurationSec = value; }
        }
        public MyTimer(float durationSec)
        {
            m_DurationSec = durationSec;
            Start();
        }
        private void Start()
        {
            if (m_DurationSec < 0) return;
            m_TimerIsRunning = true;
            m_CurantTime = 0;
        }
        public void Reset()
        {
            Start();
        }
        public bool IsRun()
        {
            return m_TimerIsRunning;
        }
        public bool IsRun(float elapsedSec)
        {
            if (!m_TimerIsRunning) return m_TimerIsRunning;

            m_CurantTime += 2 * elapsedSec;

            if (m_CurantTime < m_DurationSec) return m_TimerIsRunning;
            m_TimerIsRunning = false;

            return m_TimerIsRunning;
        }
    }
}
