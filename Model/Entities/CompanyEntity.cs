using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class CompanyEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ContactPerson { get; set; }
        public int? ContactPersonNumber { get; set; }

        public virtual List<BoilerRoomEntity> BoilerRoomEntities { get; set; }
    }
}
