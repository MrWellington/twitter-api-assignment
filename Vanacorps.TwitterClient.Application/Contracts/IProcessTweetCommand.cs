using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface IProcessTweetCommand : ICommand<Tweet> { }
}
