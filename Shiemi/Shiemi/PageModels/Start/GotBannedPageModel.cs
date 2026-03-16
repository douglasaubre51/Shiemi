using CommunityToolkit.Mvvm.Input;
using Shiemi.Storage;

namespace Shiemi.PageModels.Start;

public partial class GotBannedPageModel : BasePageModel
{
    [RelayCommand]
    async Task GoToLogin()
    {
        DataStorage.Remove("UserId");
        await Shell.Current.GoToAsync("//Index");
    }

    [RelayCommand]
    async Task SendReportEmailHandler()
    {
        string gmailBaseUrl = "https://mail.google.com/mail/u/0";
        string subject = "I want my ban removed!";
        string body = "write issue here!";
        string to = "douglasaubre@gmail.com";

        try
        {
            await Browser.Default.OpenAsync($"{gmailBaseUrl}/?view=cm&tf=1&to={to}&subject={subject}&body={body}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync(
                "Mail error",
                "Something went wrong while launching gmail email !",
                "Ok");
        }
    }
}
