namespace LMS.Api.Extensions;

internal static class ConfigurationExtensions
{
    internal static void AddModuleConfiguration(this IConfigurationBuilder configurationBuilder, string[] modules)
    {
        foreach (string module in modules)
        {
            configurationBuilder.AddJsonFile(
                path: $"modules.{module}.appsettings.json",
                optional: false,
                reloadOnChange: true);
        }
    }
}
