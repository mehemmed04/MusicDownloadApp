using MusicDownloadApp.Commands;
using MusicDownloadApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MusicDownloadApp.ViewModels
{
    public class MusicUCViewModel : BaseViewModel
    {
        public Thread _task { get; set; }

        private Music music;

        public Music Music
        {
            get { return music; }
            set { music = value; OnPropertyChanged(); }
        }

        private int second;

        public int Second
        {
            get { return second; }
            set { second = value; OnPropertyChanged(); }
        }

        private int minute;

        public int Minute
        {
            get { return minute; }
            set { minute = value; OnPropertyChanged(); }
        }
        public void StopTimer()
        {
            Timer.Stop();
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        public RelayCommand PauseCommand { get; set; }
        public RelayCommand PlayCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }


        public DispatcherTimer Timer { get; set; }

        private string statusColor;

        public string StatusColor
        {
            get { return statusColor; }
            set { statusColor = value; OnPropertyChanged(); }
        }

        public MusicUCViewModel(Music music, Thread task)
        {
            _task = task;
            Timer = new DispatcherTimer();
            Second = 0;
            Minute = 0;
            Music = music;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += IncreaseSeconds;
            Timer.Start();
            StatusColor = "Black";
            Status = "Downloading ...";


            PauseCommand = new RelayCommand((o) =>
            {
                StatusColor = "Red";
                Status = "Paused";
                Timer.Stop();
                MessageBox.Show("timer stopped");
                _task.Suspend();

            },
            (p) =>
            {
                return true;
            });

            PlayCommand = new RelayCommand((o) =>
            {
                StatusColor = "Black";
                Status = "Downloading ...";
                _task.Resume();
            },
           (p) =>
           {
               return true;
           });

            CancelCommand = new RelayCommand((o) =>
            {

                StatusColor = "Red";
                Status = "Cancelled";
                Timer.Stop();
                _task.Abort();
            },
           (p) =>
           {
               return true;
           });

        }

        private void IncreaseSeconds(object sender, EventArgs e)
        {
            Second += 1;
            if (Second == 60)
            {
                Second = 0;
                Minute += 1;
            }
        }
    }
}
