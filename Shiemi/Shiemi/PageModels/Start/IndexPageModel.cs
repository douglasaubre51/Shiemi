using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiemi.Storage;
using System.Diagnostics;

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

        [RelayCommand]
        async Task LogoutPressed()
        {
            IsBusy = true;
            Debug.WriteLine("removing UserId ...");
            DataStorage.Remove("UserId");
            IsBusy = false;
            return;
        }
    }
}
