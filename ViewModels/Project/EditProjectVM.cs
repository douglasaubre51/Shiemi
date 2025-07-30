using MongoDB.Bson;
using Shiemi.Models;

namespace Shiemi.ViewModels.Project
{
    [QueryProperty("Project", "Project")]
    public partial class EditProjectVM : BaseVM
    {
        [ObservableProperty]
        ProjectModel project;

        [ObservableProperty]
        string title;
        [ObservableProperty]
        string shortDescription;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        Decimal128 price;


        public EditProjectVM()
        {
        }
    }
}
