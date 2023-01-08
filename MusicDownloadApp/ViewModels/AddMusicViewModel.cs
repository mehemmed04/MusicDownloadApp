using Microsoft.Win32;
using MusicDownloadApp.Commands;
using MusicDownloadApp.Models;
using MusicDownloadApp.Services;
using MusicDownloadApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace MusicDownloadApp.ViewModels
{
    public class AddMusicViewModel:BaseViewModel
    {

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; Music.Path = value; OnPropertyChanged(); }
        }

        private AddMusicWindow _window { get; set; }
        private Music music;

        public Music Music
        {
            get { return music; }
            set { music = value; OnPropertyChanged(); }
        }
        public bool IsClicked { get; set; } = false;
        public RelayCommand DownloadCommand { get; set; }
        public RelayCommand SelectFilePathCommand { get; set; }
        public AddMusicViewModel(AddMusicWindow window)
        {
            Music = new Music();
            Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            _window = window;
            DownloadCommand = new RelayCommand((o) =>
            {
                try
                {
                    IsClicked = true;
                    _window.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });

            SelectFilePathCommand = new RelayCommand((o) =>
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        Path = fbd.SelectedPath+"\\";
                    }
                }


            });

        }
    }
}
