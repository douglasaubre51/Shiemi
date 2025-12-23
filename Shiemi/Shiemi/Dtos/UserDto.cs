namespace Shiemi.Dtos;

public record UserDto(
    int Id,
    string FirstName,
    string LastName,
    string Email
);

public record ProfilePageUserDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string UserId,
    bool IsDeveloper
);

