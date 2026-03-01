namespace Shiemi.Dtos;

public record UserDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string ProfilePhotoURL
);

public record ProfilePageUserDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string UserId,
    bool IsDeveloper,
    string ProfilePhotoURL
);

public record UserDetailsDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string? ProfilePhotoURL
);
