namespace WpfMediaPlayer
{
    using System;
    using System.Windows;
    using Microsoft.Win32;
    using System.Windows.Threading;
    using WinForms = System.Windows.Forms;
    using System.IO;
    using System.Collections.Generic;


    public partial class MainWindow : Window
    {
        private bool isPlaying = false;
        private List<string> audios;
        int ActivIndex = -1;

        public MainWindow()
        {
            this.InitializeComponent();
            this.ChangeIsPlaying(false);
            audios = new List<string>();
        }

        private void PlayPause_OnClick(object sender, RoutedEventArgs e)
        {
            this.ChangeIsPlaying(!this.isPlaying);
        }

        private void SetMediaSource(string file)
        {
            this.mediaElement.Source = new Uri(file);
            NameMediaFile.Text = Path.GetFileNameWithoutExtension(file);
        }

        private void Stop_OnClick(object sender, RoutedEventArgs e)
        {
            this.OnMediaEnded();
        }

        private void MediaElement_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            this.OnMediaEnded();
        }

        private void MediaElement_OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            this.OnMediaEnded();
        }

        private void OnMediaEnded()
        {
            this.mediaElement.Stop();
            this.ChangeIsPlaying(false);
        }

        private void ChangeIsPlaying(bool isPlaying)
        {
            this.isPlaying = isPlaying;

            if (this.isPlaying)
            {
                this.playPause.ChangeToPlayingState();
                this.mediaElement.Play();
            }
            else
            {
                this.playPause.ChangeToPauseState();
                this.mediaElement.Pause();
            }
        }

        private void VolumeSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.mediaElement.Volume = this.volumeSlider.Value;
            double percent = volumeSlider.Value;
            if(VolumePercent != null)
            VolumePercent.Text = Convert.ToInt32(percent * 100).ToString() + "%";
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            string file = this.OpenFile();
            if (file == null)
            {
                return;
            }

            this.SetMediaSource(file);
        }

        private string OpenFile()
        {
            string filter = "Video (*.avi, *.mkv, *.mp4, *.flv)|*.avi;*.mkv;*.mp4;*.flv|Audio(*.ogg, *.mp3, *.wav)|*.ogg;*.mp3;*.wav;|All Files(*.*)|*.*";
            var openDialog = new OpenFileDialog {Multiselect = false, CheckFileExists = true, CheckPathExists = true, Title = "Select video file", AddExtension = true, Filter = filter};
            if (openDialog.ShowDialog(this) == true)
            {
                return openDialog.FileName;
            }
            return null;
        }

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Aiman copyright 2016, All rights deserved!");
        }

        private void MenuItemOpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            string path = OpenFile();
            if (path != null)
            {
                string ext = Path.GetExtension(path);
                if (ext == ".ogg" || ext == ".mp3" || ext == ".wav")
                {
                    mediaElement.Visibility = Visibility.Hidden;
                }
                else
                {
                    mediaElement.Visibility = Visibility.Visible;
                }

                ActivIndex = -1;
                audios.Clear();
                listView.Items.Clear();
                listView.Visibility = Visibility.Hidden;
                listView.Height = 0;
                BackButton.IsEnabled = false;
                NextButton.IsEnabled = false;

                SetMediaSource(path);
                ChangeIsPlaying(true);
            }
        }

        private void MenuItemOpenFolderMusic_OnClick(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog fbd = new WinForms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = fbd.SelectedPath;
                audios.Clear();
                SerchAudioFile(path);
                UpdateList();
                ChangeIsPlaying(false);
                mediaElement.Source = null;
                NameMediaFile.Text = "";
                listView.Visibility = Visibility.Visible;
                BackButton.IsEnabled = true;
                NextButton.IsEnabled = true;
                if (MainWindowPlayer.WindowState != WindowState.Maximized)
                    listView.Height = 400;
                mediaElement.Visibility = Visibility.Hidden;
            }
        }

        private void SerchAudioFile(string path)
        {
            try
            {
                string[] files = Directory.GetFiles(path);
                foreach (string itmf in files)
                {
                    string ext = Path.GetExtension(itmf);
                    if (ext == ".ogg" || ext == ".mp3" || ext == ".wav")
                    {
                        audios.Add(itmf);
                        listView.Items.Add(Path.GetFileName(itmf));
                    }
                }
                string[] dirs = Directory.GetDirectories(path);
                foreach (string itmd in dirs)
                {
                    SerchAudioFile(itmd);
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine("Ошибка: " + exp.Data);
            } 
        }

        private void UpdateList()
        {
            foreach (string itm in audios)
            {
                listView.Items.Add(Path.GetFileName(itm));
            }
        }

        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!listView.Items.IsEmpty && listView.SelectedItem != null)
            {
                SetMediaSource(audios[listView.SelectedIndex]);
                ActivIndex = listView.SelectedIndex;
                ChangeIsPlaying(true);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActivIndex == audios.Count - 1) ActivIndex = 0;
            else ActivIndex++;
            SetMediaSource(audios[ActivIndex]);
            listView.SelectedIndex = ActivIndex;
            ChangeIsPlaying(true);

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActivIndex == 0 || ActivIndex == -1) ActivIndex = audios.Count-1;
            else ActivIndex--;
            SetMediaSource(audios[ActivIndex]);
            listView.SelectedIndex = ActivIndex;
            ChangeIsPlaying(true);
        }

        private void MainWindowPlayer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (listView.Visibility == Visibility.Visible)
            {
                if (MainWindowPlayer.WindowState != WindowState.Maximized)
                    listView.Height = 400;
                else listView.Height = double.NaN;
            }
            else
            {
                listView.Height = 0;
            }
        }
    }
}
