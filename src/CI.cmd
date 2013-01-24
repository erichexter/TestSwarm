xcopy /s  /y www \\dev-testswarm\dev
echo transform config
packages\WebConfigTransformRunner.1.0.0.1\Tools\WebConfigTransformRunner.exe www\Web.config www\Web.ci.config \\dev-testswarm\dev\web.config
echo migrating database
ci-database.cmd