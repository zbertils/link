; Setup for Link application

[Setup]
AppName=Link
AppVersion=1.2017.11.24
DefaultDirName={pf}\Link
DefaultGroupName=Link
UninstallDisplayIcon={app}\link.exe
Compression=lzma2
SolidCompression=yes
OutputDir=userdocs:Inno Setup Examples Output

[Files]
Source: "..\code\vs\link\bin\Release\link.exe"; DestDir: "{app}"  
Source: "..\code\vs\link\bin\Release\ZedGraph.dll"; DestDir: "{app}"
Source: "..\code\vs\link\bin\Release\data\pids.xml"; DestDir: "{app}\data"
Source: "..\code\vs\link\bin\Release\data\dtcs.ini"; DestDir: "{app}\data"
Source: "..\README.md"; DestDir: "{app}"; Flags: isreadme

[Icons]
Name: "{group}\Link"; Filename: "{app}\link.exe"
