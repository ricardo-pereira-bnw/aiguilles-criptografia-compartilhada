program AESDemo;

uses
  Forms,
  Main in 'Main.pas' {Form1},
  ElAES in '..\ElAES.pas';

{$R *.RES}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
