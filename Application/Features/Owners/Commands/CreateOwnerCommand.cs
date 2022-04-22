using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Abstractions;
using LoggerService.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Owners.Commands
{
    public class CreateOwnerCommand : IRequest<OwnerDto>
    {
        public OwnerCreationDto OwnerCreationDto;
        
        public CreateOwnerCommand(OwnerCreationDto ownerCreationDto)
        {
            OwnerCreationDto = ownerCreationDto;
        }
    }

    internal sealed class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, OwnerDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CreateOwnerCommandHandler(IRepositoryManager repository, ILoggerManager loggerManager, IMapper mapper)
        {
            _repository = repository;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<OwnerDto> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var ownerEntity = _mapper.Map<Owner>(request.OwnerCreationDto);
             _repository.Owner.CreateOwner(ownerEntity);
             await _repository.SaveAsync();

            return _mapper.Map<OwnerDto>(ownerEntity);
        }
    }
}