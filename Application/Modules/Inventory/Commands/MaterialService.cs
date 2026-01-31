using Application.Modules.Inventory.DTOs;

using Domain.Entities.Inventory;

using Application.Interfaces.IRepository;


public class MaterialService
{
    private readonly IMaterialRepository _repo;

    public MaterialService(IMaterialRepository repo)
    {
        _repo = repo;
    }

    public List<MaterialResponseDto> GetAll(long tenantId)
    {
        return _repo.GetAll(tenantId)
            .Select(m => new MaterialResponseDto
            {
                Id = m.Id,
                Name = m.Name,
                Unit = m.Unit,
                CurrentQuantity = m.CurrentQuantity,
                MinQuantity = m.MinQuantity,
                BranchId= m.BranchId,
                TenantId= m.TenantId,
                CostPerUnit = m.CostPerUnit,
                Description = m.Description,
                ExpiryDate = m.ExpiryDate


            }).ToList();
    }

    public Material GetById(long id)
        => _repo.GetById(id);

    public Material Create(CreateMaterialDto dto)
    {
        var material = new Material
        {
            Name = dto.Name,
            Unit = dto.Unit,
            CurrentQuantity = dto.CurrentQuantity,
            MinQuantity = dto.MinQuantity,
            CostPerUnit = dto.CostPerUnit,
            BranchId = dto.BranchId,
            TenantId = dto.TenantId,
            Description = dto.Description,
            ExpiryDate = dto.ExpiryDate

        };

        _repo.Add(material);
        _repo.Save();
        return material;
    }

    public bool Update(long id, UpdateMaterialdto dto)
    {
        var material = _repo.GetById(id);
        if (material == null) return false;

        material.Name = dto.Name;
        material.Unit = dto.Unit;
        material.CurrentQuantity = dto.CurrentQuantity;
        material.MinQuantity = dto.MinQuantity;
        material.CostPerUnit = dto.CostPerUnit;
        material.Description = dto.Description;
        material.ExpiryDate = dto.ExpiryDate;
        material.BranchId = dto.BranchId;
        material.TenantId = dto.TenantId;



        _repo.Update(material);
        _repo.Save();
        return true;
    }

    public bool Delete(long id)
    {
        var material = _repo.GetById(id);
        if (material == null) return false;

        _repo.Delete(material);
        _repo.Save();
        return true;
    }
}
