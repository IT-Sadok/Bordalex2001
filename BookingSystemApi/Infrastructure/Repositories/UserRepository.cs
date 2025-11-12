using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository
{
    public Task<User?> GetByEmailAsync(string email) => db.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task AddUserAsync(User user)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();
    }
}
