using Shiemi.Models;

namespace Shiemi.PageModels;

[QueryProperty(nameof(_project), "Project")]
public class PrivateRoomPageModel : BasePageModel
{
    private Project _project;

    public PrivateRoomPageModel()
    {
        Title = "Private Room";
    }
}
