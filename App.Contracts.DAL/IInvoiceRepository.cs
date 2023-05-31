using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IInvoiceRepository : IEntityRepository<App.DAL.DTO.Invoice>, IInvoiceRepositoryCustom<Invoice>
{
    
}

public interface IInvoiceRepositoryCustom<TEntity>
{
    // write your custom methods here 
    
}