using System.Collections.Generic;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface ITopHashtagsQuery : IQuery<List<TopHashtags>> { }
}
