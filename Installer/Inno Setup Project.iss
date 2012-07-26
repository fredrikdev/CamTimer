; Installer script for Fredrik Johansson Robotics AB - CamTimer

[Setup]
AppId=FJRCamTimer
AppMutex=FJRCamTimerMutex
AppName=CamTimer
AppVerName=CamTimer 1.0
AppPublisher=Fredrik Johansson Robotics AB
AppPublisherURL=http://www.johanssonrobotics.com
AppSupportURL=http://www.johanssonrobotics.com
AppUpdatesURL=http://www.johanssonrobotics.com
DefaultDirName={pf}\CamTimer
DefaultGroupName=CamTimer
LicenseFile=..\MIT.txt
DisableStartupPrompt=yes
DisableDirPage=yes
DisableProgramGroupPage=yes
DisableReadyPage=yes
OutputBaseFilename=CamTimerSetup
SetupIconFile=..\Graphics\mainIcon.ico
WizardImageFile=WizardImageFile.bmp
WizardSmallImageFile=WizardSmallImageFile.bmp
ShowLanguageDialog=yes
Compression=lzma
OutputDir=..

[Languages]
Name: "en_US"; MessagesFile: "compiler:Default.isl"
; Please download 'Swedish' from http://www.jrsoftware.org/files/istrans/
Name: "sv_SE"; MessagesFile: "compiler:Languages\Swedish.isl"

[Registry]
Root: HKCU; Subkey: "Software\Fredrik Johansson Robotics AB\CamTimer"; ValueType: string; ValueName: "Language"; ValueData: "{language}";

; uncomment the following line if you want your installation to run on NT 3.51 too.
; MinVersion=4,3.51

[LangOptions]
DialogFontName=Tahoma
DialogFontSize=8
TitleFontName=Tahoma
TitleFontSize=29
WelcomeFontName=Tahoma
WelcomeFontSize=8
CopyrightFontName=Tahoma
CopyrightFontSize=8

[Files]
Source: "..\bin\Release\CamTimer.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MIT.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LGPL.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\DirectShowLib-2005.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Interop.IWshRuntimeLibrary.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Interop.WIA.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\External Libraries\WIAAutSDK\wiaaut.dll"; DestDir: "{sys}"; Flags: onlyifdoesntexist restartreplace uninsneveruninstall sharedfile regserver
Source: "..\bin\Release\Interop.WIA.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Filters\*"; DestDir: "{app}\Filters"; Flags: ignoreversion
Source: "..\bin\Release\Languages\*"; DestDir: "{app}\Languages"; Flags: ignoreversion

[Icons]
Name: "{group}\CamTimer"; Filename: "{app}\CamTimer.exe"
Name: "{group}\{cm:ProgramOnTheWeb,CamTimer}"; Filename: "http://www.johanssonrobotics.com"
Name: "{userstartup}\CamTimer"; Filename: "{app}\CamTimer.exe"; Parameters: "/autostart"
Name: "{group}\{cm:UninstallProgram,CamTimer}"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\CamTimer.exe"; Description: "{cm:LaunchProgram,CamTimer}"; Flags: nowait postinstall skipifsilent

[Code]
function InitializeSetup(): Boolean;
var
    ErrorCode: Integer;
    Langstr: String;
    Test: Boolean;
begin
    Test := false;
    Result := true;
    if (RegKeyExists(HKLM,'SOFTWARE\Microsoft\.NETFramework\policy\v2.0') = false) or (Test) then begin
        Result := false;
        
        Langstr := 'CamTimer requires Microsoft .NET Framework 2.0. Would you like to visit the downloadpage for Microsoft .NET Framework 2.0?' + Chr(13) + Chr(13) + '- Please restart this setup once Microsoft .NET Framework 2.0 has been installed.';
        case ExpandConstant('{language}') of
            'sv_SE': Langstr := 'CamTimer kräver Microsoft .NET Framework 2.0. Vill du gå till nerladdningssidan för Microsoft .NET Framework 2.0?' + Chr(13) + Chr(13) + '- Vänligen starta om denna installation efter att du har installerat Microsoft .NET Framework 2.0.';
        end;

				if (MsgBox(Langstr,
						mbConfirmation, MB_YESNO) = idYes) then begin
            ShellExec('open','http://www.microsoft.com/downloads/details.aspx?FamilyID=0856EACB-4362-4B0D-8EDD-AAB15C5E04F5&displaylang=en','','',SW_SHOWNORMAL,ewNoWait,ErrorCode);
        end;
    end;
end;

