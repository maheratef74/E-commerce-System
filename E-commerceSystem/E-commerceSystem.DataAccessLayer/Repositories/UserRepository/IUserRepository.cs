using E_commerceSystem.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_commerceSystem.DataAccessLayer.Repositories.UserRepository;

public interface IUserRepository 
{ 
    Task<User?> GetUserByEmailAsync(string email);
    Task ReduceBalance(double balance , string userEmail);
}