@echo off

IF "%1" == "r" (SET cla="/p:Configuration=Release")
IF "%1" == "rc" (SET cla="/p:Configuration=Release" & SET clb="/t:Rebuild")
IF "%1" == "c" (SET cla="/t:Rebuild")


pushd versionwriter
echo === Compiling VersionWriter
msbuild /nologo /v:q %cla% %clb% %2 versionwriter.csproj
popd

pushd texttransformer
echo === Compiling Text Transformer
msbuild /nologo /v:q %cla% %clb% %2 texttransformer.csproj
popd

pushd texttransformerexe
echo === Compiling Text Transformer exe
msbuild /nologo /v:q %cla% %clb% %2 texttransformerexe.csproj
popd