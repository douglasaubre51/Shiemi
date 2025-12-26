using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Dtos;
using Shiemi.Services;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.PageModels.Dev;

public partial class EditPageModel(
    DevService devServ
) : BasePageModel
{
    private readonly DevService _devServ = devServ;

    [ObservableProperty]
    public string shortDesc = string.Empty;
    [ObservableProperty]
    public string description = string.Empty;
    [ObservableProperty]
    public string startingPrice = string.Empty;
    [ObservableProperty]
    public string advertPhotoURI = string.Empty;
    [ObservableProperty]
    public FileResult? advertPhotoResult;

    [RelayCommand]
    async Task ChoosePhoto()
    {
        if (IsBusy is true) return;
        IsBusy = true;

        try
        {
            FileResult? photoResult = await FilePicker.Default.PickAsync(
                new PickOptions
                {
                    PickerTitle = "Select an advert picture!",
                    FileTypes = FilePickerFileType.Images
                });
            if (photoResult is null)
            {
                IsBusy = false;
                return;
            }

            AdvertPhotoURI = photoResult.FullPath ?? "";
            AdvertPhotoResult = photoResult;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task GoBack()
        => await Shell.Current.GoToAsync("..");

    [RelayCommand]
    async Task Create()
    {
        if (IsBusy is true) return;
        IsBusy = true;

        if (string.IsNullOrWhiteSpace(ShortDesc) ||
            string.IsNullOrWhiteSpace(StartingPrice) ||
            string.IsNullOrWhiteSpace(Description))
        {
            IsBusy = false;
            return;
        }
        if (AdvertPhotoResult is null)
        {
            IsBusy = false;
            return;
        }

        DevDto dto = new()
        {
            ShortDesc = ShortDesc,
            StartingPrice = decimal.Parse(StartingPrice),
            Description = Description,
            UserId = UserStorage.UserId
        };
        try
        {
            bool result = await _devServ.Create(dto, AdvertPhotoResult.FullPath);
            if (result is false)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Dev Profile error",
                    "Error creating dev profile, try again later!",
                    "Ok"
                );
                IsBusy = false;
                return;
            }

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
}
