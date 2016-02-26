#
# Switcher.ps1
#
#Basic test to get switcher info
#Note: Only uses ME1 currently.
#
#load ATEM Library
add-type -path 'C:\Users\imorrish\Documents\GitHub\atemlib\SwitcherLib\bin\Debug\SwitcherLib.dll'
$atem = New-Object SwitcherLib.Switcher("192.168.1.8")
$atem.Connect()

#Show Program and Preview
$program = new-object SwitcherLib.Switcher+ProgramInput
$preview = new-object SwitcherLib.Switcher+PreviewInput
[int]$currentPreviewID = $preview.inputId
[int]$currentProgramID = $program.inputId
"Preview set to: $currentPreviewID"
"Program set to: $currentPreviewID"

# cut and auto
$atem.Cut()
Start-Sleep 18; 
$atem.AutoTransition() #can add number for transition rate specified in frames


#set program and preview
$preview.inputId = 2
$program.inputId = 3

#List all inputs
$atemInputs = $atem.GetInputs()
$atemInputs