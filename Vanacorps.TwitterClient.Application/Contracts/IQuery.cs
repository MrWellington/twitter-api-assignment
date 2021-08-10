using System.Threading.Tasks;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface IQuery<T>
    {
        Task<T> ExecuteAsync();
    }
}
