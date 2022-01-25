using System.Threading.Tasks;
using CommitMaster.Sirius.Domain.Contracts.v1.Mensageria;

namespace CommitMaster.Sirius.Domain.Contracts.v1.Bus
{
    public interface IBus
    {
        Task Publish<T>(T data) where T : class, IMessage;
    }
    
}
