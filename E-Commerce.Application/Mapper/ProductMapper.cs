using E_Commerce.Application.DTOs;
using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper
{
    public class ProductMapper
    {
        public ProductMapper()
        {

        }

        public Product MapToProduct(ProductDTO p)
        {
            return new Product
            {
                NameProduct = p.NameProduct,
                Description = p.Description,
                RegistrationDate = DateTime.Now,
                Stock = p.Stock,
                Price = p.Price,
                Quantity = p.Quantity
            };

        }
    }
}
