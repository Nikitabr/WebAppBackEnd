using System;
using System.Collections.Generic;
using System.Linq;
using App.BLL.Mappers;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DAL.DTO;
using App.DAL.EF;
using AutoMapper;
using Base.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using WebApp.ApiControllers;
using Xunit.Abstractions;

namespace Tests.WebApp.UnitTests;

public class UnitTestProductController
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly AppDbContext _ctx;
    private readonly IMapper _BLLMapper;
    private readonly ProductService _productService;

    private readonly Mock<IProductRepository> _mockProductRepository;
    


    public UnitTestProductController(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        
        var bllProfile = new App.BLL.AutomapperConfig();
        var bllConf = new MapperConfiguration(conf => conf.AddProfile(bllProfile));
        _BLLMapper = new Mapper(bllConf);
        
        _mockProductRepository = new Mock<IProductRepository>();

        //set up mock db - inMemory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);

        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();
        
        

        _mockProductRepository.Setup(m => 
            m.Add(It.Is<Product>(p => 
                p.GetType() == typeof(Product)))).Returns(new
            Product
            {
                Title = new LangStr("Product") ,
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("Product")
            });
        
        _mockProductRepository.Setup(m => 
                m.Update(It.Is<Product>(p => 
                    p.Title == "ProductOld")))
            .Returns(new Product
            {
                Title = new LangStr("ProductUpdated"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("ProductUpdated")
            });
        
        _mockProductRepository.Setup(m => 
                m.Remove(It.Is<Product>(p => 
                    p.Title == "ProductRemoveEntity")))
            .Returns(new Product
            {
                Title = new LangStr("ProductRemoveEntity"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("ProductRemoveEntity")
            });
        
        _mockProductRepository.Setup(m => 
                m.Remove(It.Is<Guid>(p => 
                    p == Guid.Empty)))
            .Returns(new Product
            {
                Title = new LangStr("ProductRemoveKey"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("ProductRemoveKey")
            });
        
        _mockProductRepository.Setup(m => 
                m.RemoveAsync(It.Is<Guid>(p => 
                    p == Guid.Empty)))
            .ReturnsAsync(new Product
            {
                Title = new LangStr("ProductRemoveAsync"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("ProductRemoveAsync")
            });


        _mockProductRepository.Setup(m =>
            m.GetAllAsync(true)).ReturnsAsync(new List<Product>()
        {
            new()
            {
                Title = new LangStr("GetAllAsync"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("GetAllAsync")
            },
            new()
            {
                Title =  new LangStr("GetAllAsync1"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("GetAllAsync1")
            }
        });

        _mockProductRepository.Setup(m =>
            m.GetAll(true)).Returns(new List<Product>()
        {
            new()
            {
                Title = new LangStr("GetAll"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("GetAll")
            },
            new()
            {
                Title = new LangStr("GetAll1"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("GetAll1")
            }
        });

        _mockProductRepository.Setup(m => m.FirstOrDefaultAsync(
            It.Is<Guid>(p => p == Guid.Empty),
            It.Is<bool>(p => p == true))).ReturnsAsync(new Product()
        {

            Title = new LangStr("FirstOrDefaultAsync"),
            Price = 100,
            Rating = 3,
            Quantity = 10,
            Description = new LangStr("FirstOrDefaultAsync")
        });

        _mockProductRepository.Setup(m => m.FirstOrDefault(
            It.Is<Guid>(p => p == Guid.Empty),
            It.Is<bool>(p => p == true))).Returns(new Product()
        {

            Title = new LangStr("FirstOrDefault"),
            Price = 100,
            Rating = 3,
            Quantity = 10,
            Description = new LangStr("FirstOrDefault")
        });

        _mockProductRepository.Setup(m => m.ExistsAsync(
            It.Is<Guid>(p => p == Guid.Empty))).ReturnsAsync(true);

        _mockProductRepository.Setup(m => m.Exists(
            It.Is<Guid>(p => p == Guid.Empty))).Returns(true);
        
        
        // custom methods

        _mockProductRepository.Setup(m =>
            m.GetProductByName(It.Is<string>(p => p.Contains("TestByName")))).ReturnsAsync(
            new List<Product>()
            {
                new Product()
                {
                    Title = new LangStr("TestByName"),
                    Price = 1000,
                    Rating = 3,
                    Quantity = 10,
                    Description = new LangStr("TestByName")
                },
                new Product()
                {
                    Title = new LangStr("TestByName2"),
                    Price = 10000,
                    Rating = 3,
                    Quantity = 10,
                    Description = new LangStr("TestByName2")
                }

            });

        _productService = new ProductService(
            _mockProductRepository.Object,
            new ProductMapper(_BLLMapper));
    }

    [Fact]
    public void Test_Products_Add()
    {
        var res = _productService.Add(new App.BLL.DTO.Product()
        {
            Title = new LangStr("Product"),
            Price = 100,
            Rating = 3,
            Quantity = 10,
            Description = new LangStr("Product") 
        });


        Assert.NotNull(res);
        Assert.True(res.Title == "Product");
        Assert.True(res.Price == 100);
        Assert.True(res.Rating == 3);
        Assert.True(res.Quantity == 10);
        Assert.True(res.Description == "Product");

    }


    [Fact]
    public void Test_Products_Update()
    {
        var product = new App.BLL.DTO.Product()
        {
            Title = new LangStr("ProductOld"),
            Price = 100,
            Rating = 3,
            Quantity = 10,
            Description = new LangStr("ProductOld") 
        };

        var res = _productService.Update(product);
        
        
        Assert.NotNull(res);
        Assert.True(res.Title == "ProductUpdated");
        Assert.True(res.Price == 100);
        Assert.True(res.Rating == 3);
        Assert.True(res.Quantity == 10);
        Assert.True(res.Description == "ProductUpdated");
    }


    [Fact]
    public void Test_Products_RemoveEntity()
    {
        var product = new App.BLL.DTO.Product()
        {
            Title = new LangStr("ProductRemoveEntity"),
            Price = 100,
            Rating = 3,
            Quantity = 10,
            Description = new LangStr("ProductRemoveEntity") 
        };

        var res = _productService.Remove(product);

        Assert.NotNull(res);
        Assert.Equal(product.Title, res.Title);
        Assert.Equal(res.Price, product.Price);
        Assert.Equal(res.Rating, product.Rating);
        Assert.Equal(res.Quantity, product.Quantity);
        Assert.Equal(res.Description, product.Description);
    }


    [Fact]
    public void Test_Products_RemoveKey()
    {
        var product = new App.BLL.DTO.Product()
        {
            Title = new LangStr("ProductRemoveKey"),
            Price = 100,
            Rating = 3,
            Quantity = 10,
            Description = new LangStr("ProductRemoveKey") 
        };

        var res = _productService.Remove(product.Id);

        Assert.NotNull(res);
        Assert.Equal(res.Title, product.Title);
        Assert.Equal(res.Price, product.Price);
        Assert.Equal(res.Rating, product.Rating);
        Assert.Equal(res.Quantity, product.Quantity);
        Assert.Equal(res.Description, product.Description);
        
        _mockProductRepository.Verify(x => 
            x.Remove(It.Is<Guid>(p => 
                p == Guid.Empty)), Times.Once);
    }


    [Fact]
    public void Test_Products_RemoveAsync()
    {
        var product = new App.BLL.DTO.Product()
        {
            Title = new LangStr("ProductRemoveAsync"),
            Price = 100,
            Rating = 3,
            Quantity = 10,
            Description = new LangStr("ProductRemoveAsync") 
        };

        var res = _productService.RemoveAsync(product.Id).Result;

        Assert.NotNull(res);
        Assert.Equal(res.Title, product.Title);
        Assert.Equal(res.Price, product.Price);
        Assert.Equal(res.Rating, product.Rating);
        Assert.Equal(res.Quantity, product.Quantity);
        Assert.Equal(res.Description, product.Description);
        
        _mockProductRepository.Verify(x => 
            x.RemoveAsync(It.Is<Guid>(p => 
                p == Guid.Empty)), Times.Once);
    }


    [Fact]
    public void Test_Products_GetAllAsync()
    {
        var product = new List<App.BLL.DTO.Product>()
        {
            new App.BLL.DTO.Product
            {
                Title = new LangStr("GetAllAsync"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("GetAllAsync")
            },
            new App.BLL.DTO.Product
            {
                Title = new LangStr("GetAllAsync1"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("GetAllAsync1")
            },
        };
        
        

        var res = _productService.GetAllAsync(true).Result;

        Assert.NotNull(res);
        Assert.NotStrictEqual(product, res);
        
        _mockProductRepository.Verify(
            x => x.GetAllAsync(It.Is<bool>(p => p == true)), 
            Times.Once);
    }


    [Fact]
    public void Test_Products_GetAll()
    {
        var product = new List<App.BLL.DTO.Product>()
        {
            new()
            {
                Title = new LangStr("GetAll"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("GetAll")
            },
            new()
            {
                Title = new LangStr("GetAll1"),
                Price = 100,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("GetAll1")
            }
        };
        
        

        var res = _productService.GetAll();

        Assert.NotNull(res);
        Assert.NotStrictEqual(product, res);
        
        _mockProductRepository.Verify(
            x => x.GetAll(It.Is<bool>(p => p == true)), 
            Times.Once);
    }


    [Fact]
    public void Test_Products_FirstOrDefaultAsync()
    {
        var product = new App.BLL.DTO.Product()
        {
            Title = new LangStr("FirstOrDefaultAsync"),
            Price = 100,
            Rating = 3,
            Quantity = 10,
            Description = new LangStr("FirstOrDefaultAsync")
        };

        var res = _productService.FirstOrDefaultAsync(Guid.Empty).Result;

        Assert.NotNull(res);
        Assert.Equal(product.Title, res!.Title);
        Assert.Equal(product.Description, res!.Description);
        Assert.Equal(product.Price, res!.Price);
        Assert.Equal(product.Quantity, res!.Quantity);
        Assert.Equal(product.Rating, res!.Rating);
        
        _mockProductRepository.Verify(x => x.FirstOrDefaultAsync(
            It.Is<Guid>(p => p.GetType() == typeof(Guid)), It.Is<bool>(p => p == true)), Times.Once);
        
    }


    [Fact]
    public void Test_Products_FirstOrDefault()
    {
        var product = new App.BLL.DTO.Product()
        {
            Title = new LangStr("FirstOrDefault"),
            Price = 100,
            Rating = 3,
            Quantity = 10,
            Description = new LangStr("FirstOrDefault")
        };
        
        

        var res = _productService.FirstOrDefault(Guid.Empty);

        Assert.NotNull(res);
        Assert.Equal(product.Title, res!.Title);
        Assert.Equal(product.Description, res.Description);
        Assert.Equal(product.Price, res.Price);
        Assert.Equal(product.Quantity, res.Quantity);
        Assert.Equal(product.Rating, res.Rating);
        
        _mockProductRepository.Verify(
            x => 
                x.FirstOrDefault(It.Is<Guid>(p => p.GetType() == typeof(Guid)),
                    It.Is<bool>(p => p == true)), Times.Once);

    }


    [Fact]
    public void Test_Products_GetProductName()
    {
        var products = new List<App.BLL.DTO.Product>()
        {
            new()
            {
                Title = new LangStr("TestByName"),
                Price = 1000,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("TestByName")
            },
            new()
            {
                Title = new LangStr("TestByName2"),
                Price = 10000,
                Rating = 3,
                Quantity = 10,
                Description = new LangStr("TestByName2")
            }
        };


        var res = _productService.GetProductByName("TestByName").Result;

        Assert.NotNull(res);
        Assert.NotStrictEqual(products, res);
        
        _mockProductRepository.Verify(x => 
            x.GetProductByName(It.Is<string>(p => p.Contains("TestByName"))), Times.Once);

    }
    


    [Fact]
    public void Test_Products_ExistsAsync()
    {
        var res = _productService.ExistsAsync(Guid.Empty).Result;
        Assert.True(res);
    }
    


    [Fact]
    public void Test_Products_Exists()
    {
        var res = _productService.Exists(Guid.Empty);
        Assert.True(res);
    }
    
    
    
    

}