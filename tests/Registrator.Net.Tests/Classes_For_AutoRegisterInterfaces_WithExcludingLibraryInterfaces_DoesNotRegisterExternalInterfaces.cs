namespace Registrator.Net.Tests;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public class CreateUser : IRequest { }

[AutoRegisterInterfaces(ServiceLifetime.Transient)]
public class ConcreteType10 : IRequestHandler<CreateUser>
{
    public Task Handle(CreateUser request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}
