using MediaToolkit;
using MediaToolkit.Model;
using MusicDownloadApp.Models;
using MusicDownloadApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VideoLibrary;

namespace MusicDownloadApp.Services
{
    public class MusicService
    {
        public static async void SaveMP3(MusicUCViewModel viewmodel,Thread task)
        {
            var music = viewmodel.Music;
            task = new Thread(() =>
            {
                try
                {
                    viewmodel._task = task;
                    string source = music.Path;
                    var youtube = YouTube.Default;
                    var vid = youtube.GetVideo(music.Url);
                    string videopath = Path.Combine(source, vid.FullName);
                    byte[] bytes = vid.GetBytes();
                    File.WriteAllBytes(videopath, bytes);
                    var inputFile = new MediaFile { Filename = Path.Combine(source, vid.FullName) };
                    var outputFile = new MediaFile { Filename = Path.Combine(source, $"{music.Name}.mp3") };
                    using (var engine = new Engine())
                    {
                        engine.GetMetadata(inputFile);
                        engine.Convert(inputFile, outputFile);
                    }
                    File.Delete(Path.Combine(source, vid.FullName));
                    viewmodel.StopTimer();
                    viewmodel.Status = "Downloaded";
                    viewmodel.StatusColor = "Green";
                }
                catch (Exception ex)
                {
                    viewmodel.StopTimer();
                    viewmodel.StatusColor = "Red";
                    viewmodel.Status = "Cancelled. " + ex.Message;
                }
            });
            task.Start();
        }
    }
}
