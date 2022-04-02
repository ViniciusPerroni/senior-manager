using Microsoft.Extensions.Configuration;

namespace SeniorManager.Crosscutting.Settings.Impl
{
    public class ReaderSettingsWebMvc : IReaderSettingsWebMvc
    {
        private readonly IConfiguration configuration;
        public ReaderSettingsWebMvc(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string BaseUrl()
        {
            return configuration.GetSection("WebMvcSettings").GetSection("BaseUrl").Value;
        }
    }
}