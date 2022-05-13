using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Accounts.Commands
{
    public class PartialUpdateAccountCommand : IRequest<Unit>
    {
        public PartialUpdateAccountCommand(AccountUpdateDto accountUpdateDto, Account account)
        {
            AccountUpdateDto = accountUpdateDto;
            Account = account;
        }

        public AccountUpdateDto AccountUpdateDto { get; set; }
       
        public Account Account { get; set; }
    }

    internal sealed class PartialUpdateAccountCommandHandler : IRequestHandler<PartialUpdateAccountCommand>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public PartialUpdateAccountCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PartialUpdateAccountCommand request, CancellationToken cancellationToken)
        {
            _mapper.Map(request.AccountUpdateDto, request.Account);
           await _repository.SaveAsync();
           
           return Unit.Value;
        }
    }
}