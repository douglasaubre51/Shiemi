using Shiemi.Models;
using Shiemi.PageModels.Market;
using Shiemi.Services;
using Shiemi.Storage;
using System.Diagnostics;

namespace Shiemi.Pages.Market;

public partial class ProjectDetails : ContentPage
{
    private readonly ReviewService _reviewService;

    public ProjectDetails(
        ProjectDetailsPageModel pageModel,
        ReviewService reviewService
        )
    {
        InitializeComponent();
        BindingContext = pageModel;
        _reviewService = reviewService;
    }

    protected override async void OnAppearing()
    {
        var context = BindingContext as ProjectDetailsPageModel;

        // disable btn for non owners !
        if (context!.ProjectVM.UserId == UserStorage.UserId)
            context.NotOwner = false;

        try
        {
            IAsyncEnumerable<Review?> reviews = _reviewService.GetReviewsByProject(context.ProjectVM.Id);
            if (reviews is null)
                return;
            // flush reviews to review cards collection !
            context.ReviewList.Clear();
            await foreach (var r in reviews)
                context.ReviewList.Add(r!);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ProjectDetails: OnAppearing: error: {ex.Message}");
        }
    }
}