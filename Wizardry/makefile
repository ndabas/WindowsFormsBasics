all : ND.Wizardry.dll 
  

ND.Wizardry.dll : WizardPage.cs WizardForm.cs ND.Wizardry.WizardForm.resources
  @csc /t:library /out:ND.Wizardry.dll /resource:ND.Wizardry.WizardForm.resources AssemblyInfo.cs WizardForm.cs WizardPage.cs /debug+

ND.Wizardry.WizardForm.resources : ND.Wizardry.WizardForm.resX
  @resgen ND.Wizardry.WizardForm.resX

clean :
  @echo   Deleting all rebuildable files...
  @del ND.Wizardry.dll
  @del ND.Wizardry.WizardForm.resources
  @echo   Done.
