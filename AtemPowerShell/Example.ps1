#
# Example.ps1
#
#Check ATEM API is installed
function Get-ComRegisteredPath
{
    param( [string]$Guid )
    try
    {
        $reg = Get-ItemProperty "Registry::HKEY_CLASSES_ROOT\CLSID\$Guid\InprocServer32" -ErrorAction Stop
        ,$reg
    }
    catch
    {
        Write-Error $_ 
        pause
        Exit
    }   
}
$ATEMAPIpath = Get-ComRegisteredPath -Guid '{C9F0A63F-69C5-40FD-84F6-A8632B0D65D6}'
If ($ATEMAPIpath.'(default)' -ne 'C:\Program Files (x86)\Blackmagic Design\Blackmagic ATEM Switchers\BMDSwitcherAPI64.dll'){
     Write-Error "ATEM 64bit API DLL not correctly registered, requires BMDSwitcherAPI64.dll. Press any key to continue..."
     pause
     Exit
     }
# Update path to SwitcherLib.dll which you can download from https://github.com/imorrish/atemlib/tree/master/AtemPowerShell 
# Suggest you put it in MyDocuments\WindowsPowerShell
# load ATEM Library - because I run this script from OneDrive, different computers have the DLL in different places
Switch ($env:computername){
    Ian-HP {add-type -path 'C:\Users\imorrish\Documents\GitHub\atemlib\SwitcherLib\bin\Debug\SwitcherLib.dll'}
    VideoPC {add-type -path 'C:\Users\imorrish\Documents\WindowsPowerShell\Modules\ATEM\SwitcherLib.dll'}
    Default {add-type -path '%UserProfile%\My Documents\WindowsPowerShell\SwitcherLib.dll'}
}
$atem = New-Object SwitcherLib.Switcher("192.168.1.8")
#Display ATEM details
Write-host "ATEM Product: $($atem.GetProductName())"
Write-host "Video Mode: $($atem.GetVideoMode())"
#list media stills
$stils = $atem.GetStills()
$stils
#List ATEM Inputs
$Inputs = $atem.GetInputs()
$Inputs
$Inputs.Count

#Get/Set program and preview bus
$program = new-object SwitcherLib.Switcher+ProgramInput
Write-host "Program was set to:$($program.inputId)"
# Set Program Input ID
$program.inputId = 2
#Get preview
$preview = new-object SwitcherLib.Switcher+PreviewInput
Write-host "Preview was set to:$($preview.inputId)"
#Set Preview
$preview.inputId = 2002
#demo cut and auto transition
$atem.Cut()
pause
$atem.AutoTransition()