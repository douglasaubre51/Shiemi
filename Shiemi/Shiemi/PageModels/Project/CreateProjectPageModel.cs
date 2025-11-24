using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Dtos;
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
        try
        {
            var dto = new CreateProjectDto(
                Title,
                ShortDesc,
                Description,
                UserStorage.UserId
                );
            await _projectService.Create(dto);

            await Shell.Current.GoToAsync("//Projects");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"CreateProject error: {ex.Message}");
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Failed to create project!",
                "Ok"
                );
        }
    }
}
