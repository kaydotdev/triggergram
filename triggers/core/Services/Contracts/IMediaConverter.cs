using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Triggergram.Core.Services.Contracts
{
    public interface IMediaConverter
    {
        Task<Stream> ConvertMediaFormatAsync(Stream mediaStream, CancellationToken token = default);
    }
}
