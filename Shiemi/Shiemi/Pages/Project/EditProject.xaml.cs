using Shiemi.PageModels.Project;

namespace Shiemi.Pages.Project;

public partial class EditProject : ContentPage
{
    public EditProject(EditProjectPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as EditProjectPageModel;
        context.IsPageLoading = true;
    }
}