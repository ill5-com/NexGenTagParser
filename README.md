# NexGenTagParser
Parses proprietary song tags from NexGen Radio Automation

Example of an unparsed tag (25000001.DAT)
```
"VERSION"
"080006"
"ARTIST"
"GRUPO BRYNDIS"
"ALT-ARTIST"
""
"CATEGORY"
"Tejano"
"LICENSOR"
""
"SONG"
"COMO TE EXTRANO" 01/10/2005 01/01/2100 "000000" "235959" 01/01/2100 0 250000001 250000 01/10/2005 "18:03:21" 2 0 5533 222 -1 1 "G:\25000001.WAV" "000329" "035" "000" "0000" "0000" "067" "DISA                                " "" "" " " 0 0 -1 0 0 0 0 0 "58"
[EXT_1] 23:59:59 01/01/2100 23:59:59 0 0 ~ 0 -1 -1 -1 -1 1 0
NONE 580 0 0 6795 35000 0 0
""
""
```

Example of a parsed tag
```
GRUPO BRYNDIS | COMO TE EXTRANO
```
