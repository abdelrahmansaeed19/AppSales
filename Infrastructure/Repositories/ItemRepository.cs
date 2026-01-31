using Domain.Entities.Inventory;
using Infrastructure.Persistence.Contexts;
public class ItemRepository : IItemRepository
{
    private readonly AppSalesDbContext _context;

    public ItemRepository(AppSalesDbContext context)
    {
        _context = context;
    }

    public List<Item> GetAll(long tenantId)
        => _context.Items.Where(i => i.TenantId == tenantId).ToList();

    public Item GetById(long id)
        => _context.Items.FirstOrDefault(i => i.Id == id);

    public void Add(Item item)
        => _context.Items.Add(item);

    public void Update(Item item)
        => _context.Items.Update(item);

    public void Delete(Item item)
    {
     
        _context.Items.Remove(item);   
    }
    public List<Item> GetAllGlobal()
    => _context.Items.ToList();
    public void Save()
        => _context.SaveChanges();
}
