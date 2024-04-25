using System;
using System.ComponentModel;

namespace ComunityConnections.Models
{
    public partial class user : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _userName;
        public string username
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(nameof(username)); }
        }

        private string _password;
        public string password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(password)); }
        }

        private string _email;
        public string email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(email)); }
        }

        private string _firstName;
        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(firstName)); }
        }

        private string _lastName;
        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(nameof(lastName)); }
        }

        private string _phoneNum;
        public string phoneNum
        {
            get { return _phoneNum; }
            set { _phoneNum = value; OnPropertyChanged(nameof(phoneNum)); }
        }

        private string _authQuestion;
        public string authQuestion
        {
            get { return _authQuestion; }
            set { _authQuestion = value; OnPropertyChanged(nameof(authQuestion)); }
        }

        private string _authAnswer;
        public string authAnswer
        {
            get { return _authAnswer; }
            set { _authAnswer = value; OnPropertyChanged(nameof(authAnswer)); }
        }
    }
}
