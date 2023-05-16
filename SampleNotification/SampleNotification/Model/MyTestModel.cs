using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SampleNotification.Model
{
    [Table("Token")]
    public partial class MyTestModel :INotifyPropertyChanged
    {
        [PrimaryKey, NotNull]
        public Guid TestId { get; set; }

        public int Period { get; set; }


        #region User

        private string _user;

        public string User
        {
            get => _user;
            set
            {
                if (_user == value) return;

                _user = value;

                OnPropertyChanged();
            }
        }

        #endregion User

        #region Display

        private string _display;



        public string Display
        {
            get => _display;
            set
            {
                if (_display == value) return;

                _display = value;

                OnPropertyChanged();
            }
        }

        #endregion Display



        #region PropertyChange


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion PropertyChange 

    }
}
