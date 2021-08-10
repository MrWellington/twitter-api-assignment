using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface IUpdateTopEmojisCommand : ICommand<Tweet> { }
}
