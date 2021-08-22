using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Triggergram.Core.Services.DTO;

namespace Triggergram.Core.Services.Contracts
{
    public interface IMediaPostService
    {
        Task<Guid> CreateMediaPostAsync(MediaPostRecord mediaPostRecord, CancellationToken token);
        Task<Stream> GetMediaAsync(Guid postId, CancellationToken token);
        Task<MediaPostView> GetMediaPostContentAsync(Guid postId, CancellationToken token);
    }
}
