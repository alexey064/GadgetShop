using Diplom.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public interface IClientRepository
    {
        public void Add(Client client);
    }
}
