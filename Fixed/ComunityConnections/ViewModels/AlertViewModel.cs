using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ComunityConnections.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.Animations;


namespace ComunityConnections.ViewModels
{
    public class AlertViewModel : INotifyPropertyChanged
    {
        //-----------------------Setup for the Alert ViewModel Class-------------------------------------------------------------------------------//
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

        //-----Variable Creation-------------------------------------------------------------------------------------//



        private ObservableCollection<alert> _alerts;
        public ObservableCollection<alert> Alerts
        {
            get { return _alerts; }
            set { _alerts = value; OnPropertyChanged("Alerts"); }
        }

        private ObservableCollection<alert> _allalerts;

        public ObservableCollection<alert> Allalerts
        {
            get { return _allalerts; }
            set { _allalerts = value; OnPropertyChanged("Allalerts"); }
        }

        private ObservableCollection<user> _users;

        public ObservableCollection<user> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged("Users"); }
        }

        private ObservableCollection<status> _statuses;

        public ObservableCollection<status> Statuses
        {
            get { return _statuses; }
            set { _statuses = value; OnPropertyChanged("Statuses"); }
        }


        private alert _newAlert;

        public alert newAlert
        {
            get { return _newAlert; }
            set { _newAlert = value; OnPropertyChanged("newAlert"); }
        }

        private int count = 0;

        private user _userLogin;

        public user userLogin
        {
            get { return _userLogin; }
            set { _userLogin = value; OnPropertyChanged("userLogin"); }
        }

        private user _tempUser;

        public user tempUser
        {
            get { return _tempUser; }
            set { _tempUser = value; OnPropertyChanged("tempUser"); }
        }

        private user _zipLocation;
        public user zipLocation
        {
            get { return _tempUser; }
            set { _tempUser = value; OnPropertyChanged("zipLocation"); }
        }

        private WeatherData _weatherData;

        public WeatherData WeatherData
        {
            get { return _weatherData; }
            set { _weatherData = value; OnPropertyChanged("WeatherData"); }
        }

        public Command LoginCommand { get; private set; }
        public Command RegisterCommand { get; private set; }
        public Command CreateAlertCommand { get; private set; }
        public Command getWeatherCommand { get; private set; }


        //==============================================================================================================//
        //---------Logic for the class----------------------------------------------------------------------------------//
        //==============================================================================================================//


        //populate the user object with the login credentials upon entry
        private void populateUser()
        {
            /*_userLogin.username = Preferences.Get("loginUsername", "");
            _userLogin.password = Preferences.Get("loginPassword", "");
            _userLogin.authQuestion = Preferences.Get("userAuthQestion", "");
            _userLogin.authAnswer = Preferences.Get("userAuthAnswer", "");
            _userLogin.email = Preferences.Get("userEmail", "");
            _userLogin.phoneNum = Preferences.Get("userPhoneNum", "");
            _userLogin.firstName = Preferences.Get("userFirstName", "");
            _userLogin.lastName = Preferences.Get("userLastName", "");*/


        }

        //reset the user in case of a failed profile update
        public void resetUser()
        {
            _userLogin = _tempUser;

            Preferences.Set("loginUsername", _userLogin.username);
            Preferences.Set("loginPassword", _userLogin.password);
            Preferences.Set("userAuthQestion", _userLogin.authQuestion);
            Preferences.Set("userAuthAnswer", _userLogin.authAnswer);
            Preferences.Set("userEmail", _userLogin.email);
            Preferences.Set("userPhoneNum", _userLogin.phoneNum);
            Preferences.Set("userFirstName", _userLogin.firstName);
            Preferences.Set("userLastName", _userLogin.lastName);
        }




        //---------------------CHECK THE USER LOGIN AND REGISTRATION---------------------------------------------------------------------------------//





        //validate user login
        protected async void checkUser()
        {
            if (Users.Any(user => user.username == _userLogin.username))
            {
                user foundUser = Users.FirstOrDefault(u => u.username == _userLogin.username);

                if (Users.Any(user => user.password == _userLogin.password))
                {
                    for (int i = 0; i < Users.Count; i++)
                    {
                        if (Users[i].username == _userLogin.username)
                        {
                            if (Users[i].password == _userLogin.password)
                            {

                                //_userLogin.authQuestion = Users[i].authQuestion;
                                //_userLogin.authAnswer = Users[i].authAnswer;
                                //_userLogin.email = Users[i].email;
                                //_userLogin.phoneNum = Users[i].phoneNum;
                                //_userLogin.firstName = Users[i].firstName;
                                //_userLogin.lastName = Users[i].lastName;

                                _userLogin = Users[i];

                                tempUser = Users[i];
                                resetUser();
                                await App.Current.MainPage.DisplayAlert("Login", "Login Successful", "OK");

                            }
                        }
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Login", "Password Incorrect " + _userLogin.username, "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Login", "Username Incorrect", "OK");
            }

        }



        //validate new user registration
        protected async void checkRegister()
        {
            //if any fields are empty
            if (_userLogin.email == "" || _userLogin.username == "" || _userLogin.password == "" || _userLogin.authQuestion == "" || _userLogin.authAnswer == "" || _userLogin.phoneNum == "")
            {
                //return because fields are empty
                await App.Current.MainPage.DisplayAlert("Registration", "Please fill in all fields", "OK");
                return;
            }
            //if any relevant fields are already in use
            if (Users.Any(user => user.username == _userLogin.username) || Users.Any(user => user.email == _userLogin.email) || Users.Any(user => user.phoneNum == _userLogin.phoneNum))
            {
                await App.Current.MainPage.DisplayAlert("Registration", "User already in use ", "OK");
            }
            //if all fields are filled and the username and email are not already in use
            else
            {
                // await App.Current.MainPage.DisplayAlert("Registration", "Registration succesfull", "OK");
                Preferences.Set("loginUsername", _userLogin.username);
                Preferences.Set("loginPassword", _userLogin.password);

                //add to the user list so they can't register again before we add them to the database
                Users.Add(_userLogin);

                //logic to add user to the database 
                PostUser();

            }

        }


        protected async void checkAlert()
        {
            //if any fields are empty
            if (_newAlert.alertType == "" || _newAlert.alertTitle == "" || _newAlert.alertDescription == "" || _newAlert.zipcode == "" || _newAlert.location == "")
            {
                //return because fields are empty
                await App.Current.MainPage.DisplayAlert("Alert", "Please fill in all fields", "OK");
                return;
            }
            //if all fields are filled
            else
            {
                _newAlert.timePosted = DateTime.Now;
                Alerts.Add(_newAlert);
                PostAlert();

            }

        }


        //----------------GETTING FROM THE API---------------------------------------------------------------------------------//

        public async void GetAlerts()
        {
            //await App.Current.MainPage.DisplayAlert("it fires", "the command works", "OK");

            alert value = new alert();
            Uri uri = new Uri("http://localhost:8080/alerts");
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetStringAsync(uri);
                // await App.Current.MainPage.DisplayAlert("Test", "test 1", "OK");

                if (response.Length > 0)
                {

                    //string content = await response.Content.ReadAsStringAsync();
                    var alerts = JsonSerializer.Deserialize<List<alert>>(response);
                    if (Allalerts.Count == 0)
                    {
                        Allalerts.Clear();
                        Alerts.Clear();
                        foreach (var alert in alerts)
                        {
                            //await App.Current.MainPage.DisplayAlert("test", alert.alertID.ToString(), "OK");
                            Allalerts.Add(alert);
                            if(alert.zipcode == Preferences.Get("zipCode", "16001"))
                            {
                                Alerts.Add(alert);
                            }
                            //Alerts.Add(alert);
                        }
                    }

                    //await App.Current.MainPage.DisplayAlert("it fires", "thar she blows", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Rest Error", ex.ToString(), "OK");
            }
        }

        public async void GetUsers()
        {
            user value = new user();
            Uri uri = new Uri("http://localhost:8080/users");
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetStringAsync(uri);
                Users.Clear();
                if (response.Length > 0)
                {

                    var users = JsonSerializer.Deserialize<List<user>>(response);

                    foreach (var user in users)
                    {
                        //await App.Current.MainPage.DisplayAlert("test", user.userName.ToString(), "OK");

                        Users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Rest Error", ex.ToString(), "OK");
            }
        }


        public async void GetStatuses()
        {
            status value = new status();
            Uri uri = new Uri("http://localhost:8080/alerts");
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetStringAsync(uri);
                Statuses.Clear();
                if (response.Length > 0)
                {

                    var statuses = JsonSerializer.Deserialize<List<status>>(response);

                    foreach (var status in statuses)
                    {
                        Statuses.Add(status);
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Rest Error", ex.ToString(), "OK");
            }
        }




        private const string ApiKey = "your key";
        private const string BaseUrl = "https://api.weatherapi.com/v1/current.json";

        public async void GetWeatherData(string zipcode)
        {
            if (zipcode == null)
            {
                zipcode = "16001";
                await App.Current.MainPage.DisplayAlert("Weather Error", "Please enter a zip code", "OK");
                //return;
            }
            using var client = new HttpClient();
            string url = $"{BaseUrl}?key={ApiKey}&q={zipcode}&aqi=no";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    WeatherData = JsonSerializer.Deserialize<WeatherData>(json);
                    string temp = WeatherData.current.condition.icon;
                    WeatherData.current.condition.icon = "https:" + temp;
                   // App.Current.MainPage.DisplayAlert("Weather", WeatherData.current.condition.icon.ToString(), "OK");
                   Preferences.Set("weatherIcon", WeatherData.current.condition.icon);
                    // Clear the Alerts collection and add alerts based on the searched zipcode
                    Alerts.Clear();
                    foreach (var alert in Allalerts)
                    {
                        if (alert.zipcode == zipcode)
                        {
                            Alerts.Add(alert);

                        }
                    }

                    //    // Notify UI of changes in the Alerts collection
                        //OnPropertyChanged("Alerts");
                    //    // Save the selected zipcode
                        Preferences.Set("zipCode", zipcode);



                    //    await App.Current.MainPage.DisplayAlert("Weather", "Weather data retrieved. Alerts found: " + Alerts.Count.ToString(), "OK");
                }
                else
                {
                    /*await App.Current.MainPage.DisplayAlert("Weather ifElse Error", response.StatusCode.ToString(), "OK");*/
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Weather tryCatch Error", ex.ToString(), "OK");
            }
        }


        //public async void GetWeatherData(string zipcode)
       // {
            //if (zipcode == null)
            //{
            //    await App.Current.MainPage.DisplayAlert("Weather Error", "Please enter a zip code", "OK");
            //    return;
            //}
            //using var client = new HttpClient();
            //string url = $"{BaseUrl}?key={ApiKey}&q={zipcode}&aqi=no";

            //try
            //{
            //    HttpResponseMessage response = await client.GetAsync(url);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        string json = await response.Content.ReadAsStringAsync();
            //        WeatherData = JsonSerializer.Deserialize<WeatherData>(json);
            //        Alerts.Clear();
            //        for (int i = 0; i < Allalerts.Count; i++)
            //        {
            //            if (Allalerts[i].zipcode == zipcode)
            //            {
            //                Alerts.Add(Allalerts[i]);
            //            }
            //        }
            //        Preferences.Set("zipCode", zipcode);
            //        await App.Current.MainPage.DisplayAlert("Weather", "Weather data retrieved. Alerts found: " + Alerts.Count.ToString(), "OK");

            //    }
            //    else
            //    {
            //        await App.Current.MainPage.DisplayAlert("Weather ifElse Error", response.StatusCode.ToString(), "OK");

            //    }
            //}
            //catch (Exception ex)
            //{
            //    await App.Current.MainPage.DisplayAlert("Weather tryCatch Error", ex.ToString(), "OK");

            //}
        //}



        //----------------POSTING TO THE API---------------------------------------------------------------------------------//

        public async Task PostUser()
        {
            try
            {
                var json = JsonSerializer.Serialize(_userLogin);

                Uri addUserUri = new Uri("http://localhost:8080/users");

                HttpClient client = new HttpClient();

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(addUserUri, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    await App.Current.MainPage.DisplayAlert("Rest Success", "Added successfully", "OK");
                }
                else
                {
                    var errorResponseContent = await response.Content.ReadAsStringAsync();
                    //await App.Current.MainPage.DisplayAlert("Rest Error", "Failed to add: " + errorResponseContent, "OK");
                    resetUser();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Rest Error", ex.ToString(), "OK");
                resetUser();
            }
        }


        public async Task PostAlert()
        {
            try
            {
                _newAlert.username = Preferences.Get("loginUsername", "");
                var json = JsonSerializer.Serialize(_newAlert);
                //await App.Current.MainPage.DisplayAlert("test 1", " " + json, "OK");

                Uri addUserUri = new Uri("http://localhost:8080/alerts");

                HttpClient client = new HttpClient();

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(addUserUri, content);
                //await App.Current.MainPage.DisplayAlert("test 2", "" + response, "OK");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    await App.Current.MainPage.DisplayAlert("Rest Success", "Added successfully", "OK");
                }
                else
                {
                    var errorResponseContent = await response.Content.ReadAsStringAsync();
                    await App.Current.MainPage.DisplayAlert("Rest Error", "Failed to add: " + errorResponseContent, "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Rest Error", ex.ToString(), "OK");
            }
        }


        //-------------PUTTING TO THE API---------------------------------------------------------------------------------//



        public async void updateUser()
        {
            populateUser();


            try
            {
                var json = JsonSerializer.Serialize(_userLogin);

                Uri addUserUri = new Uri("http://localhost:8080/users");

                HttpClient client = new HttpClient();

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(addUserUri, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    await App.Current.MainPage.DisplayAlert("Rest Success", " Pofile updated", "OK");
                }
                else
                {
                    var errorResponseContent = await response.Content.ReadAsStringAsync();
                    _userLogin = _tempUser;
                    // await App.Current.MainPage.DisplayAlert("Rest Error", "Failed to update: " + errorResponseContent, "OK");
                }
            }
            catch (Exception ex)
            {
                _userLogin = _tempUser;
                await App.Current.MainPage.DisplayAlert("Rest Error", ex.ToString(), "OK");
            }
        }


        //-----------------------Constructor for the Alert ViewModel Class-------------------------------------------------------------------------------//
        public AlertViewModel()
        {
            _userLogin = new user();
            _newAlert = new alert();
            Alerts = new ObservableCollection<alert>();
            Allalerts = new ObservableCollection<alert>();
            Users = new ObservableCollection<user>();
            Statuses = new ObservableCollection<status>();
            WeatherData = new WeatherData();

            populateUser();
            GetAlerts();
            GetUsers();
            GetWeatherData(Preferences.Get("zipCode", "16001"));


            LoginCommand = new Command(checkUser);
            RegisterCommand = new Command(checkRegister);
            CreateAlertCommand = new Command(checkAlert);
            //getWeatherCommand = new Command(GetWeatherData);
        }


    }
}
