using App_Sales.Data;
using App_Sales.Models;
using App_Sales.Models.Inventory;
using System;

public class MaterialRepository : IMaterialRepository
{
    private readonly App_Context _context;

    public MaterialRepository(App_Context context)
    {
        _context = context;
    }

    public List<Material> GetAll(long tenantId)
        => _context.material.Where(m => m.TenantId == tenantId).ToList();

    public Material GetById(long id)
        => _context.material.FirstOrDefault(m => m.Id == id);

    public void Add(Material material)
        => _context.material.Add(material);

    public void Update(Material material)
        => _context.material.Update(material);

    public void Delete(Material material)
        => _context.material.Remove(material);

    public void Save()
        => _context.SaveChanges();
}
