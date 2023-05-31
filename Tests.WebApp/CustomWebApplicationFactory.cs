using System;
using System.Linq;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tests.WebApp;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
where TStartup : class
{
    private static bool dbInitialized = false;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // find DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            
            // if found - remove
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            
            // and new DbContext
            services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });
            
            // data seeding
            // create db and seed data
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            db.Database.EnsureCreated();

            try
            {
                if (dbInitialized == false)
                {
                    dbInitialized = true;
                    // DataSeeder.SeedData(db);
                    
                    //Seed data

                    if (db.ShippingTypes.Any()) return;
                    
                    var shippingType = new ShippingType
                    {
                        Title = "Courier"
                    };
                    db.ShippingTypes.Add(shippingType);

                    if (db.ProductTypes.Any()) return;

                    var productType = new ProductType
                    {
                        Title = "Processor"
                    };

                    db.ProductTypes.Add(productType);

                    if (db.PaymentTypes.Any()) return;

                    var paymentType = new PaymentType
                    {
                        PaymentName = "PayPal",
                        Description = "something"
                    };

                    db.PaymentTypes.Add(paymentType);

                    if (db.Countries.Any()) return;

                    var country = new Country
                    {
                        CountryName = "Estonia"
                    };

                    db.Countries.Add(country);


                    if (db.ShippingInfos.Any()) return;

                    var ship = new ShippingInfo
                    {
                        ShippingTypeId = shippingType.Id,
                        CountryId = country.Id,
                        City = "Tallinn",
                        Address1 = "Akadeemia",
                        Address2 = "",
                        PostalCode = "12616",
                        PhoneNumber = "58255488",
                        MailAddress = "mail@gmail.com"
                    };

                    db.ShippingInfos.Add(ship);

                    if (db.Products.Any()) return;

                    var product = new Product
                    {
                        Title = "Intel",
                        ProductTypeId = productType.Id,
                        Price = 100,
                        Rating = 2,
                        Quantity = 3,
                        Picture = "aaa",
                        Description = "descript"
                    };

                    db.Products.Add(product);


                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " + "database with test messages. Error: {Message}", ex.Message);
            }

        });
        base.ConfigureWebHost(builder);
    }
}