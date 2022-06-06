using ClienteService.Core.DTOs;
using ClienteService.Core.Entities.Masters;
using ClienteService.Core.Interfaces;
using ClienteService.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteService.Core.Services
{
    public class ClienteService : IClienteService
    {
        #region INJECTIONS
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region CONSTRUCTOR
        public ClienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region METHODS
        public async Task<IEnumerable<ClienteListDto>> ListClientes()
        {
            var entitiesDto = new List<ClienteListDto>();
            var añosVida = 80;
            var entities = await _unitOfWork.ClienteRepository.GetAll();
            foreach(var entity in entities)
            {
                var fechaNacimeinto = DateTime.Parse(entity.FechaNacimiento);
                var restoAñosDeVida = añosVida - entity.Edad;
                var fechaPosibleMuerte = fechaNacimeinto.AddYears(restoAñosDeVida);
                entitiesDto.Add(new ClienteListDto()
                {
                    Id = entity.Id,
                    Apellido = entity.Apellido,
                    Nombre = entity.Nombre,
                    Edad = entity.Edad,
                    FechaNacimiento = entity.FechaNacimiento,
                    FechaProbableMuerte = fechaPosibleMuerte.ToString("yyyy-MM-dd"),
                });
            }
            return entitiesDto;
        }

        public async Task<ClienteKpiDto> KpiClientes()
        {
            var entities = await _unitOfWork.ClienteRepository.GetAll();
            var promedio = entities.Select(x => x.Edad).Average();
            var dEstandar = entities.Select(x => x.Edad).StandardDeviation();
            var kpiEntity = new ClienteKpiDto()
            {
                PromedioEdad = promedio,
                DesviacionEstandarEdad = dEstandar,
            };
            return kpiEntity;
        }

        public async Task<Cliente> GetById(string id)
        {
            return await _unitOfWork.ClienteRepository.GetById(id);
        }

        public async Task Insert(Cliente entity)
        {
            await _unitOfWork.ClienteRepository.Insert(entity);
        }

        public async Task<bool> Delete(string id)
        {
            await _unitOfWork.ClienteRepository.Delete(id);
            return true;
        }
        #endregion
    }
}
