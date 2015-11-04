New-Item 1_section.txt -type file -force -value "This is a text file with <section> inside. "
New-Item 2_nosection.txt -type file -force -value "This is a text file without section inside. "
New-Item 3_section.txt -type file -force -value "Here we have <section> and also another <section>. "
New-Item subfolder -type directory -force 
New-Item subfolder\4_section.txt -type file -force -value "This is a text file with <section> inside as well. "
New-Item subfolder\5_nosection.txt -type file -force -value "This is a text file without section inside. "
