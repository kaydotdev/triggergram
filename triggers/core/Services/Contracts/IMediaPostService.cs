using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Triggergram.Core.Services.DTO;

namespace Triggergram.Core.Services.Contracts
{
    public interface IMediaPostService
    {
        Task<IEnumerable<Guid>> GetMediaPostIdsByAccount(Guid accountId, CancellationToken token);
        Task<Guid> CreateMediaPostAsync(MediaPostRecord mediaPostRecord, CancellationToken token);
        Task<Stream> GetMediaAsync(Guid postId, CancellationToken token);
        Task<MediaPostView> GetMediaPostContentAsync(Guid postId, CancellationToken token);
    }
}
