using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Abstractions;
using LoggerService.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Accounts.Commands
{
    public class CreateAccountCommand : IRequest<AccountDto>
    {
        public AccountCreationDto AccountCreationDto { get; set; }
        public Guid OwnerId { get; set; }

        public CreateAccountCommand(Guid ownerId,AccountCreationDto accountCreationDto)
        {
            AccountCreationDto = accountCreationDto;
            OwnerId = ownerId;
        }
                  
    }

    internal sealed class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IRepositoryManager repository, ILoggerManager loggerManager, IMapper mapper)
        {
            _repository = repository;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            //find the owner
            var owner = await _repository.Owner.GetOwnerById(request.OwnerId,trackChanges: false);
            if (owner is null)
                _loggerManager.LogError($"owner with id {request.OwnerId} does not exist");

            var accountEntity = _mapper.Map<Account>(request.AccountCreationDto);
            _repository.Account.CreateAccount(request.OwnerId,accountEntity);
            await _repository.SaveAsync();

            var returnAccount = _mapper.Map<AccountDto>(accountEntity);

            return returnAccount;
        }
    }
}