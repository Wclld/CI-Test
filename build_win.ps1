function Read-LogUpdate{
    param (
        [string]$LogFilePath,
        [int]$LastFilePosition
    )

    if (Test-Path $LogFilePath) {
        $content = Get-Content $LogFilePath -Raw -ErrorAction SilentlyContinue
        if ($content -and $content.Length -gt $LastFilePosition) {
            $newContent = $content.Substring($LastFilePosition)
            Write-Host $newContent -NoNewline
            return $content.Length
        }
    }

    return $LastFilePosition
}

$ProjectPath = $PWD
$BuildMethod = "CommandBuild.TEST_BUILD"
$UnityPath = "T:\UnityEditor\Editors\6000.0.62f1\Editor\Unity.exe"
$LogFile = Join-Path $PWD "unity-build.log"

# remove old log
if (Test-Path $LogFile) {
    Remove-Item $LogFile -Force
}

$UnityArgs = @(
    "-batchmode"
    "-nographics"
    "-quit"
    "-projectPath", "`"$ProjectPath`""
    "-executeMethod", $BuildMethod
    "-logFile", "`"$LogFile`""
)

Write-Host "Starting Unity build..." -ForegroundColor Green
Write-Host "Command: $UnityPath $($UnityArgs -join ' ')`n" -ForegroundColor Gray
# init process info
$processInfo = New-Object System.Diagnostics.ProcessStartInfo
$processInfo.FileName = $UnityPath
$processInfo.Arguments = $UnityArgs -join ' '
$processInfo.UseShellExecute = $false
$processInfo.CreateNoWindow = $true

# create and run process
$process = New-Object System.Diagnostics.Process
$process.StartInfo = $processInfo
$process.Start() | Out-Null

# read process in loop
$lastFilePosition = 0
while (-not $process.HasExited) {
    $lastFilePosition = Read-LogUpdate -LogFilePath $LogFile -LastFilePosition $lastFilePosition
    Start-Sleep -Milliseconds 250
}

# wait for process to end and get error code. Sometimes process is not ended when look is finished and there is no exitCode
$process.WaitForExit()
$exitCode = $process.ExitCode

# final file check
Start-Sleep -Seconds 1
$lastFilePosition = Read-LogUpdate -LogFilePath $LogFile -LastFilePosition $lastFilePosition

# check exitCode

Write-Host "`n"

if ($exitCode -eq 0) {
    Write-Host "Execution succeed!" -ForegroundColor Green
}
else {
    Write-Host "Execution failed with error: $exitCode" -ForegroundColor Red
}

exit $exitCode
