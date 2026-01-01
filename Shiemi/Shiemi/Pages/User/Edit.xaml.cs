using System.Diagnostics;
using Shiemi.PageModels.User;

namespace Shiemi.Pages.User;

public partial class Edit : ContentPage
{
    public Edit(EditPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnAppearing()
    {
        try
        {
            EditPageModel context = (EditPageModel)BindingContext;
            context.FirstName = context.CurrentUser!.FirstName;
            context.LastName = context.CurrentUser!.LastName;
            context.CustomProgressBar = ProgressView;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"EditPage: OnAppearingError: {ex.Message}");
        }

        base.OnAppearing();
    }
}