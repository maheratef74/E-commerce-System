using Microsoft.AspNetCore.Identity;

namespace E_commerceSystem.DataAccessLayer.Entities;

public class User
{
    public int Id { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public double Balance { get; set; } = 100.00;
}