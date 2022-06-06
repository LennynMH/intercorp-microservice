using AutoMapper;
using ClienteService.Core.DTOs;
using ClienteService.Core.Entities.Masters;

namespace ClienteService.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ClienteCreateDto, Cliente>();
        }
    }
}
