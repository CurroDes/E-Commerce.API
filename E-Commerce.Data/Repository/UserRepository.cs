using E_Commerce.Data.Data;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Repository;

public class UserRepository<T> : IUserRepository<T> where T : User
{
    private readonly ECommerceContext _context;
    public UserRepository(ECommerceContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(T userApp)
    {
        await _context.AddAsync(userApp);
    }

    public async Task<T> GetIdUserAsync(int id)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);


    }

    public async Task<T> GetUserEmailAsync(string email)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email);
    }
}
