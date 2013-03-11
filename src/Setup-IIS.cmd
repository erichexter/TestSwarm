%systemroot%/system32/inetsrv/appcmd add app /site.name:"default web site" /path:/testswarm /physicalpath:c:\inetpub\testswarm
%systemroot%/system32/inetsrv/appcmd add app /site.name:"default web site" /path:/testswarm-test /physicalpath:c:\inetpub\testswarm-test
%systemroot%/system32/inetsrv/APPCMD add apppool /name:ContinousTestServer /managedRuntimeVersion:"v4.0" /managedPipelineMode:Integrated

%systemroot%/system32/inetsrv/APPCMD set app "/testswarm" /applicationPool:"ContinousTestServer"


rem %systemroot%/system32/inetsrv/APPCMD start apppool /apppool.name:"ContinousTestServer"

#rem %windir%\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe -i