namespace Shiemi.Models;

public class DevModel
{
    public int UserId { get; set; }
    public string Advert { get; set; } = string.Empty;
    public string ShortDesc { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;
    public decimal StartingPrice { get; set; }
}
