namespace ComunityConnections.Views;

public partial class ProfileVerification : ContentPage
{
	protected string answer;
	public ProfileVerification()
	{
		InitializeComponent();
        QuestionLabel.Text = Preferences.Get("userAuthQestion", "default");

    }


    private void Button_Pressed(object sender, EventArgs e)
    {
		answer = answerEntry.Text;
        if (answer == Preferences.Get("userAuthAnswer", "default"))
		{
            Navigation.PushAsync(new ProfilePage());
        }
        else
        {
            DisplayAlert("Error", "Incorrect answer", "OK");
        }

    }
}