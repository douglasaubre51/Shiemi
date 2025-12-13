using Shiemi.Models;

namespace Shiemi.ViewModels;

public class ReviewCardViewModel
{
    public bool AllowedToWrite { get; set; }
    public List<Review>? Reviews { get; set; }
}
