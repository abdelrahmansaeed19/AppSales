using App_Sales.DTO.InventoryDTO.MaterialsDTO;
using Microsoft.AspNetCore.Mvc;

namespace App_Sales.Controllers
{
    [ApiController]
    [Route("api/materials")]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialService _materialService;

        public MaterialController(MaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            long tenantId = 1;
            var materials = _materialService.GetAll(tenantId);
            return Ok(materials);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var material = _materialService.GetById(id);
            if (material == null) return NotFound();
            return Ok(material);
        }

        [HttpPost]
        public IActionResult Create(CreateMaterialDto dto)
        {
            var material = _materialService.Create(dto);
            return Ok(material);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateMaterialdto dto)
        {
            var updated = _materialService.Update(id, dto);
            if (!updated) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var deleted = _materialService.Delete(id);
            if (!deleted) return NotFound();
            return Ok("Deleted");
        }
    }
}
