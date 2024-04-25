namespace ComunityConnections.Views;
using ComunityConnections.ViewModels;
using System.Reflection.Emit;


public partial class ShowAlerts : ContentPage
{
    private AlertViewModel _alertViewModel;

    public ShowAlerts()
    {
        InitializeComponent();
        BindingContext = new AlertViewModel();
        _alertViewModel = new AlertViewModel();
        /*ZipEntry.Text = Preferences.Get("zipCode", "");*/
    }

    public RefreshView RefreshView { get; set; }

    private async void RefreshView_Refreshing(object sender, EventArgs e)
    {
        await Task.Delay(2000);
        _alertViewModel.GetAlerts();
        RefreshView.IsRefreshing = false;
    }

    private async void CreateAlertNav(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateAlert());
    }



    protected void alertShow(object sender, EventArgs e)
    {

    }
    protected void OnAppearing()
    {
        base.OnAppearing();
        _alertViewModel.GetAlerts();
/*        _alertViewModel.GetWeatherData(ZipEntry.Text);*/
        imagew.Source = Preferences.Get("weatherIcon", "");
       /* ZipEntry.Text = Preferences.Get("zipCode", "");*/

    }

    private void Search(object sender, EventArgs e)
    {
/*        string a = ZipEntry.Text.ToString();*//*
        _alertViewModel.GetWeatherData(a);*/
    }
}