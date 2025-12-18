using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Dtos;
using Shiemi.Models;
using Shiemi.Services;
using Shiemi.Storage;

namespace Shiemi.PageModels.Dev;

public partial class EditPageModel(
    DevService devServ
) : BasePageModel
{
    private readonly DevService _devServ = devServ;

    [ObservableProperty]
    public string shortDesc = string.Empty;
    [ObservableProperty]
    public string startingPrice = string.Empty;

    [RelayCommand]
    async Task Create()
    {
        if (string.IsNullOrWhiteSpace(ShortDesc) || string.IsNullOrWhiteSpace(StartingPrice))
            return;

        DevDto dto = new()
        {
            ShortDesc = ShortDesc,
            StartingPrice = decimal.Parse(StartingPrice),
            UserId = UserStorage.UserId
        };
        try
        {
            await _devServ.SetDeveloper(UserStorage.UserId);
            bool result = await _devServ.Create(dto);
            if (result is false)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Dev Profile error",
                    "Error creating dev profile, try again later!",
                    "Ok"
                );
                return;
            }

            await Shell.Current.GoToAsync(
                ".."
            );
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
