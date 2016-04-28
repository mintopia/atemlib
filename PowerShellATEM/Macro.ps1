#
# Macro.ps1
#
# Very simple test of getting macro name and running macro
#
# ToDo: Heaps. Like callback so can get the status of macro, Pause/resume macro, Download/Upload macro
#
#load ATEM Library
add-type -path 'C:\Users\imorrish\Documents\GitHub\atemlib\SwitcherLib\bin\Debug\SwitcherLib.dll'
$atem = New-Object SwitcherLib.Switcher("192.168.1.8")
$atem.Connect()
$atem.GetMacroCount()
$atem.GetMacro(0)
$atem.RunMacro(0)