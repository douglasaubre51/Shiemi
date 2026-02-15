using Shiemi.Dtos;
using Shiemi.Models;
using Shiemi.Services;
using Shiemi.Utilities.ServiceProviders;

namespace Shiemi.Utilities.Handlers;

public class DevSearchHandler : SearchHandler
{
    private readonly DevService _devService;

    public object? PageContext { get; set; }
    public List<SearchDevDto> SearchProjectsCollection { get; set; } = [];

    public DevSearchHandler()
    {
        _devService = Provider.GetService<DevService>()!;
    }

    protected async override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        try
        {
            Debug.WriteLine($"text: {newValue}");
            List<SearchDevDto>? results = await _devService.GetSearchedDevs(newValue);
            if (results is null || results.Count == 0)
                return;

            ItemsSource = results;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    protected async override void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        try
        {
            SearchDevDto selectedDev = (SearchDevDto)item;

            DevDto? dbDev = await _devService.GetById(selectedDev.DevId);
            if (dbDev is null) return;

            var viewModel = new DevModel
            {
                Id = selectedDev.DevId,
                Username = dbDev.Username,
                Advert = dbDev.Advert,
                ShortDesc = dbDev.ShortDesc,
                Profile = dbDev.Profile,
                StartingPrice = dbDev.StartingPrice,
            };

            await Shell.Current.GoToAsync(
                "DetailsDev",
                true,
                new Dictionary<string, object>
                {
                    { "CurrentDev", viewModel }
                });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
