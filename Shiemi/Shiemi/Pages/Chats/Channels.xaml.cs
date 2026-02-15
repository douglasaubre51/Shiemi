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
}