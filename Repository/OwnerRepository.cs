using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Owner> GetAll()
        {
            return FindAll().OrderBy(own=>own.Name).ToList();
        }

        public Owner GetById(Guid ownerGuid)
        {
            return FindByCondition(owner => owner.OwnerId.Equals(ownerGuid)).FirstOrDefault();
         
        }

        public Owner GetOwnerWithAccounts(Guid OwnerGuid) {
            return FindByCondition(owner => owner.OwnerId.Equals(OwnerGuid))
                .Include(owac => owac.Accounts).FirstOrDefault();
        }

        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }
    }
}
