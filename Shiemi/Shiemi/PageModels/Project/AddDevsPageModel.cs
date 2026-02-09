using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Shiemi.Dtos;
using Shiemi.Models.ProjectModels;
using Shiemi.Services;
using Shiemi.Utilities.ServiceProviders;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Project;

[QueryProperty(nameof(CurrentProject), "CurrentProject")]
public partial class AddDevsPageModel(ProjectService projectServ) : BasePageModel
{
    private bool firstPageLoad = true;

    [ObservableProperty]
    private ProjectsPageProjectViewModel? currentProject;

    private readonly ProjectService _projectServ = projectServ;

    [ObservableProperty]
    private bool isPageLoading;

    [ObservableProperty]
    private ObservableRangeCollection<DeveloperCardModel> developerCardCollection = [];

    [RelayCommand]
    async Task RemoveDevFromProject(int clientId)
    {
        try
        {
            await _projectServ.RemoveDevFromProject(CurrentProject!.Id, clientId);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    [RelayCommand]
    async Task AddDevToProject(int clientId)
    {
        try
        {
            await _projectServ.AddDevToProject(CurrentProject!.Id, clientId);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    async partial void OnCurrentProjectChanged(ProjectsPageProjectViewModel? value)
    {
        try
        {
            if (firstPageLoad is false) return;

            var devUsers = await _projectServ.GetAllPotentialCandidates(CurrentProject!.Id);
            if (devUsers is null || devUsers.Count is 0) return;

            var mapper = MapperProvider.GetMapper<UserDetailsDto, DeveloperCardModel>();
            var developerCards = mapper!.Map<List<DeveloperCardModel>>(devUsers);
            DeveloperCardCollection.AddRange(developerCards);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            firstPageLoad = false;
        }
    }
}

