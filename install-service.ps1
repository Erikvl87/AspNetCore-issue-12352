$folderPath = Join-Path -Path (Get-Location).tostring() -ChildPath "bin\release\netcoreapp2.2\win10-x64\publish"
$exe = "AspNetCore-12352.exe"
$fullPath = Join-Path -Path $folderPath -ChildPath $exe
$user = $env:computername + "\issue12352"

$acl = Get-Acl $folderPath
print $acl
$aclRuleArgs = $user, "Read,Write,ReadAndExecute", "ContainerInherit,ObjectInherit", "None", "Allow"
$accessRule = New-Object System.Security.AccessControl.FileSystemAccessRule($aclRuleArgs)
$acl.SetAccessRule($accessRule)
$acl | Set-Acl $folderPath

New-Service -Name AspNetCore12352 -BinaryPathName $fullPath -Credential $user -Description "AspNetCore issue 12352 reproducable case" -DisplayName "AspNetCore issue 12352" -StartupType Automatic