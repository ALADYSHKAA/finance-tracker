using Microsoft.Extensions.Hosting;

namespace WebUi.Utils.Extensions
{
    public static class HostEnvironmentExtensions
    {
        public static bool IsTesting(this IHostEnvironment env)
        {
            return env.EnvironmentName == "Testing";
        }
    }
}