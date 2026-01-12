using CommunityToolkit.Mvvm.ComponentModel;
using Shiemi.Models;
using Shiemi.Utilities.HubClients;

namespace Shiemi.PageModels.Dev;

[QueryProperty(nameof(CurrentClient), "CurrentClient")]
public partial class ChatPageModel(
	RoomClient roomClient
): BasePageModel
{
	private readonly RoomClient _roomClient = roomClient;
    [ObservableProperty]
    private ProfileCardModel currentClient;

	//[ObservableProperty]
	//private ObservableRangeCollection<MessageViewModel> messages = [];

	//[ObservableProperty]
	//private bool isPageLoading;
	//[ObservableProperty]
	//private bool isPageExiting;

	//async partial void OnIsPageLoadingChanged(bool value)
	//{
	//	try
	//	{
	//		if(value is false) return;
			
	//		_roomClient.InitSignalR)
			
}
