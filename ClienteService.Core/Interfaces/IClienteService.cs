using ClienteService.Core.DTOs;
using ClienteService.Core.Entities.Masters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClienteService.Core.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteListDto>> ListClientes();

        Task<ClienteKpiDto> KpiClientes();

        Task<Cliente> GetById(string id);

        Task Insert(Cliente entity);

        Task<bool> Delete(string id);
    }
}
