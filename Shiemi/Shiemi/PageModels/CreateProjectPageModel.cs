using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Dtos.ProjectsDtos;
using Shiemi.Services;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.PageModels;

public partial class CreateProjectPageModel : BasePageModel
{
    private readonly ProjectService _projectService;
    private readonly UserService _userService;

    public CreateProjectPageModel(
        ProjectService projectService,
        UserService userService
        )
    {
        Title = "Create project";
        _projectService = projectService;
        _userService = userService;
    }

    [ObservableProperty]
    private string projectTitle;
    [ObservableProperty]
    private string shortDesc;
    [ObservableProperty]
    private string description;


    [RelayCommand]
    async Task Create()
    {
        Debug.WriteLine("submitted form");

        try
        {
            var user = await _userService.Get(DataStorage.Get("UserId"));
            var userIdDto = await _userService.GetUserId(user!.Id);
            int userId = userIdDto!.Id;
            var dto = new ProjectDto
            {
                Title = ProjectTitle,
                ShortDesc = ShortDesc,
                Description = Description,
                UserId = userId
            };
            await _projectService.Create(dto);

            await Shell.Current.GoToAsync("//Projects");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"CreateProject error: {ex.Message}");
        }
    }
}
