using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IPaymentTypeRepository : IEntityRepository<App.DAL.DTO.PaymentType>, IPaymentTypeRepositoryCustom<PaymentType>
{

}

public interface IPaymentTypeRepositoryCustom<TEntity>
{
    // write your custom methods here 
}