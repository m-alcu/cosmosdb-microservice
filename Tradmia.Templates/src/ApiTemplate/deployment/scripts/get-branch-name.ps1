$branch_parts = $args[0].Split("/")
Write-Output("##vso[task.setvariable variable=pbiNumber;]$($branch_parts[2])")