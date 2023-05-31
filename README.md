# icd0021-21-22-s

### PcBuilder


### Database
~~~sh
dotnet ef migrations add --project App.DAL.EF --startup-project WebApp --context AppDbContext Initial

dotnet ef migrations remove --project App.DAL.EF --startup-project WebApp --context AppDbContext

dotnet ef database update --project App.DAL.EF --startup-project WebApp

dotnet ef database drop --project App.DAL.EF --startup-project WebApp

~~~

###Controllers

~~~sh
cd WebApp
dotnet aspnet-codegenerator controller -name CountriesController -actions -m App.Domain.Country -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name FeedbacksController -actions -m App.Domain.Feedback -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InvoicesController -actions -m App.Domain.Invoice -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrdersController -actions -m App.Domain.Order -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PaymentTypesController -actions -m App.Domain.PaymentType -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PcsController -actions -m App.Domain.Pc -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductsController -actions -m App.Domain.Product -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductInOrdersController -actions -m App.Domain.ProductInOrder -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductInPcsController -actions -m App.Domain.ProductInPc -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductTypesController -actions -m App.Domain.ProductType -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ServicesController -actions -m App.Domain.Service -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ServiceTypesController -actions -m App.Domain.ServiceType -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShippingInfosController -actions -m App.Domain.ShippingInfo -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShippingTypesController -actions -m App.Domain.ShippingType -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SpecificationsController -actions -m App.Domain.Specification -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SpecificationTypesController -actions -m App.Domain.SpecificationType -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout  --useAsyncActions --referenceScriptLibraries -f
~~~

### WebApi

~~~sh
cd WebApp
dotnet aspnet-codegenerator controller -name CountryController -actions -m App.Domain.Country -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name FeedbackController -actions -m App.Domain.Feedback -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InvoiceController -actions -m App.Domain.Invoice -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderController -actions -m App.Domain.Order -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PaymentTypeController -actions -m App.Domain.PaymentType -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PcController -actions -m App.Domain.Pc -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductController -actions -m App.Domain.Product -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductInOrderController -actions -m App.Domain.ProductInOrder -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductInPcController -actions -m App.Domain.ProductInPc -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductTypeController -actions -m App.Domain.ProductType -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ServiceController -actions -m App.Domain.Service -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ServiceTypeController -actions -m App.Domain.ServiceType -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShippingInfoController -actions -m App.Domain.ShippingInfo -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShippingTypeController -actions -m App.Domain.ShippingType -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SpecificationController -actions -m App.Domain.Specification -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SpecificationTypeController -actions -m App.Domain.SpecificationType -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions --referenceScriptLibraries -f


~~~


### Mapper

~~~sh
ShippingInfos = country.ShippingInfos != null ? country.ShippingInfos.Select(ShippingInfoMapper.ToBll).ToList() : new List<App.BLL.DTO.ShippingInfo>()

~~~

## DOCKER
