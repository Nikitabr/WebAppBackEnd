using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class InvoiceService : BaseEntityService<App.BLL.DTO.Invoice, App.DAL.DTO.Invoice, IInvoiceRepository>,
    IInvoiceService
{
    public InvoiceService(IInvoiceRepository repository, IMapper<Invoice, DAL.DTO.Invoice> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<Invoice>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;
        
    }

    public Task<IEnumerable<Invoice>> GetAllByTitleAsync(string name, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}