using App.Public.DTO.v1.Identity;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Base.BLL;

public abstract class BaseBLL<TDal> : IBLL
    where TDal: IUnitOfWork
{
    public abstract Task<int> SaveChangesAsync();

    public abstract  int SaveChanges();
}