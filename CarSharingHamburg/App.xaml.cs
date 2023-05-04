namespace CarSharingHamburg;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        if (DeviceInfo.Platform == DevicePlatform.UWP || DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            MainPage = new AppShellWindows();
        }
        else
        {
            MainPage = new AppShell();

        }

    }
}
