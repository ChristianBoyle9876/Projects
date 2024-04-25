using System;
using System.ComponentModel;

namespace ComunityConnections.Models
{
    public partial class alert : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                //PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);

                PropertyChanged(this, args);
            }
        }

        private DateTime _timePosted;
        public DateTime timePosted
        {
            get { return _timePosted; }
            set { _timePosted = value; OnPropertyChanged(nameof(timePosted)); }
        }

        private string _alertType;
        public string alertType
        {
            get { return _alertType; }
            set { _alertType = value; OnPropertyChanged(nameof(alertType)); }
        }

        private string _alertTitle;
        public string alertTitle
        {
            get { return _alertTitle; }
            set { _alertTitle = value; OnPropertyChanged(nameof(alertTitle)); }
        }

        private string _alertDescription;
        public string alertDescription
        {
            get { return _alertDescription; }
            set { _alertDescription = value; OnPropertyChanged(nameof(alertDescription)); }
        }

        private string _zipcode;
        public string zipcode
        {
            get { return _zipcode; }
            set { _zipcode = value; OnPropertyChanged(nameof(zipcode)); }
        }

        private string _location;
        public string location
        {
            get { return _location; }
            set { _location = value; OnPropertyChanged(nameof(location)); }
        }

        private string _status;
        public string status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(status)); }
        }

        private string _username;
        public string username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(username)); }
        }
    }
}
