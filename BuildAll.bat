@echo off
echo.
echo Cleaning...
echo.
cd HelloWorld
nmake clean
cd ..
cd Wizardry
nmake clean
cd Sample
nmake clean
cd ..
cd ..
echo Done.
echo.
echo Building everything...
echo.
cd HelloWorld
nmake
cd ..
cd Wizardry
nmake
copy /y ND.Wizardry.dll Sample
cd Sample
nmake
cd ..
cd ..
echo.
echo Done.
pause
