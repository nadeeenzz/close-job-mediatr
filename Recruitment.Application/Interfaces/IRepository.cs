using Recruitment.Domain.Entities;

namespace Recruitment.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    T? GetById(Guid id);
    List<T> GetAll();
    void Add(T entity);
    void Update(T entity);
}
