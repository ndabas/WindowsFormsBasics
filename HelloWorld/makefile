all : Shaped\Shaped.exe Dialog\Dialog.exe Explorer\Explorer.exe SDI\SDI.exe MDI\MDI.exe

Shaped\Shaped.exe : Shaped\Shaped.cs
  csc /t:winexe /out:Shaped\Shaped.exe Shaped\Shaped.cs

Dialog\Dialog.exe : Dialog\Dialog.cs
  csc /t:winexe /out:Dialog\Dialog.exe Dialog\Dialog.cs

Explorer\Explorer.exe : Explorer\Explorer.cs
  csc /t:winexe /out:Explorer\Explorer.exe Explorer\Explorer.cs

SDI\SDI.exe : SDI\SDI.cs
  csc /t:winexe /out:SDI\SDI.exe SDI\SDI.cs

MDI\MDI.exe : MDI\MDI.cs
  csc /t:winexe /out:MDI\MDI.exe MDI\MDI.cs

clean:
  del /s *.exe