using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using App.Public.DTO.v1;
using Base.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WebApp.DTO.Identity;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;
using JwtResponse = App.Public.DTO.v1.Identity.JwtResponse;

namespace Tests.WebApp.IntegrationTestsApi;

public class HappyFlowIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{

    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new JsonSerializerOptions(JsonSerializerDefaults.Web);

    public HappyFlowIntegrationTests(CustomWebApplicationFactory<Program> factory,
        ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        
    }

    [Fact]
    public async void StartApp()
    {
        await Register_Api();
    }

    public async Task Register_Api()
    {

        var uri = "/api/v1.0/Identity/Account/Register";

        var registerDto = new Register
        {
            Email = "nibrja@itcollege.ee",
            Password = "Kala.maja1",
            FirstName = "Nikita",
            LastName = "Brjakilev"
        };

        var jsonStr = JsonSerializer.Serialize(registerDto);
        var data = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        var getRegister = await _client.PostAsync(uri, data);

        var content = await getRegister.Content.ReadAsStringAsync();

        var actualData = JsonSerializer.Deserialize<JwtResponse>(content, JsonSerializerOptions);
        
        Assert.Equal(200, (int) getRegister.StatusCode);
        Assert.Equal("Nikita", actualData!.FirstName);
        Assert.Equal("Brjakilev", actualData.LastName);
        _testOutputHelper.WriteLine("Register success!");
        await Login_Api();
    }

