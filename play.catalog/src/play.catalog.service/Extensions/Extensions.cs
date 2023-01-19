using play.catalog.service.Dtos;
using play.catalog.service.Entities;

namespace play.catalog.service // Note this
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item) // Received an instance of an item entity
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}