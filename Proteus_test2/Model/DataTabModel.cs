using Proteus_test2.Common.MVVM;
using Proteus_test2.Interfaces;
using Proteus_test2.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Proteus_test2.Model
{
    public class DataTabModel : Tab
    {
        private bool playVisibility;
        public bool PlayVisibility
        {
            get => playVisibility;
            set
            {
                playVisibility = value;
                OnPropertyChanged(nameof(PlayVisibility));
            }
        }

        private bool pauseVisibility;
        public bool PauseVisibility
        {
            get => pauseVisibility;
            set
            {
                pauseVisibility = value;
                OnPropertyChanged(nameof(PauseVisibility));
            }
        }

        private bool proceedVisibility;
        public bool ProceedVisibility
        {
            get => proceedVisibility;
            set
            {
                proceedVisibility = value;
                OnPropertyChanged(nameof(ProceedVisibility));
            }
        }

        private bool dischargeVisibility;
        public bool DischargeVisibility
        {
            get => dischargeVisibility;
            set
            {
                dischargeVisibility = value;
                OnPropertyChanged(nameof(DischargeVisibility));
            }
        }

        private string timerText;
        public string TimerText
        {
            get => timerText;
            set
            {
                timerText = value;
                OnPropertyChanged(nameof(TimerText));
            }
        }

        private bool dischargeEnbled;
        public bool DischargeEnbled
        {
            get => dischargeEnbled;
            set
            {
                dischargeEnbled = value;
                OnPropertyChanged(nameof(DischargeEnbled));
            }
        }
        
        public ICommand PlayTimerCommand { get; }
        public ICommand PauseTimerCommand { get; }
        public ICommand ProceedTimerCommand { get; }
        public ICommand DischargeTimerCommand { get; }

        public DataTabModel()
        {
            PlayTimerCommand = new RelayCommand(PlayTimerExecute);
            PauseTimerCommand = new RelayCommand(PauseTimerExecute);
            //ProceedTimerCommand = new RelayCommand(ProceedTimerExecute);
            DischargeTimerCommand = new RelayCommand(DischargeTimerExecute);

            PlayVisibility = true;
            PauseVisibility = false;
            ProceedVisibility = false;
            DischargeVisibility = false;
            DischargeEnbled = false;

            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);

            MainWindowViewModel.NumTab++;
            Name = "Секундомер " + MainWindowViewModel.NumTab + ' ' + DateTime.Now.ToLongTimeString();
            TimerText = "00:00:00";
        }

        private void DischargeTimerExecute(object obj)
        {
            sw.Reset();
            DeleteEnable = true;
            PauseVisibility = false;
            DischargeVisibility = false;
            DischargeEnbled = false;
            ProceedVisibility = false;
            PlayVisibility = true;
            TimerText = "00:00:00";
        }

        //private void ProceedTimerExecute(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        private void PauseTimerExecute(object obj)
        {
            if (sw.IsRunning)
            {
                sw.Stop();
                //numTab--;
            }
            DeleteEnable = false;
            ProceedVisibility = true;
            PauseVisibility = false;
            DischargeEnbled = true;
        }

        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;
        public bool DeleteEnable = true;
        private void PlayTimerExecute(object obj)
        {
            sw.Start();
            dt.Start();
            DeleteEnable = false;
            DischargeEnbled = false;
            PauseVisibility = true;
            DischargeVisibility = true;
            PlayVisibility = false;
            ProceedVisibility = false;
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                TimerText = currentTime;
            }
        }
    }
}
