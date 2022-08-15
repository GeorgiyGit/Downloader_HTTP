using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Downloader_HTTP
{
    enum ProgressBarState
    {
        Loading,
        Uploaded,
        Error
    }

    internal class MyProgressBar : MyNotifyPropertyChanged
    {          
        private ProgressBarState state;
        public ProgressBarState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Color));
            }
        }

        private int value;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Percent));
            }
        }

        public string Percent
        {
            get
            {
                return Value + " %";
            }
        }

        public Brush Color
        {
            get
            {
                switch (State)
                {
                    case ProgressBarState.Loading:
                        return Brushes.Green;
                        break;

                    case ProgressBarState.Uploaded:
                        return Brushes.Blue;
                        break;

                    case ProgressBarState.Error:
                        return Brushes.Red;
                        break;
                    default:
                        return Brushes.Red;
                        break;
                }
            }
        }
    }
}
