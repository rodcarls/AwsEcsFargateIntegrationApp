using Amazon;
using Amazon.ECS;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsEcsTestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var basicCredentials = new BasicAWSCredentials("XXXXXXXXXXXXXX", "XXXXXXXXXXXXXXXXXXXXXXXXXXX");
            var client = new AmazonECSClient(basicCredentials, RegionEndpoint.USWest2);

            // list available clusters in region
            var regionClusterArn = client.ListClusters(new Amazon.ECS.Model.ListClustersRequest { }).ClusterArns;
            foreach (var regionClusterName in regionClusterArn)
            {
                var clusterServiceArn = (await client.ListServicesAsync(new Amazon.ECS.Model.ListServicesRequest { Cluster = regionClusterName }).ConfigureAwait(false)).ServiceArns;
                var describeServiceRequest = new Amazon.ECS.Model.DescribeServicesRequest { Cluster = regionClusterName, Services = clusterServiceArn };

                // list all available services in cluster
                var regionClusterService = (await client.DescribeServicesAsync(describeServiceRequest).ConfigureAwait(false)).Services;
            }
        }
    }
}
