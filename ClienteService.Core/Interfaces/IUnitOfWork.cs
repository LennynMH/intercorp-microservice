using ClienteService.Core.Entities.Masters;

namespace ClienteService.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryBase<Cliente> ClienteRepository { get; }
    }
}
