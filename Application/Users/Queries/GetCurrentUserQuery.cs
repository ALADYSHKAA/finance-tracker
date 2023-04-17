using Application._Common.Interfaces.Infrastructure.Services;
using Application._Common.Interfaces.Persistence;
using Application.Users.Vms;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;

public class GetCurrentUserQuery : IRequest<UserVm?>
{
}

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserVm?>
{
    private readonly IFinanceTrackerContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserIdService _userIdService;

    public GetCurrentUserQueryHandler(IFinanceTrackerContext context, IMediator mediator, IMapper mapper,
                                      IUserIdService userIdService)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
        _userIdService = userIdService;
    }

    public async Task<UserVm?> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.ProjectTo<UserVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == _userIdService.GetUserId(), cancellationToken);
    }
}

public class GetCurrentUserQueryValidator : AbstractValidator<GetCurrentUserQuery>
{
}