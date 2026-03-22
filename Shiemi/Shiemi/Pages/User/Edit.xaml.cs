using Shiemi.PageModels.User;
using Shiemi.Services;

namespace Shiemi.Pages.User;

public partial class Edit : ContentPage
{
    private readonly UserService _userServ;

    public Edit(
        EditPageModel pageModel,
        UserService userServ)
    {
        InitializeComponent();
        BindingContext = pageModel;
        _userServ = userServ;
    }

    protected async override void OnAppearing()
    {
        try
        {
            EditPageModel context = (EditPageModel)BindingContext;
            context.FirstName = context.CurrentUser!.FirstName;
            context.LastName = context.CurrentUser!.LastName;
            context.OptionalUserDetails = await _userServ.GetOptionalDetails(context.CurrentUser.Id);

            context.CustomProgressBar = ProgressView;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"EditPage: OnAppearingError: {ex.Message}");
        }

        base.OnAppearing();
    }
}