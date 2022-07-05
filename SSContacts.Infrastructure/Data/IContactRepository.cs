using SSContacts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SSContacts.Infrastructure.Data
{
    public interface IContactRepository
    {
        Task<Contact> GetByIdAsync(int id);

        Task<List<Contact>> ListAsync();

        Task<List<Contact>> ListAsync(Expression<Func<Contact, bool>> predicate);

        Task<Contact> AddAsync(Contact entity);

        Task UpdateAsync(Contact entity);

        Task DeleteAsync(int id);
    }
}