    public async Task Login_Api()
    {
        var uri = "/api/v1.0/Identity/Account/Login";

        var loginDto = new Login()
        {
            Email = "nibrja@itcollege.ee",
            Password = "Kala.maja1"
        };

        var dataToPost = new StringContent(System.Text.Json.JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");

        var getResponse = await _client.PostAsync(uri, dataToPost);
        
        Assert.Equal(200, (int) getResponse.StatusCode);


        var body = await getResponse.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<JwtResponse>(body, JsonSerializerOptions);

        Assert.NotNull(data);
        Assert.Equal("Nikita", data!.FirstName);
        Assert.Equal("Brjakilev", data!.LastName);
        _testOutputHelper.WriteLine("Login success!");
        await Create_Order(data);
    }

    public async Task Create_Order(JwtResponse user)
    {
        var uri_postOrder = "/api/v1.0/Orders";
        var uri_getShippingInfo = "/api/v1.0/ShippingInfos";
        var uri_getPaymentType = "/api/v1.0/PaymentTypes";
        var uri_getProducts = "/api/v1.0/Products";
        var uri_postProductInOrder = "/api/v1.0/ProductInOrder";

        var reqShippingInfo = await _client.GetAsync(uri_getShippingInfo);
        var reqPaymentType = await _client.GetAsync(uri_getPaymentType);
        var reqProducts = await _client.GetAsync(uri_getProducts);

        reqShippingInfo.EnsureSuccessStatusCode();
        reqPaymentType.EnsureSuccessStatusCode();
        reqProducts.EnsureSuccessStatusCode();

        var bodyShippingInfo = await reqShippingInfo.Content.ReadAsStringAsync();
        var bodyPaymentType = await reqPaymentType.Content.ReadAsStringAsync();
        var bodyProducts = await reqProducts.Content.ReadAsStringAsync();

        var shippingInfoList = 
            JsonSerializer.Deserialize<ICollection<ShippingInfo>>(bodyShippingInfo, JsonSerializerOptions);
        var paymentTypeList =
            JsonSerializer.Deserialize<ICollection<PaymentType>>(bodyPaymentType, JsonSerializerOptions);
        var productsList = JsonSerializer.Deserialize<ICollection<Product>>(bodyProducts, JsonSerializerOptions);

        var shippingInfo = shippingInfoList.FirstOrDefault(x => x.City == "Tallinn");
        var paymentType = paymentTypeList.FirstOrDefault(x => x.PaymentName == "PayPal");
        var product = productsList.FirstOrDefault(x => x.Title == "Intel");

        var orderDto = new Order
        {
            AppUserId = user.AppUserId,
            PaymentTypeId = paymentType!.Id,
            ShippingInfoId = shippingInfo!.Id,
            Price = 100,
            Description = "Something"
        };

        var productInOrderDto = new ProductInOrder
        {
            ProductId = product!.Id,
            OrderId = orderDto.Id
        };

        var orderToPost = new StringContent(JsonSerializer.Serialize(orderDto), Encoding.UTF8, "application/json");
        var productInOrderToPost =
            new StringContent(JsonSerializer.Serialize(productInOrderDto), Encoding.UTF8, "application/json");
        

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

        var getRespOrder = await _client.PostAsync(uri_postOrder, orderToPost);
        var getRespProductInOrder = 
            await _client.PostAsync(uri_postProductInOrder, productInOrderToPost);
        

        getRespOrder.EnsureSuccessStatusCode();
        getRespProductInOrder.EnsureSuccessStatusCode();
        
        Assert.Equal(201, (int) getRespOrder.StatusCode);
        Assert.Equal(201, (int) getRespProductInOrder.StatusCode);

        var bodyOrder = await getRespOrder.Content.ReadAsStringAsync();
        var bodyProductInOrder = await getRespProductInOrder.Content.ReadAsStringAsync();
        
        var dataOrder = JsonSerializer.Deserialize<Order>(bodyOrder, JsonSerializerOptions);
        var dataProductInOrder = JsonSerializer.Deserialize<ProductInOrder>(bodyProductInOrder, JsonSerializerOptions);
        
        
        Assert.NotNull(dataOrder);
        Assert.NotNull(dataProductInOrder);
        _testOutputHelper.WriteLine("Order and ProductInOrder successfully created!");

        await Create_Invoice_Api(user, orderDto.Id, paymentType.PaymentName);
    }

    public async Task Create_Invoice_Api(JwtResponse user, Guid orderId, string paymentMethod)
    {
        var uri_postInvoice = "/api/v1.0/Invoices";
        
        var invoiceDto = new Invoice
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PaymentMethod = paymentMethod,
            DeliveryMethod = "Courier",
            OrderId = orderId,
            Code = "Secret Code tsh"
        };
        
        
        
        var invoiceToPost = new StringContent(JsonSerializer.Serialize(invoiceDto), Encoding.Default, "application/json");

        var getRespInvoice = await _client.PostAsync(uri_postInvoice, invoiceToPost);

        getRespInvoice.EnsureSuccessStatusCode();
        
        Assert.Equal(201, (int) getRespInvoice.StatusCode);
        
        var bodyInvoice = await getRespInvoice.Content.ReadAsStringAsync();
        
        var dataInvoice = JsonSerializer.Deserialize<Invoice>(bodyInvoice, JsonSerializerOptions);
        
        
        Assert.NotNull(dataInvoice);
        _testOutputHelper.WriteLine("Invoice successfully created!");
        
        await Get_Invoice_Api(orderId);
        
    }

    public async Task Get_Invoice_Api(Guid orderId)
    {
        var uri_getInvoices = "/api/v1.0/Invoices";
        

        var reqInvoice = await _client.GetAsync(uri_getInvoices);

        reqInvoice.EnsureSuccessStatusCode();

        var bodyInvoice = await reqInvoice.Content.ReadAsStringAsync();

        var invoicesList = 
            JsonSerializer.Deserialize<ICollection<Invoice>>(bodyInvoice, JsonSerializerOptions);
        
        var invoice = invoicesList!.FirstOrDefault();
        
        Assert.NotNull(invoice);
        Assert.Equal("Nikita", invoice!.FirstName);
        Assert.Equal("Brjakilev", invoice.LastName);
        Assert.Equal("Secret Code tsh", invoice.Code);
        Assert.Equal("nibrja@itcollege.ee", invoice.Email);
        Assert.Equal("Courier", invoice.DeliveryMethod);
        Assert.Equal("PayPal", invoice.PaymentMethod);
        Assert.Equal(orderId, invoice.OrderId);
        
        _testOutputHelper.WriteLine("Purchase succeed and Invoice was correctly done!");
        
    }
    
}