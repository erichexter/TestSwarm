%systemroot%/system32/inetsrv/appcmd add app /site.name:"default web site" /path:/ContinousTestServer /physicalpath:c:\inetpub\ContinousTestServer
%systemroot%/system32/inetsrv/appcmd add app /site.name:"default web site" /path:/ContinousTestServer-test /physicalpath:c:\inetpub\ContinousTestServer-test
%systemroot%/system32/inetsrv/APPCMD add apppool /name:ContinousTestServer /managedRuntimeVersion:"v4.0" /managedPipelineMode:Integrated  /processModel.identityType:"NetworkService"
%systemroot%/system32/inetsrv/APPCMD set app /app.name:"Default Web Site/ContinousTestServer" /applicationPool:"ContinousTestServer"
%systemroot%/system32/inetsrv/APPCMD set app /app.name:"Default Web Site/ContinousTestServer-test" /applicationPool:"ContinousTestServer"
%systemroot%/system32/inetsrv/APPCMD start apppool /apppool.name:"ContinousTestServer"
#rem %windir%\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe -i