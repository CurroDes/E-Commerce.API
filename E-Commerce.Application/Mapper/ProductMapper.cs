using E_Commerce.Application.DTOs;
using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper;

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

    public Product MapToPutProduct(Product p, ProductDTO pd)
    {

        if (p.Quantity != 0)
        {
            p.Quantity += pd.Quantity;
        }

        p.Price = pd.Price;

        if (p.Quantity >= 1)
        {
            p.Stock = 1;
        }

        return p;
    }

}