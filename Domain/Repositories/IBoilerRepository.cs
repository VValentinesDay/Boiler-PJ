using Domain.DTO;

namespace Domain.Repositories
{
    public interface IBoilerRepository
    {
        public Task<List<BoilerRoomDTO>> GetAllBoiler();
        public Task<BoilerRoomDTO> GetBoilerId(Guid id);
        public Task<BoilerRoomDTO> GetBoilerName(string name);
        
        
        public Task<Guid> CreateBoiler(BoilerRoomDTO boiler);



        public Task<Guid> UpdateBoiler(string name, string discription, string adress); // если аргумент - целая сущность то
        // придётся обязятаельно заполнять все поля => придётся придумать метод решающий это
        // прощё прописать поля, причём сделать их необязательными для заполнения
        // Можно прописать при обновлении что если поле нал то свойство не трогать,
        // тогда это будет что-то в духе цикла с проверкой полей. Вроде просто
        public Task<string> DeleteBoiler(string name);

        

    
        
    }
}
