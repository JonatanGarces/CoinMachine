using CoinMachine;
using System;
using System.Timers;

namespace Library
{
    public class CountDownTimer : IDisposable
    {
        public Action TimeChanged;
        public Action CountDownFinished;
        public Action Notification;
        public Action Started;

        public ConfigManager configmanager = new ConfigManager();
        public bool IsRunnign => timer.Enabled;

        public double StepMs
        {
            get => timer.Interval;

            set => timer.Interval = value;
        }

        public Timer timer = new Timer();
        public TimeSpan TimeLeft { get; set; }
        public TimeSpan NotificationTime { get; set; }
        public string TimeLeftStr => TimeLeft.ToString("mm\\:ss");
        public double MinutesLeft => TimeLeft.TotalMinutes;

        private void TimerTick(object sender, EventArgs e)
        {
            // TimeLeft.Ticks / TimeSpan.TicksPerMillisecond;

            if ((TimeLeft.Ticks / TimeSpan.TicksPerMillisecond) > timer.Interval)
            {
                if (TimeLeft.TotalMilliseconds < NotificationTime.TotalMilliseconds && Global.Instance.NotificationAppeared == false) { Global.Instance.NotificationAppeared = true; Notification?.Invoke(); }
                TimeLeft = TimeLeft - TimeSpan.FromMilliseconds(timer.Interval);
                TimeChanged?.Invoke();
            }
            else
            {
                Global.Instance.NotificationAppeared = false;
                TimeLeft = TimeSpan.Zero;
                timer.Stop();
                CountDownFinished?.Invoke();
            }
        }

        public CountDownTimer()
        {
            Init();
        }

        private void Init()
        {
            StepMs = 1000;
            SetNotificationTime();
            timer.AutoReset = true;
            timer.Elapsed += TimerTick;
        }

        public void SetTime(float minutes)
        {
            TimeLeft = TimeSpan.FromMinutes(minutes);
            if (timer.Enabled == false)
            {
                this.Start();
                Started?.Invoke();
            }
        }

        public void SetNotificationTime()
        {
            NotificationTime += (TimeSpan.FromMinutes(Int32.Parse(configmanager.ReadSetting("NotificationMinute"))));
        }

        public void Start() => timer.Start();

        public void Dispose() => timer.Dispose();
    }
}