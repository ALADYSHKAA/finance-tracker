using Application._Common.Exceptions;
using Application._Common.Interfaces.Persistence;
using Application._Common.Mappings;
using AutoMapper;
using Domain.Domains.Users.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Cmds;

public class EditUserCmd : IRequest<long>, IMapTo<User>
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Nickname { get; set; }
    public string Email { get; set; }
    public bool Disabled { get; set; }
}

public class EditUserCmdHandler : IRequestHandler<EditUserCmd, long>
{
    private readonly IFinanceTrackerContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public EditUserCmdHandler(IFinanceTrackerContext context, IMediator mediator, IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<long> Handle(EditUserCmd request, CancellationToken cancellationToken)
    {
        User? user = null;

        if (request.Id is not 0)
        {
            user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user is null) throw new NotFoundException(request.Id, nameof(User));
        }

        user ??= new User();

        _mapper.Map(request, user);

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}

public class EditUserCmdValidator : AbstractValidator<EditUserCmd>
{
    public EditUserCmdValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}