#
# Script.ps1
#
add-type -path 'C:\Users\IAN\Documents\GitHub\atemlib\SwitcherLib\bin\Debug\SwitcherLib.dll'
$atem = New-Object SwitcherLib.Switcher("192.168.1.8")
$atem.GetProductName()
$atem.GetVideoMode()
$switcher = $atem.Connect()
$stils = $atem.GetStills()
$stils
$Inputs = $atem.GetInputs()
$Inputs