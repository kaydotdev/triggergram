using System;
using System.Threading;
using System.Threading.Tasks;

using Triggergram.Core.Services.DTO;

namespace Triggergram.Core.Services.Contracts
{
    public interface IMediaPostService
    {
        Task<Guid> CreateMediaPost(MediaPostRecord mediaPostRecord, CancellationToken token);
    }
}
