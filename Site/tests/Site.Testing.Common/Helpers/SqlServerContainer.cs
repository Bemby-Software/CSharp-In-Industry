using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Site.Testing.Common.Helpers
{
    public static class SqlServerContainer
    {
        public static async Task StartAsync()
        {
            var settings = TestConfiguration.GetConfiguration();
            var client = new DockerClientConfiguration().CreateClient();

            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters());
            var sqlServer = containers.FirstOrDefault(x => x.Names.Any(x => x.Contains(settings.SqlServerContainerName)));
            if (sqlServer is not null)
                await client.Containers.RemoveContainerAsync(sqlServer.ID, new ContainerRemoveParameters {Force = true});

            var usingPorts = containers.Where(c => c.Ports.Any(x => x.PublicPort == settings.SqlServerPort) && c.ID != sqlServer?.ID).ToList();
            if (usingPorts.Any())
            {
                foreach (var containersUsingPort in usingPorts)
                    await client.Containers.RemoveContainerAsync(containersUsingPort.ID, new ContainerRemoveParameters {Force = true});
            }
            
            await StartContainer(client, settings);
        }

        private static async Task StartContainer(DockerClient client, TestConfiguration settings)
        {
            var response = await client.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = settings.SqlServerImage,
                Name = settings.SqlServerContainerName,
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    {$"{settings.SqlServerPort}", default(EmptyStruct)}
                },
                Env = new List<string>
                {
                    "ACCEPT_EULA=Y",
                    $"SA_PASSWORD={settings.SqlServerPassword}"
                },
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {$"{settings.SqlServerPort}", new List<PortBinding>{new PortBinding{HostPort = $"{settings.SqlServerPort}"}}}
                    }
                },
            });
            
            await client.Containers.StartContainerAsync(response.ID, null);
            
        }

        private static async Task PullImage(DockerClient client,string imageName)
        {
            var progress = new Progress<JSONMessage>();

            await client.Images.CreateImageAsync(new ImagesCreateParameters()
            {
                FromImage = imageName,
            }, null, progress);
            
        }
    }
}