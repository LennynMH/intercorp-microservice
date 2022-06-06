using AutoMapper;
using ClienteService.Api.Responses;
using ClienteService.Core.DTOs;
using ClienteService.Core.Entities.Masters;
using ClienteService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClienteService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        #region INJECTIONS
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        #endregion

        #region CONSTRUCTOR
        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }
        #endregion

        #region HTTP PETITIONS

        [HttpGet]
        [Route("ListClientes")]
        public async Task<ActionResult> ListClientes()
        {
            var result = await _clienteService.ListClientes();
            var response = new ApiResponse<IEnumerable<ClienteListDto>>(result);
            return Ok(response);
        }

        [HttpGet]
        [Route("KpiDeClientes")]
        public async Task<ActionResult> KpiDeClientes()
        {
            var result = await _clienteService.KpiClientes();
            var response = new ApiResponse<ClienteKpiDto>(result);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _clienteService.GetById(id);
            var response = new ApiResponse<Cliente>(result);
            return Ok(response);
        }

        [HttpPost]
        [Route("CreaCliente")]
        public async Task<IActionResult> Post([FromBody] ClienteCreateDto entityDto)
        {
            var entity = _mapper.Map<Cliente>(entityDto);
            entity.Id = Guid.NewGuid().ToString();
            await _clienteService.Insert(entity);
            var response = new ApiResponse<Cliente>(entity);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clienteService.Delete(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        #endregion
    }
}
