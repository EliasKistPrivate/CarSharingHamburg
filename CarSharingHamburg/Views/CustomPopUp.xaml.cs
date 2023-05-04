using CommunityToolkit.Maui.Views;

namespace CarSharingHamburg.Views;

public partial class CustomPopUp : Popup
{
	public CustomPopUp()
	{
		InitializeComponent();
	}

    void OnOKButtonClicked(object? sender, EventArgs e) => Close();
}