using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace CheckinVerifier
{
    class Program
    {
        //===========================================
        //
        // PATH CONFIGURATION
        //
        //===========================================

        const string BaseFolder = @"Source\";
        const string SampleFolder = @"Samples\SampleMvcApp\";

        static string BaseDeployFolder = Path.Combine( BaseFolder, @"AzureDeploy\" );
        static string SampleDeployFolder = Path.Combine( SampleFolder, @"SampleAzureDeploy\" );

        const string ServiceDefinitionFilename = "ServiceDefinition.csdef";
        const string ServiceConfigurationFilename = "ServiceConfiguration.Local.cscfg";

        //===========================================
        //
        // METHODS
        //
        //===========================================

        static void Main( string[] args )
        {
            //
            // Step 1: Verify ServiceDefinition between Source/AzureDeploy and Sample/SampleAzureDeploy.
            //
            {
                var baseServiceDefinition = Path.Combine( BaseDeployFolder, ServiceDefinitionFilename );
                var sampleServiceDefinition = Path.Combine( SampleDeployFolder, ServiceDefinitionFilename );
                PrintResult( ServiceDefinitionFilename, VerifyFirstElement( baseServiceDefinition, sampleServiceDefinition ) );
            }

            //
            // Step 2: Verify the local ServiceConfiguration between Source/AzureDeploy and Sample/SampleAzureDeploy.
            //
            {
                var baseDoc = Path.Combine( BaseDeployFolder, ServiceConfigurationFilename );
                var sampleDoc = Path.Combine( SampleDeployFolder, ServiceConfigurationFilename );
                PrintResult( ServiceConfigurationFilename, VerifyFirstElement( baseDoc, sampleDoc ) );
            }


            Console.ReadKey( );
        }

        static void PrintResult( string test, bool result )
        {
            Console.Write( String.Format( "Verifying {0, -50}", test + ".." ) );

            // Print the result.
            if ( result )
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write( "\t\t[ OK ]" );
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write( "\t\t[ FAIL ]" );
            }

            Console.WriteLine( );
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static bool VerifyFirstElement( string basePath, string samplePath )
        {
            XElement baseDoc = XElement.Load( basePath );
            XElement sampleDoc = XElement.Load( samplePath );

            var baseWorkerRole = baseDoc.FirstNode;
            var sampleWorkerRole = sampleDoc.FirstNode;

            return ( baseWorkerRole.ToString( ).Equals( sampleWorkerRole.ToString( ) ) );
        }
    }
}
