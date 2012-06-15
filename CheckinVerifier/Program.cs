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
                var baseServiceDefinition = XElement.Load( Path.Combine( BaseDeployFolder, ServiceDefinitionFilename ) );
                var sampleServiceDefinition = XElement.Load( Path.Combine( SampleDeployFolder, ServiceDefinitionFilename ) );
                PrintResult( ServiceDefinitionFilename, VerifyServiceDefinition( baseServiceDefinition, sampleServiceDefinition ) );
            }

            //
            // Step 2: Verify the local ServiceConfiguration between Source/AzureDeploy and Sample/SampleAzureDeploy.
            //
            {
                var baseDoc = XElement.Load( Path.Combine( BaseDeployFolder, ServiceConfigurationFilename ) );
                var sampleDoc = XElement.Load( Path.Combine( SampleDeployFolder, ServiceConfigurationFilename ) );
                PrintResult( ServiceConfigurationFilename, VerifyServiceDefinition( baseDoc, sampleDoc ) );
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

        static bool VerifyServiceDefinition( XElement baseDoc, XElement sampleDoc )
        {
            var baseWorkerRole = baseDoc.FirstNode;
            var sampleWorkerRole = sampleDoc.FirstNode;

            return ( baseWorkerRole.ToString( ).Equals( sampleWorkerRole.ToString( ) ) );
        }
    }
}
