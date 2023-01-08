using MusicDownloadApp.Commands;
using MusicDownloadApp.Services;
using MusicDownloadApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MusicDownloadApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand AddMusicCommand { get; set; }
        public WrapPanel MusicsWrapPanel { get; set; }
        public MainViewModel()
        {
            AddMusicCommand = new RelayCommand((o) =>
            {
                AddMusicWindow amw = new AddMusicWindow();
                AddMusicViewModel amwm = new AddMusicViewModel(amw);
                amw.DataContext = amwm;
                amw.ShowDialog();
                if (amwm.IsClicked)
                {
                    Thread task = null;
                    MusicUCViewModel mvm = new MusicUCViewModel(amwm.Music,task);
                    MusicUC musicUC = new MusicUC();
                    musicUC.DataContext = mvm;
                    MusicsWrapPanel.Children.Add(musicUC);
                    MusicService.SaveMP3(mvm,task);
                    
                }

            });
        }
    }
}
