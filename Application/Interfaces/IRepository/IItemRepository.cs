using Domain.Entities.Inventory;

public interface IItemRepository
{
   public List<Item> GetAll(long tenantId);
   public Item GetById(long id);
    public List<Item> GetAllGlobal();
    public void Add(Item item);

    public  void Update(Item item);
    public  void Delete(Item item);
    public void Save();

}
