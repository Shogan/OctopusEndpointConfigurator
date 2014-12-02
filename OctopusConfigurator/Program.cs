using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Client;

namespace OctopusConfigurator
{
    class Program
    {
        static int Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Usage: OctopusConfigurator.exe \"http://octopusserver.domain.com\" \"API-APIKEYHERE\" \"FRIENDLY-OCTOPUS-MACHINE-NAME\" \"New_port_number_to_use_for_URI\" ");
                return (int)ExitCodes.ExitCode.Success;
            }
            var server = args[0]; // Server URL http: etc
            var apiKey = args[1]; // Get this from your 'profile' page in the Octopus web portal
            var endpoint = new OctopusServerEndpoint(server, apiKey);
            var repository = new OctopusRepository(endpoint);

            var machineName = args[2];
            string newPortNumber = args[3];

            try
            {
                var machine = repository.Machines.FindByName(machineName);
                var currentUri = machine.Uri;

                if (currentUri.Contains("10933"))
                {
                    var newUri = currentUri.Replace("10933", newPortNumber);

                    machine.Uri = newUri;
                    repository.Machines.Modify(machine);
                    Console.WriteLine();
                    Console.WriteLine("Successfully modified machine: {0} URI to: {1}", machineName, newUri);
                    return (int)ExitCodes.ExitCode.Success;
                }

                Console.WriteLine();
                Console.WriteLine("Machine specified does not have a URI port specification of 10933. No action taken. This utility will only modify default port assignment URIs.");
                return (int)ExitCodes.ExitCode.NoActionTaken;
            }
            catch (Exception exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error encountered. Details: {0}", exception.Message);
                return (int)ExitCodes.ExitCode.UnknownError;
            }
        }
    }
}
