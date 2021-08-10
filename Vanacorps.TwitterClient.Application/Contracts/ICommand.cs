using System.Threading.Tasks;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface ICommand<T>
    {
        Task ExecuteAsync(T entity);
    }
}
