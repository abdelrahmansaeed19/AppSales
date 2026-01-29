using App_Sales.DTO.InventoryDTO.ItemDTO;
using App_Sales.DTO.InventoryDTO.MaterialsDTO;
using App_Sales.Models;
using App_Sales.Models.Inventory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_Sales.Controllers
{
    [Route("api/[controller]")]
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
            var MaterialDTOs = MaterialRepository.GetAll(tenantId)
            .Select(m => new MaterialResponseDto
            {
               Id = m.Id,
               Name = m.Name,
               Description = m.Description,
               Unit = m.Unit,
               CurrentQuantity = m.CurrentQuantity,
               MinQuantity = m.MinQuantity,
               CostPerUnit = m.CostPerUnit,
               ExpiryDate = m.ExpiryDate
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
        [HttpPut("{Id}")]
        public IActionResult Update(UpdateItemDto itemDto, long id)
        {
            var itemEntity = ItemRepository.GetById(id);
            if (itemEntity == null)
            {
                return NotFound($"Order with ID {id} not found.");
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

            ItemRepository.Save();
            return Ok();
        }
        [HttpPost]
        public IActionResult Add(CreateItemDto itemDto)
        {
            var item = new Item
            {
                CategoryId = itemDto.CategoryId,
                Name = itemDto.Name,
                Description = itemDto.Description,
                Barcode = itemDto.Barcode,
                Sku = itemDto.Sku,
                Image = itemDto.Image,
                CostPrice = itemDto.CostPrice,
                SellingPrice = itemDto.SellingPrice,
                MinStockLevel = itemDto.MinStockLevel,
               


            };
            ItemRepository.Add(item);
            ItemRepository.Save();


            return Ok();

        }

            [HttpDelete]
        public IActionResult Delete(long id)
        {

            var item = ItemRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            ItemRepository.Delete(item);
            ItemRepository.Save();

            return Ok("Deleted");

        }

    }

}
