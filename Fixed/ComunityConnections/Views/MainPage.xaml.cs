namespace ComunityConnections;
using ComunityConnections.ViewModels;
using ComunityConnections.Views;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new AlertViewModel();
    }

    private async void RegiNav(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    private void ZipBtn_Clicked(object sender, EventArgs e)
    {
    Preferences.Set("zipCode", ZipEntry.Text);

    }
}

