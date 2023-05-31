using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class InvoiceRepository : BaseEntityRepository<App.DAL.DTO.Invoice, App.Domain.Invoice, AppDbContext>, IInvoiceRepository
{
    public InvoiceRepository(AppDbContext dbContext, IMapper<Invoice, Domain.Invoice> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Invoice>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);    
    }
}