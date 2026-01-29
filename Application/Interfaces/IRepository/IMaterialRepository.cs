using Domain.Entities.Inventory;

namespace Application.Interfaces.IRepository
{
    public interface IMaterialRepository
    {
        List<Material> GetAll(long tenantId);
        Material GetById(long id);
        void Add(Material material);
        void Update(Material material);
        void Delete(Material material);
        void Save();
    }

}