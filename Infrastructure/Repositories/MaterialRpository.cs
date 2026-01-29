using Domain.Entities.Inventory;
using Infrastructure.Persistence.Contexts;
using Application.Interfaces.IRepository;

public class MaterialRepository : IMaterialRepository
{
    private readonly AppSalesDbContext _context;

    public MaterialRepository(AppSalesDbContext context)
    {
        _context = context;
    }

    public List<Material> GetAll(long tenantId)
        => _context.Materials.Where(m => m.TenantId == tenantId).ToList();

    public Material GetById(long id)
        => _context.Materials.FirstOrDefault(m => m.Id == id);

    public void Add(Material material)
        => _context.Materials.Add(material);

    public void Update(Material material)
        => _context.Materials.Update(material);

    public void Delete(Material material)
        => _context.Materials.Remove(material);

    public void Save()
        => _context.SaveChanges();
}
