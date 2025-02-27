namespace Model.Entities
{
    public class BoilerRoomEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Adress { get; set; }
        //public string Company { get; set; } // задумано как ссылка на компанию? тогда FK - имя компнаии, тогда здесь этого
        //// поля не должно быть 
        // UPD: задумка оказалась верной, спустя время понадообилось поле для компании - а его не оказалось
        public string? CompanyName { get; set; }


        //public string Devices { get; set; } // задумано как ссылка на оборудования => ссылка на связующую таблицу 
        //// 
        public virtual List<BoilerDevicesEntity>? BoilerDevices { get; set; }
        public virtual CompanyEntity? Company { get; set; }


    }
}
