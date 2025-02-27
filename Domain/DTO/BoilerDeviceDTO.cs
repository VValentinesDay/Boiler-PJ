
namespace Domain.DTO
{
    public class BoilerDeviceDTO
    { // ссылки по id на котельную
        public Guid Id { get; set; }
        public string? BoilerRoomName { get; set; }
        public string Type { get; set; } //фильтры, водоподготовка, щит, клапан/герметичность, насосы, сигнализатор, дэратор
                                         // может быть удачнее разместить в виде записи  - см ниже
        public string Name { get; set; }
        public int? NominalValue { get; set; }
        public string? Instruction { get; set; } // url
        public string? Notes { get; set; } // замечания по объекту
        public DateTime? Installed { get; set; }

    }
}
