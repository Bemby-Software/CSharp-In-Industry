using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Site.Core.Configuration;

namespace Site.Web
{
    public class Environment : IEnvironment
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Environment(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool IsDevelopment => _webHostEnvironment.IsDevelopment();
    }
}