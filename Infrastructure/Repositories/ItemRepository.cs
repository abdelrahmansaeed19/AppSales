using App_Sales.Data;
using App_Sales.Models;
using App_Sales.Models.Inventory;
using System;

public class ItemRepository : IItemRepository
{
    private readonly App_Context _context;

    public ItemRepository(App_Context context)
    {
        _context = context;
    }

    public List<Item> GetAll(long tenantId)
        => _context.item.Where(i => i.TenantId == tenantId).ToList();

    public Item GetById(long id)
        => _context.item.FirstOrDefault(i => i.Id == id);

    public void Add(Item item)
        => _context.item.Add(item);

    public void Update(Item item)
        => _context.item.Update(item);

    public void Delete(Item item)
    {
     
        _context.item.Remove(item);


        
    }
    public void Save()
        => _context.SaveChanges();
}
