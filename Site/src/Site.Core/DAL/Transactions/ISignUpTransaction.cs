using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Core.DAL.Transactions
{
    public interface ISignUpTransaction
    {
        Task SignUpAsync(Team team);
    }
}