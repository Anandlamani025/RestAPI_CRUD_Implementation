using System.Collections.Generic;
using System;

using static RestAPI_CRUD.Models;
namespace RestAPI_CRUD

{
    public class EntityRepository
    {
        private static List<Entity> _entities = new List<Entity>
        {
            new Entity
            {
                Id = "1",
                Names = new List<Name>
                {
                    new Name { FirstName = "John", MiddleName = "Michael", Surname = "Doe" }
                },
                Addresses = new List<Address>
                {
                    new Address { AddressLine = "123 Main St", City = "Anytown", Country = "USA" }
                },
                Dates = new List<Date>
                {
                    new Date { DateType = "Birth", DateValue = new DateTime(1980, 5, 10) }
                },
                Gender = "Male",
                Deceased = false
            },
            new Entity
            {
                Id = "2",
                Names = new List<Name>
                {
                    new Name { FirstName = "Jane", MiddleName = "Elizabeth", Surname = "Smith" }
                },
                Addresses = new List<Address>
                {
                    new Address { AddressLine = "456 Oak Rd", City = "Otherville", Country = "Canada" }
                },
                Dates = new List<Date>
                {
                    new Date { DateType = "Birth", DateValue = new DateTime(1985, 11, 25) }
                },
                Gender = "Female",
                Deceased = false
            }
        };

        public IEnumerable<Entity> GetEntities()
        {
            return _entities;
        }

        public Entity? GetEntityById(string id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        public void UpdateEntity(Entity entity)
        {
            var existingEntity = _entities.FirstOrDefault(e => e.Id == entity.Id);
            if (existingEntity != null)
            {
                _entities.Remove(existingEntity);
                _entities.Add(entity);
            }
        }

        public void DeleteEntity(string id)
        {
            var entityToDelete = _entities.FirstOrDefault(e => e.Id == id);
            if (entityToDelete != null)
            {
                _entities.Remove(entityToDelete);
            }
        }
    }
}

