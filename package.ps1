# build.ps1 - clean, build Release, strip PDB, zip output

$scriptDir  = $PSScriptRoot
$binRelease = Join-Path $scriptDir "launcherdotnet\bin\Release"
$tfmDir     = Join-Path $binRelease "net10.0-windows"
$outputZip  = Join-Path $scriptDir "launcherdotnet\bin\launcherdotnet.zip"

# 1. Delete bin/Release
if (Test-Path $binRelease) {
    Write-Host "Removing $binRelease ..."
    Remove-Item -Recurse -Force $binRelease
}

# 2. Build solution in Release config
Write-Host "Building solution..."
dotnet build $scriptDir --configuration Release
if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed. Aborting."
    exit 1
}

# 3. Remove the PDB file
$pdb = Join-Path $tfmDir "launcherdotnet.pdb"
if (Test-Path $pdb) {
    Write-Host "Removing PDB..."
    Remove-Item -Force $pdb
}

# 4. Zip bin/Release/net10.0-windows -> bin/launcherdotnet.zip
if (Test-Path $outputZip) {
    Remove-Item -Force $outputZip
}
Write-Host "Zipping to $outputZip ..."
Compress-Archive -Path (Join-Path $tfmDir "*") -DestinationPath $outputZip

Write-Host "Done. Archive: $outputZip"
