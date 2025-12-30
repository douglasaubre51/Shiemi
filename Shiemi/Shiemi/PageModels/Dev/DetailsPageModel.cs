using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using Shiemi.Models;
using Shiemi.ViewModels;

namespace Shiemi.PageModels.Dev;

[QueryProperty(nameof(CurrentDev), "CurrentDev")]
public partial class DetailsPageModel : BasePageModel
{
	[ObservableProperty]
		private DevModel? currentDev;

	[ObservableProperty]
		private bool isSendingChat;
	[ObservableProperty]
		private string sendChatText = string.Empty;
	[ObservableProperty]
		private ObservableRangeCollection<ChatMessageViewModel> messages = [];

	async partial void OnIsSendingChatChanged(bool value)
		=> await SendingChat();

	async Task SendingChat()
	{
		try
		{	if(IsSendingChat is false) return;

			Debug.WriteLine("is sending chat...");
			Debug.WriteLine(SendChatText);
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		finally
		{
			IsSendingChat = false;
		}
	}
}
