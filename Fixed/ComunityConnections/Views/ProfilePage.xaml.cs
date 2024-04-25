namespace ComunityConnections.Views;
using ComunityConnections.ViewModels;
using System.Runtime.ExceptionServices;

public partial class ProfilePage : ContentPage
{
    private AlertViewModel model;
    //Preferences.Set("userAuthQestion", Users[i].authQuestion);
    //                Preferences.Set("userAuthAnswer", Users[i].authAnswer);
    //                Preferences.Set("userEmail", Users[i].email);
    //                Preferences.Set("userPhoneNum", Users[i].phoneNum);
    //                Preferences.Set("userFirstName", Users[i].firstName);
    //                Preferences.Set("userLastName", Users[i].lastName);
    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = new AlertViewModel();

        model = new AlertViewModel();
    }

    //make it so it repopulates the field everytime you enter the page
    protected override void OnAppearing()
    {
        base.OnAppearing();
        usernameLabel.Text = Preferences.Get("loginUsername", "User");
        passwordEntry.Text = Preferences.Get("loginPassword", "nothing");
        FirstNameEntry.Text = Preferences.Get("userFirstName", "First Name");
        LastNameEntry.Text = Preferences.Get("userLastName", "Last Name");
        emailEntry.Text = Preferences.Get("userEmail", "Email");
        phoneEntry.Text = Preferences.Get("userPhoneNum", "Phone Number");
        authQuestionEntry.Text = Preferences.Get("userAuthQestion", "Auth Question");
        authAnswerEntry.Text = Preferences.Get("userAuthAnswer", "Auth Answer");
    }

    private void Button_Pressed(object sender, EventArgs e)
    {

        Preferences.Set("loginUsername", usernameLabel.Text);
        Preferences.Set("loginPassword", passwordEntry.Text);
        Preferences.Set("userFirstName", FirstNameEntry.Text);
        Preferences.Set("userLastName", LastNameEntry.Text);
        Preferences.Set("userEmail", emailEntry.Text);
        Preferences.Set("userPhoneNum", phoneEntry.Text);
        Preferences.Set("userAuthQestion", authQuestionEntry.Text);
        Preferences.Set("userAuthAnswer", authAnswerEntry.Text);


        BindingContext = new AlertViewModel();

        model.updateUser();
        usernameLabel.Text = Preferences.Get("loginUsername", "User");
        passwordEntry.Text = Preferences.Get("loginPassword", "nothing");
        FirstNameEntry.Text = Preferences.Get("userFirstName", "First Name");
        LastNameEntry.Text = Preferences.Get("userLastName", "Last Name");
        emailEntry.Text = Preferences.Get("userEmail", "Email");
        phoneEntry.Text = Preferences.Get("userPhoneNum", "Phone Number");
        authQuestionEntry.Text = Preferences.Get("userAuthQestion", "Auth Question");
        authAnswerEntry.Text = Preferences.Get("userAuthAnswer", "Auth Answer");
    }
}