namespace AspNetCoreTemplate.Web.ViewModels.Users
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;
    using AutoMapper;

    public class RolesDropDownViewModels : IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationRole, RolesDropDownViewModels>()
                .ForMember(x => x.Id, options =>
                {
                    options.MapFrom(p => p.Id);
                });

            configuration.CreateMap<ApplicationRole, RolesDropDownViewModels>()
                .ForMember(x => x.Name, options =>
                {
                    options.MapFrom(p => p.Name);
                });
        }
    }
}
