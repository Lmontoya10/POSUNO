using Microsoft.EntityFrameworkCore;
using POSUNO.api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSUNO.api.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckUserAsync();    
            await CheckCustomersAsync();    
            await CheckProductsAsync();    
        }

        private async Task CheckUserAsync()
        {
            if (_context.Users.Any())
            {
                _context.Users.Add(new Entities.User { Email = "lmontoya@yopmail.com", FirstName = "Luisa", LastName = "Montoya", Password = "123456" });
                _context.Users.Add(new Entities.User { Email = "crestrepo@yopmail.com", FirstName = "Cristian", LastName = "Restrepo", Password = "123456" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Users.Any())
            {
                Random random = new Random();
                User user = await _context.Users.FirstOrDefaultAsync();
                for (int i = 1; i < 200; i++)
                {
                    _context.Products.Add(new Product { Name = $"Producto {i}", Description = $"producto {i}", Price = random.Next(5, 1000), Stock = random.Next(0, 500),User = user, IsActive= true });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCustomersAsync()
        {
            if (!_context.Customers.Any())
            {
                Random random = new Random();
                User user = await _context.Users.FirstOrDefaultAsync();
                for (int i = 1; i < 50; i++)
                {
                    _context.Customers.Add(new Customer {FirstName=$"Cliente {i}", LastName=$"Apellido {i}", Phonenumber="3223114620", Address="Cr 1 2 3", IsActive=true, User=user , Email=$"cliente{i}@yopmail.com"});
                }

                await _context.SaveChangesAsync();
            }
        }

  
    }

}
