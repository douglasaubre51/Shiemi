using Shiemi.Dtos;
using Shiemi.PageModels.Chat;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.Utilities.HubClients;
using System.Diagnostics;

namespace Shiemi.Pages.Chats;

public partial class Channels : ContentPage
{
    private readonly ProjectClient _projectClient;
    private readonly ProjectService _projectService;

    public Channels(
        ChannelsPageModel pageModel,
        ProjectClient projectClient,
        ProjectService projectService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _projectClient = projectClient;
        _projectService = projectService;
    }

    protected override async void OnAppearing()
    {
        var context = BindingContext as ChannelsPageModel;
        try
        {
            List<ProjectDto>? dtos = await _projectService.GetAllByUser(UserStorage.UserId);
            if (dtos is null)
            {
                Debug.WriteLine("ProjectDtos is null!");
                return;
            }
            // load projects list
            context!.ProjectCollection.AddRange(dtos);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Channels: OnAppearing: error: {ex.Message}");
        }
    }
}