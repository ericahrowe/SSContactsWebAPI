using LiteDB;
using SSContacts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SSContacts.Infrastructure.Data
{
    public class ContactRepository : IContactRepository
    {
        private readonly LiteDatabase _db;

        public ContactRepository(string connectionString)
        {
            BsonMapper.Global.EnumAsInteger = true;
            _db = new LiteDatabase(connectionString);
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            var returnValue = new Contact();
            await Task.Run(() =>
            {
                var col = GetCollection();
                returnValue = col.FindOne(x => x.Id == id);
            });

            return returnValue;
        }

        public async Task<List<Contact>> ListAsync()
        {
            var returnValue = new List<Contact>();
            await Task.Run(() =>
            {
                var col = GetCollection();
                returnValue = col.Query().ToList();
            });
            return returnValue;
        }

        public async Task<List<Contact>> ListAsync(Expression<Func<Contact, bool>> predicate)
        {
            var returnValue = new List<Contact>();
            await Task.Run(() =>
            {
                var col = GetCollection();
                returnValue = col.Query().Where(predicate).ToList();
            });
            return returnValue;
        }

        public async Task<Contact> AddAsync(Contact entity)
        {
            await Task.Run(() =>
            {
                var col = GetCollection();
                var newEntityId = col.Insert(entity);
                entity.Id = newEntityId.AsInt32;
            });

            return entity;
        }

        public async Task UpdateAsync(Contact entity)
        {
            await Task.Run(() =>
             {
                 var col = GetCollection();
                 var result = col.Update(entity);
                 //I do not prefer generic exceptions like this, but in the interest of time...
                 if (!result)
                     throw new Exception($"Unable to find contact with Id:{entity.Id} to update");
             });
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                var col = GetCollection();
                var result = col.Delete(id);
                //I do not prefer generic exceptions like this, but in the interest of time...
                if (!result)
                    throw new Exception($"Id {id} not found to delete");
            });
        }

        /// <summary>
        /// This is only to support cleaning up the database for tests at present.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int DeleteMany(Expression<Func<Contact, bool>> predicate)
        {
            var col = GetCollection();
            var result = col.DeleteMany(predicate);

            return result;
        }

        private ILiteCollection<Contact> GetCollection()
        {
            var col = _db.GetCollection<Contact>("contacts");

            return col;
        }
    }
}