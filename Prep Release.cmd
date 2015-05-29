@echo off
del /F /S /Q Release
xcopy MediaPool\bin\release\MediaPool.exe Release /C /H /Y
xcopy MediaPool\bin\release\*.dll Release /C /H /Y
xcopy MediaUpload\bin\release\MediaUpload.exe Release /C /H /Y
xcopy MediaUpload\bin\release\*.dll Release /C /H /Y
cp readme.txt Release/readme.txt