Verifications
-------------

**Source\AzureDeploy\ServiceDefinition.csdef** is a proper subset of **Samples\SampleMvcApp\SampleAzureDeploy\ServiceDefinition.csdef**

*Details:* In the core ServiceDefinition.csdef file, under the <code>ServiceDefinition</code> tag, there is a <code>WorkerRole</code> resource called ReplicaSetRole. The exact same role must exist in the sample app.

**Source\AzureDeploy\ServiceConfiguration.Local.cscfg** is a proper subset of **Samples\SampleMvcApp\SampleAzureDeploy\ServiceConfiguration.Local.cscfg**


**Source\Setup\ServiceConfiguration.Cloud.cscfg.ref** is a proper subset of **Samples\SampleMvcApp\Setup\ServiceConfiguration.Cloud.cscfg.ref**


**Source\Setup\ServiceConfiguration.Cloud.cscfg.ref** is the same as **Source\AzureDeploy\ServiceConfiguration.Local.cscfg except for values of ConnectionStrings**


**Source\Setup\ServiceConfiguration.Cloud.cscfg.ref** is the same as **Source\AzureDeploy\ServiceConfiguration.Cloud.cscfg except for values of ConnectionStrings**