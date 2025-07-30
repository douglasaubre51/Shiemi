using Shiemi.ViewModels.Project;

namespace Shiemi.Views.Project;

public partial class EditProjectView : ContentPage
{
    public EditProjectView(EditProjectVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var model = BindingContext as EditProjectVM;

        model.Title = model.Project.Title;
        model.ShortDescription = model.Project.ShortDescription;
        model.Description = model.Project.Description;
        model.Price = model.Project.Price;
    }
}