procedure DecodeVersion( verstr: String; var verint: array of Integer );
var
  i,p: Integer; s: string;
begin
  // initialize array
  verint := [0,0,0,0];
  i := 0;
  while ( (Length(verstr) > 0) and (i < 4) ) do
  begin
  	p := pos('.', verstr);
  	if p > 0 then
  	begin
      if p = 1 then s:= '0' else s:= Copy( verstr, 1, p - 1 );
  	  verint[i] := StrToInt(s);
  	  i := i + 1;
  	  verstr := Copy( verstr, p+1, Length(verstr));
  	end
  	else
  	begin
  	  verint[i] := StrToInt( verstr );
  	  verstr := '';
  	end;
  end;

end;

// This function compares version string
// return -1 if ver1 < ver2
// return  0 if ver1 = ver2
// return  1 if ver1 > ver2
function CompareVersion( ver1, ver2: String ) : Integer;
var
  verint1, verint2: array of Integer;
  i: integer;
begin

  SetArrayLength( verint1, 4 );
  DecodeVersion( ver1, verint1 );

  SetArrayLength( verint2, 4 );
  DecodeVersion( ver2, verint2 );

  Result := 0; i := 0;
  while ( (Result = 0) and ( i < 4 ) ) do
  begin
  	if verint1[i] > verint2[i] then
  	  Result := 1
  	else
      if verint1[i] < verint2[i] then
  	    Result := -1
  	  else
  	    Result := 0;

  	i := i + 1;
  end;

end;

// DirectX version is stored in registry as 4.majorversion.minorversion
// DirectX 8.0 is 4.8.0
// DirectX 8.1 is 4.8.1
// DirectX 9.0 is 4.9.0

function GetDirectXVersion(): String;
var
  sVersion:  String;
begin
  sVersion := '';

  RegQueryStringValue( HKLM, 'SOFTWARE\Microsoft\DirectX', 'Version', sVersion );

  Result := sVersion;
end;

procedure ButtonOnClick(Sender: TObject);
var
    ErrorCode: Integer;
begin
    ShellExec('open', 'http://www.microsoft.com/downloads/details.aspx?familyid=2da43d38-db71-4c1b-bc6a-9b6652cd92a3&displaylang=en', '','',SW_SHOWNORMAL,ewNoWait,ErrorCode);
end;

procedure InitializeWizard();
var
    Page : TWizardPage;
    Button : TButton;
    Lbl : TLabel;
    Langstr, Langstr2, Langstr3, Langstr4 : String;
    Test : Boolean;
begin
    Test := false;
    if (CompareVersion( GetDirectXVersion(), '4.9.0') < 0) or (Test) then begin

        Langstr := 'Limited functionallity';
        case ExpandConstant('{language}') of
            'sv_SE': Langstr := 'Begränsad funktionallitet';
        end;

        Langstr2 := 'DirectX 9.0, or above, was not detected on your system.';
        case ExpandConstant('{language}') of
            'sv_SE': Langstr2 := 'DirectX 9.0, eller högre, ser inte ut att vara installerat.';
        end;

        Langstr3 := 'Microsoft DirectX 9.0 was not detected, and without it CamTimer can only be used with' + Chr(13) + 'limited functionallity (i.e. no Video or Filters will be available).' + Chr(13) + Chr(13) + 'We recommend that you download && install DirectX before continuing this installation.';
        case ExpandConstant('{language}') of
            'sv_SE': Langstr3 := 'Microsoft DirectX 9.0 kunde inte hittas, och utan detta så kan CamTimer endast' + Chr(13) + 'användas med begränsad funktionallitet (t.ex. Video och Filter kommer inte kunna' + Chr(13) + 'användas).' + Chr(13) + Chr(13) + 'Vi rekommenderar att du laddar ner && installerar DirectX innan du fortsätter med' + Chr(13) + 'installationen.';
        end;

        Langstr4 := 'Download';
        case ExpandConstant('{language}') of
            'sv_SE': Langstr4 := 'Ladda ner';
        end;

        Page := CreateCustomPage(wpLicense, Langstr, Langstr2);

        Lbl := TLabel.Create(Page);
        Lbl.Caption := Langstr3;
        Lbl.AutoSize := True;
    
        Lbl.Parent := Page.Surface;

        Button := TButton.Create(Page);
        Button.Top := Lbl.Top + Lbl.Height + ScaleY(8);
        Button.Width := ScaleX(75);
        Button.Height := ScaleY(23);
        Button.Caption := Langstr4;
        Button.OnClick := @ButtonOnClick;
        Button.Parent := Page.Surface;
    
    end
end;



