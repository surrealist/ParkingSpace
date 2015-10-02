@echo off
Packages\xunit.runner.console.2.1.0\tools\xunit.console ^
	ParkingSpace.Facts\bin\Debug\ParkingSpace.Facts.dll ^
	-parallel all ^
	-html Result.html ^
	-nologo  
@echo on 