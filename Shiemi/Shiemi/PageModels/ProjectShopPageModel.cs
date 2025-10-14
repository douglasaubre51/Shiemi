using Shiemi.Models;
using System.Collections.ObjectModel;

namespace Shiemi.PageModels;

public partial class ProjectShopPageModel : BasePageModel
{
    public ObservableCollection<Project> ProjectCollection { get; set; } = new();

    public ProjectShopPageModel()
    {
        Title = "Project Shop";
    }
}
