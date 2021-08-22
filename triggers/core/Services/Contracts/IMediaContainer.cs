using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Triggergram.Core.Services.Contracts
{
    public interface IMediaContainer
    {
        Task SaveMediaAsync(string name, Stream fileStream, CancellationToken token);
    }
}
