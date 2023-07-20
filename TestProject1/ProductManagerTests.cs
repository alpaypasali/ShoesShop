using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ShoeShop.Businness.Concrete;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.Entities.Concrete;
using System;
using ShoeShop.Dtos.Concrete;
using AutoMapper;

public class ProductManagerTests
{
    private Mock<IProductRepository> _mockProductRepository;
    private Mock<IMapper> _mockMapper;
    private ProductManager _productManager;

    public ProductManagerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockMapper = new Mock<IMapper>();
        _productManager = new ProductManager(_mockProductRepository.Object, _mockMapper.Object);
    }


    [Fact]
    public void GetAllProducts_ReturnsAllProducts()
    {
        var expectedProducts = new List<Product>
    {
        new Product
        {
            ID = 1,
            BrandID = 1,
            CategoryID = 1,
            ColorID = 1,
            GenderID = 1,
            Name = "Test Product 1",
            ImageUrl = "url1",
            ImageUrl2 = "url2",
            ImageUrl3 = "url3",
            ImageUrl4 = "url4",
            Price = 100.0,
            Discount = 10.0,
            Material = "Leather",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            IsActive = true
        },
        new Product
        {
            ID = 2,
            BrandID = 2,
            CategoryID = 2,
            ColorID = 2,
            GenderID = 2,
            Name = "Test Product 2",
            ImageUrl = "url1",
            ImageUrl2 = "url2",
            ImageUrl3 = "url3",
            ImageUrl4 = "url4",
            Price = 200.0,
            Discount = 20.0,
            Material = "Synthetic",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            IsActive = true
        }
    };

        _mockProductRepository.Setup(repo => repo.GetAll()).Returns(expectedProducts);

        var actualProducts = _productManager.GetAllProducts();

        Assert.Equal(expectedProducts, actualProducts);
    }
 

    [Fact]
    public void SoftDelete_DeletesProductInRepository()
    {
        // Arrange
        var productDto = new ProductDto { ID = 1, Name = "Test Product" };
        var product = new Product { ID = 1, Name = "Test Product" };

        _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);

        // Act
        _productManager.SoftDelete(productDto);

        // Assert
        _mockProductRepository.Verify(repo => repo.SoftDelete(product), Times.Once);
    }

    

    [Fact]
    public void UpdateProduct_UpdatesProductInRepository()
    {
        // Arrange
        var productDto = new ProductDto { ID = 1, Name = "Test Product" };
        var product = new Product { ID = 1, Name = "Test Product" };

        _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);
        _mockProductRepository.Setup(repo => repo.Update(product)).Returns(1);

        // Act
        var result = _productManager.UpdateProduct(productDto);

        // Assert
        Assert.Equal(1, result);
        _mockProductRepository.Verify(repo => repo.Update(product), Times.Once);
    }
    [Fact]
    public void CreateProduct_AddsProductToRepository()
    {
        // Arrange
        var productDto = new ProductDto { ID = 1, Name = "Test Product" };
        var product = new Product { ID = 1, Name = "Test Product" };

        _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);
        _mockProductRepository.Setup(repo => repo.Add(product)).Returns(product.ID);

        // Act
        var result = _productManager.CreateProduct(productDto);

        // Assert
        Assert.Equal(product.ID, result);
        _mockProductRepository.Verify(repo => repo.Add(product), Times.Once);
    }

}
