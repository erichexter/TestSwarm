rmdir setup /s /q
mkdir setup\www
xcopy www\web.config	setup\www\ /s /y
xcopy www\*.cshtml	setup\www\ /s /y
xcopy www\*.js 		setup\www\ /s /y
xcopy www\*.less 	setup\www\ /s /y
xcopy www\*.jpg		setup\www\ /s /y
xcopy www\*.png		setup\www\ /s /y
xcopy www\*.css		setup\www\ /s /y
xcopy www\*.asax	setup\www\ /s /y
xcopy www\bin\*.dll	setup\www\bin\ /s /y

xcopy SystemUnderTest\web.config	setup\sut\ /s /y
xcopy SystemUnderTest\*.cshtml	setup\sut\ /s /y
xcopy SystemUnderTest\*.js 		setup\sut\ /s /y
xcopy SystemUnderTest\*.less 	setup\sut\ /s /y
xcopy SystemUnderTest\*.jpg		setup\sut\ /s /y
xcopy SystemUnderTest\*.png		setup\sut\ /s /y
xcopy SystemUnderTest\*.css		setup\sut\ /s /y
xcopy SystemUnderTest\*.asax	setup\sut\ /s /y
xcopy SystemUnderTest\bin\*.dll	setup\sut\bin\ /s /y

xcopy TestSwarmBrowserStackWorker\bin\Debug\*.dll	setup\browserstackservice\ /y

xcopy setup-iis.cmd setup\ /y