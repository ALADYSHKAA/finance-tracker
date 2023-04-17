using Application._Common.Interfaces.Persistence;
using Application.Users.Vms;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;

public class GetUserByIdQuery : IRequest<UserVm?>
{
    public long Id { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserVm?>
{
    private readonly IFinanceTrackerContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetUserByIdQueryHandler(IFinanceTrackerContext context, IMediator mediator, IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<UserVm?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.ProjectTo<UserVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}