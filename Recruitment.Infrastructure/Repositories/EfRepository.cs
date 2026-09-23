using Microsoft.EntityFrameworkCore;
using Recruitment.Application.Interfaces;
using Recruitment.Domain.Entities;
using Recruitment.Infrastructure.Data;

namespace Recruitment.Infrastructure.Repositories;

// One generic repository that works for Job, Candidate and JobApplication.
public class EfRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Db;

    public EfRepository(AppDbContext db)
    {
        Db = db;
    }

    public T? GetById(Guid id) => Db.Set<T>().Find(id);

    public List<T> GetAll() => Db.Set<T>().AsNoTracking().ToList();

    public void Add(T entity)
    {
        Db.Set<T>().Add(entity);
        Db.SaveChanges();
    }

    public void Update(T entity)
    {
        Db.Set<T>().Update(entity);
        Db.SaveChanges();
    }
}
