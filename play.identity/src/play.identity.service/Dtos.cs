using System.ComponentModel.DataAnnotations;

namespace play.identity.service.Dtos
{
    public record UserDto(
        Guid Id,
        string Username,
        string Email,
        decimal Gil,
        DateTimeOffset CreatedDate
    );

    public record UpdateUserDtos(
    [Required, EmailAddress]string Email,
    [Range(0, 1000000)]decimal Gil
    );
}