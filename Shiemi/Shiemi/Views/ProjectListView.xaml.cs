using MvvmHelpers;
using Shiemi.Dtos;

namespace Shiemi.Views;

public partial class ProjectListView : VerticalStackLayout
{
    public static readonly BindableProperty ProjectCollectionProperty = BindableProperty.Create(
        nameof(ProjectCollection),
        typeof(ObservableRangeCollection<ProjectDto>),
        typeof(ProjectListView),
        new ObservableRangeCollection<ProjectDto>(),
        propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            var context = bindable as ProjectListView;
            context!.ProjectCollectionView.ItemsSource = newvalue as ObservableRangeCollection<ProjectDto>;
        }
        );

    public ObservableRangeCollection<ProjectDto> ProjectCollection
    {
        get => (ObservableRangeCollection<ProjectDto>)GetValue(ProjectCollectionProperty);
        set => SetValue(ProjectCollectionProperty, value);
    }

    public ProjectListView()
        => InitializeComponent();
}