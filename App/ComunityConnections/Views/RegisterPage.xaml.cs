namespace ComunityConnections.Views;
using ComunityConnections.ViewModels;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new AlertViewModel();

    }
}