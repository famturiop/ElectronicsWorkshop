
namespace ElectronicsWorkshop.Core.Domain.DTOs
{
    public class BaseDeviceReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
