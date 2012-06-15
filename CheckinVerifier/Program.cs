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

        static class BaseProject
        {
            public const string DeployFolder = "Source\\AzureDeploy";
            public const string SetupFolder = "Source\\Setup";
        }

        static class SampleProject
        {
            public const string DeployFolder = "Samples\\SampleMvcApp\\SampleAzureDeploy";
            public const string SetupFolder =  "Samples\\SampleMvcApp\\Setup"; 
        }    

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
                var file = "ServiceDefinition.csdef";
                var baseServiceDefinition = Path.Combine( BaseProject.DeployFolder, file );
                var sampleServiceDefinition = Path.Combine( SampleProject.DeployFolder, file );
                PrintResult( file, VerifyFirstElement( baseServiceDefinition, sampleServiceDefinition ) );
            }

            //
            // Step 2: Verify the local ServiceConfiguration between Source/AzureDeploy and Sample/SampleAzureDeploy.
            //
            {
                var file = "ServiceConfiguration.Local.cscfg";
                var baseDoc = Path.Combine( BaseProject.DeployFolder, file );
                var sampleDoc = Path.Combine( SampleProject.DeployFolder, file );
                PrintResult( file, VerifyFirstElement( baseDoc, sampleDoc ) );
            }

            //
            // Step 3: Verify the cloud ServiceConfiguration between the two setup dirs.
            //
            {
                var file = "ServiceConfiguration.Cloud.cscfg.ref";
                var basePath = Path.Combine( BaseProject.SetupFolder, file );
                var samplePath = Path.Combine( SampleProject.SetupFolder, file );
                PrintResult( file, VerifyFirstElement( basePath, samplePath ) );
            }

            Console.ReadKey( );
        }

        static void PrintResult( string test, bool result )
        {
            Console.Write( String.Format( "Verifying {0, -60}", test + ".." ) );

            // Print the result.            
            if ( result )
            {                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write( "[ OK ]" );
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write( "[ FAIL ]" );
            }
            
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine( );
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
