namespace AspNetCoreTemplate.Web.Resources
{
    using System.Reflection;

    using Microsoft.Extensions.Localization;

    public class LocService
    {
        private readonly IStringLocalizer localizer;

        public LocService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            this.localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return this.localizer[key];
        }
    }
}
