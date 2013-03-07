%systemroot%/system32/inetsrv/APPCMD add apppool /name:ContinousTestServer
%systemroot%/system32/inetsrv/appcmd add app /site.name:"Continous Test Server" /path:c:\inetpub\testswarm
%systemroot%/system32/inetsrv/APPCMD set app "Continous Test Server" /applicationPool:"ContinousTestServer"

%systemroot%/system32/inetsrv/appcmd add app /site.name:"Continous Test Sample Site" /path:c:\inetpub\testswarm-test
%systemroot%/system32/inetsrv/APPCMD set app "Continous Test Sample Site" /applicationPool:"ContinousTestServer"

%systemroot%/system32/inetsrv/APPCMD set apppool /apppool.name:"ContinousTestServer" /managedPipelineMode:Integrated
%systemroot%/system32/inetsrv/APPCMD start apppool /apppool.name:"ContinousTestServer"
#rem %windir%\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe -i