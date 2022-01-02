using System;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers.V1;
using WebApi.DTOs;
using WebApi.Mapps;
using Xunit;
using FluentValidation;
using FluentAssertions;
using WebApi.Validation;

namespace WebApi.Test
{
    public class ContractControllerTest
    {
        private Mock<IContractRepository> mockIContractRepository;
        ContractController contractController;
        private IMapper mapper;

        private Contract ContractEntity = new Contract()
        {
            Id = Guid.NewGuid().ToString(),
            BrokerAgentName = "BrokerAgentName",
            CustomerAddress = "CustomerAddress",
            CustomerName = "CustomerName"
        };

        private ContractDto ContractDto = new ContractDto()
        {
             BrokerAgentName = "BrokerAgentName",
            CustomerAddress = "CustomerAddress",
            CustomerName = "CustomerName"
        };


        public ContractControllerTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ContractProfile>());
            mapper = config.CreateMapper();

            mockIContractRepository = new Mock<IContractRepository>();
            contractController = new ContractController(mockIContractRepository.Object, mapper);

        }


        [Fact]
        public async void PostContract_WithValidData_ReturnContractWithId()
        {
            var contract = GetContractEntotyFromDto(ContractDto);

            // Arrange

            mockIContractRepository.Setup(x => x.AddAsync(It.IsAny<Contract>())).ReturnsAsync(contract);
            mockIContractRepository.Setup(x => x.FindAsync(contract.Id)).ReturnsAsync(contract);

            // Act

            var result = await contractController.PostContract(ContractDto);
            var resultValue = (ContractDto)(result.Result as CreatedAtActionResult).Value;

            // Assert
            resultValue.Should().BeEquivalentTo(ContractDto, options => options.ExcludingMissingMembers().Excluding(x => x.Id));
            resultValue.Id.Should().NotBeEmpty();
            resultValue.Id.Should().BeEquivalentTo(contract.Id);

            mockIContractRepository.Verify(x => x.AddAsync(It.IsAny<Contract>()), Times.Once);

        }


        [Fact]
        public async void PostContract_WithinvalidData_ReturnBadRequestResult()
        {
            var dto = new ContractDto();
            var validator = new ContractDtoValodation();

            var validatorResult = validator.Validate(dto);

            validatorResult.Errors.ForEach(error => contractController.ModelState.AddModelError(error.PropertyName, error.ErrorMessage));

            // Act
            var result = await contractController.PostContract(dto);

            // Assertion

            result.Result.Should().BeOfType<BadRequestObjectResult>();
            validatorResult.IsValid.Should().BeFalse();
            Assert.Contains(nameof(dto.CustomerName), contractController.ModelState.Keys);
            Assert.True(contractController.ModelState[nameof(dto.CustomerName)].Errors.Count == 2);

            mockIContractRepository.Verify(x => x.AddAsync(It.IsAny<Contract>()), Times.Never);

            Assert.False(contractController.ModelState.IsValid);


        }


        [Fact]
        public async void GetContract_WithExistingItem_ReturnContract()
        {
            // Arrenge
            mockIContractRepository.Setup(x => x.FindAsync(It.IsAny<string>())).ReturnsAsync(ContractEntity);

            //Act 
            var result = await contractController.GetContract(Guid.NewGuid().ToString());

            // Assert
            result.Value.Should().BeEquivalentTo(ContractEntity, option => option.ExcludingMissingMembers());
        }



        [Fact]
        public async void GetContract_WithUnExistingItem_ReturnNull()
        {
            // Arrenge
            mockIContractRepository.Setup(x => x.FindAsync(It.IsAny<string>())).ReturnsAsync((Contract)null);

            //Act 
            var result = await contractController.GetContract(Guid.NewGuid().ToString());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }



        private Contract GetContractEntotyFromDto(ContractDto dto)
        {
            return new Contract()
            {
                Id = Guid.NewGuid().ToString(),
                BrokerAgentName = dto.BrokerAgentName,
                CustomerAddress = dto.CustomerAddress,
                CustomerName = dto.CustomerName
            };
        }
    }
}
