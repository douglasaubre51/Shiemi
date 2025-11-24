using AutoMapper;
using Shiemi.Dtos;
using Shiemi.PageModels.Chat;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;
using System.Diagnostics;

namespace Shiemi.Pages.Chats;

public partial class Channels : ContentPage
{
    private readonly ProjectService _projectService;

    public Channels(
        ChannelsPageModel pageModel,
        ProjectService projectService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _projectService = projectService;
    }

    protected override async void OnAppearing()
    {
        ChannelsPageModel? context = BindingContext as ChannelsPageModel;
        if (context is null)
        {
            Debug.WriteLine("Channels: OnAppearing error: Context is null!");
            return;
        }

        try
        {
            List<ProjectDto>? dtos = await _projectService.GetAllByUser(UserStorage.UserId);
            if (dtos is null)
            {
                Debug.WriteLine("Channels: OnAppearing error: ProjectDtos is null!");
                return;
            }

            // load projects list
            Mapper? mapper = MapperProvider.GetMapper<ProjectDto, ChatListProjectViewModel>();
            if (mapper is null)
            {
                Debug.WriteLine("Channels: OnAppearing: error: GetMapper returned null!");
                return;
            }

            // clear and flush ProjectCollection on ProjectListView
            var projectViewModels = mapper.Map<List<ChatListProjectViewModel>>(dtos);
            context.ProjectCollection.Clear();
            context.ProjectCollection.AddRange(projectViewModels);

            foreach (var p in projectViewModels)
            {
                Debug.WriteLine($"project title: {p.Title}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Channels: OnAppearing: error: {ex.Message}");
        }
    }
}