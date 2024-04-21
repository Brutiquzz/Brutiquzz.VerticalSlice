using Microsoft.Extensions.DependencyInjection;

namespace Brutiquzz.Contractor.Arch.Contractor.Installers;

public interface IApplicationInstaller
{
    void InstallBusiness(IServiceCollection services);
    void InstallDataAccess(IServiceCollection services);

}
