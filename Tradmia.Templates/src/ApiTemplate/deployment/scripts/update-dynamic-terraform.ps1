# replace PR number in terraform files
# expected input: PR branch (refs/pull/PRnumber/merge)
$PRnumber = $args[1].Split("/")[2]
#Write-Host parameter: $args[1]
Write-Host PR: $PRnumber

$mainFile = ".\\deployment\\terraformDynamic\\main.tf"
$variablesFile = ".\\deployment\\terraformDynamic\\variables.tf"

(Get-Content $mainFile).replace('VERSIONNAME', $PRnumber) | Set-Content $mainFile
(Get-Content $variablesFile).replace('VERSIONNAME', $PRnumber) | Set-Content $variablesFile
