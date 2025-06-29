namespace Registrator.Net.Tests.ToExclude;

[AutoRegisterTypeAndInterfaces]
public sealed class ClassToExclude : IInterfaceToExclude
{
    
}

public interface IInterfaceToExclude
{
}
