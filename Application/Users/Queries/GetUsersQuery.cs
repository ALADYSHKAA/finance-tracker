using Application._Common.Interfaces.Persistence;
using Application.Users.Vms;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;

public class GetUsersQuery : IRequest<List<UserVm>>
{
    //фильтры или настройки паджинации тут.
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserVm>>
{
    private readonly IFinanceTrackerContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetUsersQueryHandler(IFinanceTrackerContext context, IMediator mediator, IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<List<UserVm>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.ProjectTo<UserVm>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}

public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
}