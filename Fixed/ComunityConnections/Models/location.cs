//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ComunityConnections.Models
//{
//    public class location : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//        public string name { get; set; }
//        public string region { get; set; }
//        public string country { get; set; }
//        public double lat { get; set; }
//        public double lon { get; set; }
//        public string tz_id { get; set; }
//        public long localtime_epoch { get; set; }
//        public DateTime localtime { get; set; }


//    }

//    public class Condition : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }

//        public string icon { get; set; }
//    }

//    public class CurrentWeather : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }

//        public double temp_f { get; set; }
//        public Condition condition { get; set; }
//    }

//    public class WeatherData : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }

//        public location location { get; set; }
//        public CurrentWeather current { get; set; }
//    }
//}

//------------------------------------------------------------------------------------------------------------------------//
//========================================================================================================================//
//------------------------------------------------------------------------------------------------------------------------//

//using System;
//using System.ComponentModel;

//namespace ComunityConnections.Models
//{
//    public class Location : INotifyPropertyChanged
//    {

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(string propertyName)
//        {
//            if (PropertyChanged != null)
//            {
//                //PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);

//                PropertyChanged(this, args);
//            }
//        }
//        private string _name;
//        public string name
//        {
//            get { return _name; }
//            set { _name = value; OnPropertyChanged(nameof(name)); }
//        }

//        private string _region;
//        public string region
//        {
//            get { return _region; }
//            set { _region = value; OnPropertyChanged(nameof(region)); }
//        }

//        private string _country;
//        public string country
//        {
//            get { return _country; }
//            set { _country = value; OnPropertyChanged(nameof(country)); }
//        }

//        private double _lat;
//        public double lat
//        {
//            get { return _lat; }
//            set { _lat = value; OnPropertyChanged(nameof(lat)); }
//        }

//        private double _lon;
//        public double lon
//        {
//            get { return _lon; }
//            set { _lon = value; OnPropertyChanged(nameof(lon)); }
//        }

//        private string _tz_id;
//        public string tz_id
//        {
//            get { return _tz_id; }
//            set { _tz_id = value; OnPropertyChanged(nameof(tz_id)); }
//        }

//        private long _localtime_epoch;
//        public long localtime_epoch
//        {
//            get { return _localtime_epoch; }
//            set { _localtime_epoch = value; OnPropertyChanged(nameof(localtime_epoch)); }
//        }

//        private string _localtime;
//        public string localtime
//        {
//            get { return _localtime; }
//            set { _localtime = value; OnPropertyChanged(nameof(localtime)); }
//        }

//    }

//    public class Condition : INotifyPropertyChanged
//    {

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(string propertyName)
//        {
//            if (PropertyChanged != null)
//            {
//                //PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);

//                PropertyChanged(this, args);
//            }
//        }
//        private string _icon;
//        public string icon
//        {
//            get { return _icon; }
//            set { _icon = value; OnPropertyChanged(nameof(icon)); }
//        }

//    }

//    public class CurrentWeather : INotifyPropertyChanged
//    {

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(string propertyName)
//        {
//            if (PropertyChanged != null)
//            {
//                //PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);

//                PropertyChanged(this, args);
//            }
//        }
//        private double _temp_f;
//        public double temp_f
//        {
//            get { return _temp_f; }
//            set { _temp_f = value; OnPropertyChanged(nameof(temp_f)); }
//        }

//        private Condition _condition;
//        public Condition condition
//        {
//            get { return _condition; }
//            set { _condition = value; OnPropertyChanged(nameof(condition)); }
//        }

//    }

//    public class WeatherData : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(string propertyName)
//        {
//            if (PropertyChanged != null)
//            {
//                //PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);

//                PropertyChanged(this, args);
//            }
//        }
//        private Location _location;
//        public Location location
//        {
//            get { return _location; }
//            set { _location = value; OnPropertyChanged(nameof(location)); }
//        }

//        private CurrentWeather _current;
//        public CurrentWeather current
//        {
//            get { return _current; }
//            set { _current = value; OnPropertyChanged(nameof(current)); }
//        }
//    }
//}

//------------------------------------------------------------------------------------------------------------------------//
//========================================================================================================================//
//------------------------------------------------------------------------------------------------------------------------//

public class Location
{
    public string name { get; set; }
    public string region { get; set; }
    public string country { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }
    public string tz_id { get; set; }
    public long localtime_epoch { get; set; }
    public string localtime { get; set; }
}

public class Condition
{
    //public string icon { get; set; }
    private string _icon;

    public string icon
    {
        get { return _icon; }
        set { _icon = "https:" + value;  }
    }

}

public class CurrentWeather
{
    public double temp_f { get; set; }
    public Condition condition { get; set; }
}

public class WeatherData
{
    public Location location { get; set; }
    public CurrentWeather current { get; set; }
}
