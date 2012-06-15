using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.XmlDiffPatch;

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

        //===========================================
        //
        // METHODS
        //
        //===========================================

        static void Main( string[] args )
        {
            //
            // Step 1: Verify ServiceDefinition.csdef between Source/AzureDeploy and Sample/SampleAzureDeploy.
            //
            var baseVersion = Path.Combine( BaseDeployFolder, ServiceDefinitionFilename );
            var sampleVersion = Path.Combine( SampleDeployFolder, ServiceDefinitionFilename );

            XmlWriter output = XmlWriter.Create( "diff.xml", new XmlWriterSettings { Indent = true } );
            DiffXmlFiles( baseVersion, sampleVersion, output );
        }

        /// <summary>
        /// Diffs the two files and writes the result to differencesWriter.
        /// </summary>
        public static void DiffXmlFiles( string originalFile, string finalFile, XmlWriter differencesWriter )
        {
            XmlDiff xmldiff = new XmlDiff( XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreNamespaces | XmlDiffOptions.IgnorePrefixes );
            bool bIdentical = xmldiff.Compare( originalFile, finalFile, true, differencesWriter );
            differencesWriter.Close( );
        }

    }
}
