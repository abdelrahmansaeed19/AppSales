using App_Sales.Models;
using App_Sales.Models.Inventory;

public interface IItemRepository
{
   public List<Item> GetAll(long tenantId);
   public Item GetById(long id);
    public void Add(Item item);

    public  void Update(Item item);
    public  void Delete(Item item);
    public void Save();
}
