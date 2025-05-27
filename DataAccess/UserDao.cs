using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using inventory_manager.Models;

public class UserDao{
    private readonly InventoryManagerContext _context;

    public UserDao(InventoryManagerContext context){
        _context = context;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> Login(string email, string password)
    {
        User currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (currentUser != null)
        {
            string hashedPassword = ComputeSha256Hash(password);

            if (currentUser.Pass == hashedPassword)
            {
                return currentUser;
            }
        }
        return null;
    }

    public async Task<User?> CreateUserAsync(string email, string password)
    {
        var existingUser = await GetByEmailAsync(email);
        if (existingUser == null)
        {
            string hashedPassword = ComputeSha256Hash(password);
            User user = new User
            {
                Email = email,
                Pass = hashedPassword
            };

            await AddAsync(user);
            return user;
        }

        return null;
    }

    public async Task<User?> Recovery(string email, string newPassword){
        User currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (currentUser != null){
            currentUser.Pass = ComputeSha256Hash(newPassword);
            await UpdateAsync(currentUser);
            return currentUser;
        }
        return null;
    }

    private string ComputeSha256Hash(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            StringBuilder builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}

