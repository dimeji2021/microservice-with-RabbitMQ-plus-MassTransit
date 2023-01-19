using System.ComponentModel.DataAnnotations;

namespace play.catalog.service.Dtos
{
    public record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);
    public record CreatedItemDto([Required] string Name, string Description, [Range(0, 100)] decimal Price);
    public record UpdatedItemDto([Required] string Name, string Description, [Range(0, 1000)] decimal Price);
}