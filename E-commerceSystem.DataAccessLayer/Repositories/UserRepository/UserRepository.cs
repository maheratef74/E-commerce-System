using Microsoft.AspNetCore.Identity;
using E_commerceSystem.DataAccessLayer.DbContext;
using E_commerceSystem.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_commerceSystem.DataAccessLayer.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
       return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task ReduceBalance(double balance, string userEmail)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        if (user is not null && user.Balance >= balance)
        {
            user.Balance -= balance;
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
        }
    }
}