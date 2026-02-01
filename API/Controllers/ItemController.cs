using Application.Modules.Inventory.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities.Inventory;
using Application.Interfaces.IRepository;

namespace App_Sales.Controllers
{
    [Route("item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        IItemRepository ItemRepository;
        IMaterialRepository MaterialRepository;
        public ItemController(IItemRepository itemRepos, IMaterialRepository materialRepos)
        {
            ItemRepository = itemRepos;
            MaterialRepository = materialRepos;

        }

        [HttpGet]
        public IActionResult GetAll(long tenantId)
        {
            var MaterialDTOs = ItemRepository.GetAll(tenantId)
            .Select(m => new MaterialResponseDto
            {
               Id = m.Id,
               Name = m.Name,
               BranchId = m.BranchId,
                TenantId = m.TenantId,
                Description = m.Description,
                CurrentQuantity = m.CurrentStock,
                MinQuantity = m.MinStockLevel,
                CostPerUnit = m.CostPrice,

                    
            })
        .ToList();
            return Ok(MaterialDTOs);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            Item item = ItemRepository.GetById(Id);
            if (item == null)
            {
                return NotFound();

            }
            return Ok(item);

        }
        [HttpPut("{id}")]
        public IActionResult Update(UpdateItemDto itemDto, long id)
        {
            var itemEntity = ItemRepository.GetById(id);
            if (itemEntity == null)
                return NotFound($"Item with ID {id} not found.");

            // Check for duplicate SKU in other items
            if (!string.IsNullOrWhiteSpace(itemDto.Sku))
            {
                var duplicateSku = ItemRepository.GetAllGlobal()
                    .Any(i => i.Sku == itemDto.Sku && i.Id != id);

                if (duplicateSku)
                    return BadRequest($"An item with SKU '{itemDto.Sku}' already exists.");
            }

            itemEntity.CategoryId = itemDto.CategoryId;
            itemEntity.Name = itemDto.Name;
            itemEntity.Description = itemDto.Description;
            itemEntity.Barcode = itemDto.Barcode;
            itemEntity.Sku = itemDto.Sku;
            itemEntity.Image = itemDto.Image;
            itemEntity.CostPrice = itemDto.CostPrice;
            itemEntity.SellingPrice = itemDto.SellingPrice;
            itemEntity.MinStockLevel = itemDto.MinStockLevel;
            itemEntity.IsActive = itemDto.IsActive;
            itemEntity.CurrentStock = itemDto.CurrentStock;

            ItemRepository.Save();
            return Ok(itemEntity);
        }

        [HttpPost]
        public IActionResult Add(CreateItemDto itemDto)
        {
            if (string.IsNullOrWhiteSpace(itemDto.Sku))
                return BadRequest("SKU is required.");

            // Check if SKU already exists globally
            var existingItem = ItemRepository.GetAllGlobal()
                .Any(i => i.Sku == itemDto.Sku);

            if (existingItem)
                return BadRequest($"An item with SKU '{itemDto.Sku}' already exists.");

            var item = new Item
            {
                CategoryId = itemDto.CategoryId,
                TenantId = itemDto.TenantId,
                BranchId = itemDto.BranchId,
                Name = itemDto.Name,
                Description = itemDto.Description,
                Barcode = itemDto.Barcode,
                Sku = itemDto.Sku,
                Image = itemDto.Image,
                CostPrice = itemDto.CostPrice,
                SellingPrice = itemDto.SellingPrice,
                CurrentStock = itemDto.CurrentStock,
                MinStockLevel = itemDto.MinStockLevel,
                IsActive = true
            };

            ItemRepository.Add(item);
            ItemRepository.Save();

            return Ok(item);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = ItemRepository.GetById(id);
            if (item == null)
                return NotFound();

            ItemRepository.Delete(item);
            ItemRepository.Save();

            return Ok("Deleted");
        }


    }

}
