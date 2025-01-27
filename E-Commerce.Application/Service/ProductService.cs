using E_Commerce.Application.DTOs;
using E_Commerce.Application.Interfaces;
using E_Commerce.Application.Mapper;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service;

public class ProductService : IProductService
{
    private readonly ProductMapper _productMapper;
    private readonly ILogger<ProductService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository<Product> _productRepository;
    public ProductService(ProductMapper productMapper, ILogger<ProductService> logger, IUnitOfWork unitOfWork, IProductRepository<Product> productRepository)
    {
        _productMapper = productMapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<Result> AddProductService(ProductDTO p)
    {
        Result result = new Result();

        result.Success = true;

        try
        {
            var productApp = _productMapper.MapToProduct(p);

            if (productApp == null)
            {
                result.Success = false;
                result.Error = $"Error al intentar añadir el producto {p.NameProduct}";
                _logger.LogError(result.Error.ToString());

                return result;
            }

            await _productRepository.AddProductAsync(productApp);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            var time = DateTime.Now;
            result.Text = $"Se ha añadido correctamente el producto: {p.NameProduct} a las {time}";
            _logger.LogInformation(result.Text.ToString());

        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = $"Error al intentar añadir el producto {p.NameProduct}";
            _logger.LogError(ex.ToString(), result.Error.ToString());

            await _unitOfWork.RollbackAsync();
        }

        return result;
    }

    public async Task<Result> PutProductService(int id, ProductDTO p)
    {
        Result result = new Result();

        result.Success = true;

        try
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product.Id != p.IdProduct)
            {
                result.Success = false;
                result.Error = $"Error al modificar el producto {p.NameProduct} con la cantidad de productos {p.Quantity}";
                _logger.LogError(result.Error.ToString());

                return result;
            }

            //Mapeamos la modificación del producto.
            var productMapper = _productMapper.MapToPutProduct(product, p);

            await _productRepository.ModifyProductNormalAsync(productMapper);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = $"Error al modifcar el producto {p.NameProduct}";
            _logger.LogError(ex.ToString(), result.Error.ToString());

            await _unitOfWork.RollbackAsync();
        }

        return result;
    }
}