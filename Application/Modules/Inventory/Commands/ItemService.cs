using Application.Modules.Inventory.DTOs;


public class ItemService
{
    private readonly IItemRepository _repo;

    public ItemService(IItemRepository repo)
    {
        _repo = repo;
    }

    public List<ItemDto> GetAll(long tenantId)
    {
        return _repo.GetAll(tenantId)
            .Select(i => new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                SellingPrice = i.SellingPrice,
                CurrentStock = i.CurrentStock,
                IsActive = i.IsActive
            }).ToList();
    }

    public ItemDto GetById(long id)
    {
        var item = _repo.GetById(id);
        if (item == null) return null;

        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            SellingPrice = item.SellingPrice,
            CurrentStock = item.CurrentStock,
            IsActive = item.IsActive
        };
    }

    public Item Create(CreateItemDto dto)
    {
        var item = new Item
        {
            Name = dto.Name,
            SellingPrice = dto.SellingPrice,
            CurrentStock = dto.CurrentStock,
            IsActive = true
        };

        _repo.Add(item);
        _repo.Save();
        return item;
    }

    public bool Update(long id, UpdateItemDto dto)
    {
        var item = _repo.GetById(id);
        if (item == null) return false;

        item.Name = dto.Name;
        item.SellingPrice = dto.SellingPrice;
        item.IsActive = dto.IsActive;

        _repo.Update(item);
        _repo.Save();
        return true;
    }

    public bool Delete(long id)
    {
        var item = _repo.GetById(id);
        if (item == null) return false;

        _repo.Delete(item);
        _repo.Save();
        return true;
    }
}
