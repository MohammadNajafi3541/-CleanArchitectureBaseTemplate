using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Interfaces.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Mapps;

namespace WebApi.Controllers.V2
{
    [ApiController]
    [Route("api/contract")]
    [ApiVersion("2.0")]
    public class ContractController : ControllerBase
    {
        private readonly IContractRepository _contractRepository;
        private ContractMapper _contractMapper;

        public ContractController(IContractRepository contractRepository,IMapper mapper)
        {
            this._contractRepository = contractRepository;
            _contractMapper = new ContractMapper(mapper);

        }

        [HttpGet]
        public ActionResult<IEnumerable<ContractDto>> GetContract([FromQuery]int pagesize, [FromQuery] int pagenumber)
        {
            return _contractRepository.GetAll(new PagingParameter()).Select(x => _contractMapper.ToDto(x)).ToList();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ContractDto>> GetContract(string id)
        {
            var contract = await _contractRepository.FindAsync(id);

            if (contract == null)
                return NotFound();

            return _contractMapper.ToDto(contract);
        }


        [HttpPost]
        public async Task<ActionResult<ContractDto>> PostContract(ContractDto contractDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newContract = await _contractRepository.AddAsync(_contractMapper.ToEntity(contractDto));

            return CreatedAtAction("GetContract", new { id = newContract.Id }, _contractMapper.ToDto(newContract));
        }


        [HttpPut]
        public async Task<ActionResult> UpdateName(string id,string customerName)
        {
            var entity = await _contractRepository.FindAsync(id);

            if (entity == null)
                return NotFound();

            var dto = _contractMapper.ToDto(entity);

            dto.CustomerName = customerName;

            if(!TryValidateModel(dto))
            {
                return ValidationProblem(ModelState);
            }

            await _contractRepository.UpdateAsync(_contractMapper.ToEntity(dto,entity));


            return CreatedAtAction("GetContract", new { id = entity.Id }, _contractMapper.ToDto(entity));

        }
    }
}
