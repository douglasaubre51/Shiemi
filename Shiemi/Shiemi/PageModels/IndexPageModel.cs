using CommunityToolkit.Mvvm.ComponentModel;

namespace Shiemi.PageModels
{
    public partial class IndexPageModel : BasePageModel
    {
        [ObservableProperty]
        private bool isCanceled;

        public IndexPageModel()
        {
            Title = "Start";
        }
    }
}
