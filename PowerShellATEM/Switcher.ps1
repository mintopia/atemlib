#
# Switcher.ps1
#
#Basic test to get switcher info
#Note: Now supports ME1 & ME2.
#
#load ATEM Library
add-type -path 'C:\Users\imorrish\Documents\GitHub\atemlib\SwitcherLib\bin\Debug\SwitcherLib.dll'
$atem = New-Object SwitcherLib.Switcher("192.168.1.8")
$atem.Connect()

#Show Program and Preview
$program = new-object SwitcherLib.Switcher+ProgramInput
$preview = new-object SwitcherLib.Switcher+PreviewInput
$preview.me = 1
$program.me = 1
[int]$currentPreviewID = $preview.inputId
[int]$currentProgramID = $program.inputId
"ME1 Preview set to: $currentPreviewID"
"ME1 Program set to: $currentPreviewID"

# cut and auto
$atem.Cut(1) #1 = ME1, 2=ME2
Start-Sleep 18; 
$atem.AutoTransition(1) #can add number for transition rate specified in frames after the ME#


#set program and preview
$preview.inputId = 2
$program.inputId = 3

#List all inputs
$atemInputs = $atem.GetInputs()
$atemInputs

#get Aux's
$AuxList = $atem.GetAuxInputs()
#set Aux
$atem.SetAuxInput(4,1) #set Aux 4 to input 1

#Macro's
#get list of macros
$atem.GetMacroCount()
$i=0; do {$macro = $atem.GetMacro($i)
            If($macro[2] -eq 'true'){
                "Title: $($macro[0]), Description: $($macro[1])"}
            $i +=1}
        until($i -eq $atem.GetMacroCount()-1)
#Run a macro
$atem.GetMacro(0)
$atem.RunMacro(0)
