namespace Shiemi.ViewModels;

public record ChatViewModel(
    int Id,
    string Title
);

public record ChatRoomViewModel(
    int RoomId,
    int SenderId,
    string Title
);

public record ChatMessageViewModel(
		string Text,
		DateTime SentAt,
		bool IsOwner = false
);
	
