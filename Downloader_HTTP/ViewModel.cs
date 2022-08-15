using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Downloader_HTTP
{
    internal class ViewModel:MyNotifyPropertyChanged
    {
        ObservableCollection<MyFile> files = new ObservableCollection<MyFile>();
        public IEnumerable<MyFile> Files => files;

        private RelayCommand startDownloadCmd;
        public ICommand StartDownloadCmd => startDownloadCmd;

        private string source;
        public string Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
                OnPropertyChanged();
            }
        }

        private string path;

        public MyFile SelectedFile
        {
            set
            {
                if (value != null)
                {
                    file = new FileInfo(value.Path);
                    OpenFile(value.Path);
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(File));
            }
        }

        private FileInfo file;
        public FileInfo File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
                OnPropertyChanged();
            }
        }

        public ViewModel()
        {
            startDownloadCmd = new RelayCommand((t) => StartDownload(), (t) => CanStartDownload());
        }
        private void StartDownload()
        {
            string s = System.IO.Path.GetExtension(Source);
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = $"Files *{s}|*{s}";
            if (saveFileDialog.ShowDialog() == true)
                path = saveFileDialog.FileName;

            files.Add(new MyFile(RemoveFile)
            {
                Path=path,
            });

            files.Last().DownloadFileAsync(Source);
        }
        private bool CanStartDownload()
        {
            if (Source != null) return true;
            return false;
        }

        public void RemoveFile(MyFile file)
        {
            files.Remove(file);
        }

        public void OpenFile(string path2)
        {

        }
    }
}
