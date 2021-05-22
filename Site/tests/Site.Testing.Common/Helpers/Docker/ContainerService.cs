using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Site.Testing.Common.Helpers.Docker
{
    public class ContainerService
    {
        public async Task Start(DockerContainerBuilder dockerContainer)
        {
            var client = new DockerClientConfiguration().CreateClient();

            var images = await client.Images.ListImagesAsync(new ImagesListParameters() {All = true});

            //If no images already on machine pull the image
            if (images.Any(i => i.RepoTags.Any(t => t.Contains(dockerContainer.Image))) == false)
            {
                await PullImage(client, dockerContainer.Image);
            }
            
            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters{All = true});

            
            if (!dockerContainer.Force)
            {
                if(IsContainerAlreadyRunning(dockerContainer, containers))
                    return;
            }

            
            //Kill containers that have same name
            var usingName = IsContainerUsingSameName(containers, dockerContainer.Name);
            if (usingName is not null)
                await KillContainer(client, usingName);


            containers = await client.Containers.ListContainersAsync(new ContainersListParameters {All = true});

            //Kill containers that are using hosts port
            var containersUsingPorts = GetContainerUsingPorts(containers, dockerContainer);
            foreach (var usingPort in containersUsingPorts)
                await KillContainer(client, usingPort);

            var response = await client.Containers.CreateContainerAsync(GetCreateContainerParameters(dockerContainer));
            await client.Containers.StartContainerAsync(response.ID, null);
        }

        private static CreateContainerParameters GetCreateContainerParameters(DockerContainerBuilder builder)
        {

            var exposedPorts = builder.PortMappings
                .ToDictionary(
                    port => port.Key.ToString(), port
                        => new EmptyStruct());

            var portBindings = builder.PortMappings
                .ToDictionary<KeyValuePair<int, int>, string, IList<PortBinding>>(
                    port => port.Value.ToString(),
                    port => new List<PortBinding> {new() {HostPort = port.Key.ToString()}});

            return new CreateContainerParameters
            {
                Name = builder.Name,
                Image = builder.Image,
                ExposedPorts = exposedPorts,
                HostConfig = new HostConfig
                {
                    PortBindings = portBindings
                },
                Env = builder.EnvironmentVariables.Select(variable => $@"{variable.Key}={variable.Value}").ToList()
            };
        }


        private List<ContainerListResponse> GetContainerUsingPorts(IList<ContainerListResponse> containers,
            DockerContainerBuilder dockerContainer)
        {
            var response = new List<ContainerListResponse>();

            foreach (var potentialClash in containers)
            {
                var containerPublicPorts = potentialClash.Ports.Select(p => p.PublicPort);
                var mustNotContainerPorts = dockerContainer.PortMappings.Select(pm => pm.Key).ToList();

                if (containerPublicPorts.Any(publicPort => mustNotContainerPorts.Contains(publicPort)))
                {
                    response.Add(potentialClash);
                }
            }

            return response;
        }
        
        private static Task KillContainer(DockerClient client, ContainerListResponse container) => client.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters {Force = true});

        private static ContainerListResponse IsContainerUsingSameName(IList<ContainerListResponse> containers, string name)
        {
            return containers.FirstOrDefault(x => x.Names.Any(x => x.Contains(name)));
        }

        private static bool IsContainerAlreadyRunning(DockerContainerBuilder dockerContainer, IList<ContainerListResponse> containers)
        {
            var potentialContainer =
                containers.FirstOrDefault(c => c.Names.Any(n => n.Contains(dockerContainer.Name)));

            if (potentialContainer is not null && potentialContainer.State == "running")
            {
                foreach (var containerPortMapping in dockerContainer.PortMappings)
                {
                    if (!potentialContainer.Ports.Any(port =>
                        port.PublicPort == containerPortMapping.Key && port.PrivatePort == containerPortMapping.Value))
                        return false;
                }

                return true;
            }

            return false;
        }

        private static async Task PullImage(DockerClient client,string imageName)
        {
            var progress = new Progress<JSONMessage>();
            progress.ProgressChanged += (_, message) =>
                Debug.WriteLine($"Pulling dockerContainer image: {imageName} {message.ProgressMessage}");

            await client.Images.CreateImageAsync(new ImagesCreateParameters()
            {
                FromImage = imageName,
            }, null, progress);
            
        }
    }
}