Verifications
-------------

**ServiceDefinition.csdef**
The ServiceDefinition.csdef in the Core/CloudDeploy project must be a proper subset of the version in Sample/MvcMove/SampleCloudDeploy.
*Details:* Under the <code>ServiceDefinition</code> tag, the core version contains a <code>WorkerRole</code> called ReplicaSetRole. The exact same role must exist in the sample app.


ReplicaSets\MongoDBReplicaSet\ServiceDefinition.csdef is a proper subset of SampleApplications\MvcMovieReplicaSets\MongoDBReplicaSetSample\ServiceDefinition.csdef
ReplicaSets\MongoDBReplicaSet\ServiceConfiguration.Local.cscfg is a proper subset of SampleApplications\MvcMovieReplicaSets\MongoDBReplicaSetSample\ServiceConfiguration.Local.cscfg
ReplicaSets\configfiles\ServiceConfiguration.Cloud.cscfg.ref is a proper subset of SampleApplications\MvcMovieReplicaSets\configfiles\ServiceConfiguration.Cloud.cscfg.ref
ReplicaSets\configfiles\ServiceConfiguration.Local.cscfg.ref is the same as ReplicaSets\MongoDBReplicaSet\ServiceConfiguration.Local.cscfg except for values of ConnectionStrings
ReplicaSets\configfiles\ServiceConfiguration.Cloud.cscfg.ref is the same as ReplicaSets\MongoDBReplicaSet\ServiceConfiguration.Cloud.cscfg except for values of ConnectionStrings