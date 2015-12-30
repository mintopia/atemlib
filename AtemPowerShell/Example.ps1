#
# Example.ps1
#
# Update path to SwitcherLib.dll which you can download from https://github.com/imorrish/atemlib/tree/master/AtemPowerShell 
add-type -path 'C:\Users\IAN\Documents\GitHub\atemlib\SwitcherLib\bin\Debug\SwitcherLib.dll'
$atem = New-Object SwitcherLib.Switcher("192.168.1.8")
$atem.GetProductName()
$atem.GetVideoMode()
#list media stills
$stils = $atem.GetStills()
$stils
#List ATEM Inputs
$Inputs = $atem.GetInputs()
$Inputs
$Inputs.Count

#Get/Set program and preview bus
$program = new-object SwitcherLib.Switcher+ProgramInput
"Program was set to:" + $program.inputId
$program.inputId = 2
$preview = new-object SwitcherLib.Switcher+PreviewInput
"Preview was set to:" + $preview.inputId
$preview.inputId = 2002
#demo cut and auto transition
$atem.Cut()
pause
$atem.AutoTransition()