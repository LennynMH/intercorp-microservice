using ClienteService.Core.Entities.Masters;
using ClienteService.Core.Interfaces;
using ClienteService.Core.Options;
using ClienteService.Infrastructure.Base;
using Microsoft.Extensions.Options;

namespace ClienteService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region INJECTIONS
        private readonly ConnectionOptions _connectionOptions;
        #endregion

        #region CONSTANTS
        private readonly IRepositoryBase<Cliente> _clienteRepository;
        #endregion

        #region CONSTRUCTOR
        public UnitOfWork(IOptions<ConnectionOptions> connectionOptions)
        {
            _connectionOptions = connectionOptions.Value;
        }
        #endregion

        #region METHODS
        public IRepositoryBase<Cliente> ClienteRepository => _clienteRepository ?? new RepositoryBase<Cliente>(_connectionOptions);
        #endregion
    }
}
