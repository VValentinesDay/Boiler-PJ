using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class BoilerDevicesEntity
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
        public DateTime? Updated { get; set; }

        public virtual BoilerRoomEntity? BoilerRoom { get; set; }
    }

    //public record DeviceType
    //{
    //    string Burner { get; set; }
    //    string Boiler { get; set; }
    //    string Valve { get; set; }
    //    string Pump { get; set; }
    //    string Alarm { get; set; }
    //    string WaterDevice { get; set; }
    //}
}
