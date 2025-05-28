using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using inventory_manager.Models;


public class ItemDao{
    private readonly InventoryManagerContext _context;

    public ItemDao(InventoryManagerContext context){
        _context = context;
    }

    public async Task<Item?> GetByIdAsync(int id){
        return await _context.Items.FindAsync(id);
    }

    public async Task<Item?> UpdateAsync(Item item){
        var itemToUpdate = await GetByIdAsync(item.Id);
        if (itemToUpdate != null){
            itemToUpdate.Name = item.Name;
            itemToUpdate.Price = item.Price;
            _context.Items.Update(itemToUpdate);
            await _context.SaveChangesAsync();
            return itemToUpdate;
        }
        return null;
    }

    public async Task<List<Item>> GetAllAsync(){
        return await _context.Items.ToListAsync();
    }

    public async Task<Item?> CreateAsync(Item item){
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task DeleteAsync(int id){
        var ItemToDelete = await GetByIdAsync(id);
        if (ItemToDelete != null){
            _context.Items.Remove(ItemToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
