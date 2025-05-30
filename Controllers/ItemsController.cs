using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using inventory_manager.Models;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ItemDao _itemDao;

    public ItemsController(ItemDao itemDao)
    {
        _itemDao = itemDao;
    }

    [HttpGet]
    public async Task<ActionResult<List<Item>>> GetItems(){
        var items = await _itemDao.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(int id){
        var item = await _itemDao.GetByIdAsync(id);
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem([FromBody] Item item){
        var newItem = await _itemDao.CreateAsync(item);
        if (newItem == null) return BadRequest();
        return Ok(newItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] Item item){
        var newItem = await _itemDao.UpdateAsync(item);
        if (newItem == null) return BadRequest();
        return Ok(newItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id){
        await _itemDao.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetItemCount(){
        var count = await _itemDao.GetItemCountAsync();
        return Ok(count);
    }

    [HttpGet("total-price")]
    public async Task<ActionResult<decimal>> GetTotalPrice(){
        var totalPrice = await _itemDao.GetTotalPriceAsync();
        return Ok(totalPrice);
    }
}
