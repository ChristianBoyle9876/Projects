namespace ComunityConnections.Views;
using ComunityConnections.ViewModels;
public partial class CreateAlert : ContentPage
{
	public CreateAlert()
	{
		InitializeComponent();
        BindingContext = new AlertViewModel();

    }
}