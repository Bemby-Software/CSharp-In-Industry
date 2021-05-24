using System.Threading.Tasks;

namespace Site.Core.Services
{
    public interface IGitHubAccountService
    {
        Task IncrementIssueTransferCount(int id);

        Task Assign(string repository, int teamId);
    }
}