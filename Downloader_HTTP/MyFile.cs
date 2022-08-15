using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Downloader_HTTP
{
    internal class MyFile:MyNotifyPropertyChanged
    {
        private string path;
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                OnPropertyChanged();
            }
        }

        private WebClient client;

        private RelayCommand stopCmd;
        public ICommand StopCmd => stopCmd;

        private RelayCommand removeCmd;
        public ICommand RemoveCmd => removeCmd;

        private MyProgressBar progressBar;
        public MyProgressBar ProgressBar
        {
            get
            {
                return progressBar;
            }
            set
            {
                progressBar = value;
                OnPropertyChanged();
            }
        }

        private bool isUnloaded;
        public bool IsUnloaded
        {
            get
            {
                return isUnloaded;
            }
            set
            {
                isUnloaded = value;
                OnPropertyChanged();
            }
        }

        public MyFile(RemoveFile removeFile)
        {
            this.removeFile = removeFile;

            client = new WebClient();

            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.DownloadProgressChanged += Client_DownloadProgressChanged;

            ProgressBar = new MyProgressBar();

            stopCmd = new RelayCommand((t) => Stop(), (t) => CanStop());
            removeCmd = new RelayCommand((t) => Remove(), null);
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ProgressBar.State = ProgressBarState.Uploaded;
        }

        private void Stop()
        {
            if (!IsUnloaded) client.CancelAsync();
        }
        private bool CanStop()
        {
            if (!IsUnloaded) return true;
            return false;
        }
        private void Remove()
        {
            if (!IsUnloaded) client.CancelAsync();
            removeFile.Invoke(this);
        }

        public RemoveFile removeFile;
        public delegate void RemoveFile(MyFile file);

        public async void DownloadFileAsync(string address)
        {
            try
            {
                await client.DownloadFileTaskAsync(address, path);
            }
            catch
            {
                ProgressBar.State = ProgressBarState.Error;
            }
            
        }
    }
}
