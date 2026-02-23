using Shiemi.PageModels.Chat;
using Shiemi.Services;

namespace Shiemi.Pages.Chats;

public partial class Channels : ContentPage
{
    private readonly ProjectService _projectService;

    public Channels(ChannelsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnDisappearing()
    {
        ChannelsPageModel? pageModel = BindingContext as ChannelsPageModel;
        pageModel!.IsPageExiting = true;

        base.OnDisappearing();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        ChannelsPageModel? pageModel = BindingContext as ChannelsPageModel;
        pageModel!.IsPageLoading = true;
    }
}