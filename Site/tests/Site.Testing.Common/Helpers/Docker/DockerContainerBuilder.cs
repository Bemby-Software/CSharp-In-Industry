using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Site.Testing.Common.Helpers.Docker
{
    public class DockerContainerBuilder
    {
        private readonly Dictionary<int, int> _portMappings;
        private readonly Dictionary<string, string> _environmentVariables;
        

        public string Image { get; private set; }
        
        public string Name { get; private set; }
        
        public bool Force { get; private set; }

        public ReadOnlyDictionary<int, int> PortMappings => new(_portMappings);
        
        public ReadOnlyDictionary<string, string> EnvironmentVariables => new(_environmentVariables);

        public DockerContainerBuilder()
        {
            _portMappings = new Dictionary<int, int>();
            _environmentVariables = new Dictionary<string, string>();
        }

        public DockerContainerBuilder WithImage(string image)
        {
            Image = image;
            return this;
        }
        
        public DockerContainerBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public DockerContainerBuilder WithPortMapping(int hostPort, int containerPort)
        {
            _portMappings.Add(hostPort , containerPort);
            return this;
        }
        
        public DockerContainerBuilder WithEnvironmentVariables(string name, string value)
        {
            _environmentVariables.Add(name , value);
            return this;
        }

        public DockerContainerBuilder ForceRecreation()
        {
            Force = true;
            return this;
        }

        public async Task Start()
        {
            var containerService = new ContainerService();
            await containerService.Start(this);
        }
        
        
        
    }
}