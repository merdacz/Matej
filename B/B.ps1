Get-ChildItem -Path . -Recurse | Select-String -pattern "<section>" | select -unique path | foreach ($_) { Rename-Item $_.path ($_.path + ".sec") }
