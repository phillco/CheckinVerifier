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
            public const string DeployFolder = "src\\MongoDB.WindowsAzure.Deploy";
            public const string SetupFolder = "Setup";
        }

        static class SampleProject
        {
            public const string DeployFolder = "src\\SampleApplications\\MvcMovieSample\\MongoDB.WindowsAzure.Sample.Deploy";
            public const string SetupFolder = "Setup";
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
                PrintResult( file, VerifyElementMatches( baseServiceDefinition, sampleServiceDefinition, "ReplicaSetRole" ) );
            }

            //
            // Step 2: Verify the local ServiceConfiguration between Source/AzureDeploy and Sample/SampleAzureDeploy.
            //
            {
                var file = "ServiceConfiguration.Local.cscfg";
                var baseDoc = Path.Combine( BaseProject.DeployFolder, file );
                var sampleDoc = Path.Combine( SampleProject.DeployFolder, file );
                PrintResult( file, VerifyElementMatches( baseDoc, sampleDoc, "ReplicaSetRole" ) );
            }

            //
            // Step 3: Verify the cloud ServiceConfiguration between the two setup dirs.
            //
            {
                var basePath = Path.Combine( BaseProject.SetupFolder, "ServiceConfiguration.Cloud.cscfg.core" );
                var samplePath = Path.Combine( SampleProject.SetupFolder, "ServiceConfiguration.Cloud.cscfg.sample" );
                PrintResult( "ServiceConfiguration.Cloud.cscfg", VerifyElementMatches( basePath, samplePath, "ReplicaSetRole") );
            }

            Console.ReadKey( );
        }

        /// <summary>
        /// Verifies that both the document in basePath and samplePath have an element with a "name" attribute of elementName, and that these two elements are identical.
        /// </summary>
        static bool VerifyElementMatches( string basePath, string samplePath, string elementName )
        {
            XElement baseDoc = XElement.Load( basePath );
            XElement sampleDoc = XElement.Load( samplePath );

            try
            {
                var baseElement = baseDoc.Elements( ).First( element => element.Attribute( "name" ).Value == elementName );
                var sampleElement = sampleDoc.Elements( ).First( element => element.Attribute( "name" ).Value == elementName );

                return ( baseElement.ToString( ).Equals( sampleElement.ToString( ) ) );
            }
            catch ( InvalidOperationException )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( "\n\nERROR: Element \"" + elementName + "\" does not even exist for this test:" );
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }            
        }

        /// <summary>
        /// Prints the result of the given test nicely to the console.
        /// </summary>
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
    }
}
