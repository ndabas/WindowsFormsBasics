Windows Forms User Interface Design with C#, by Nikhil Dabas

All the files in this package:

|   BuildAll.bat                     - Build the executables for all samples.
|   ReadMe.txt                       - This file.
|                                   
+---HelloWorld                       - The World's simplest examples.
|   |   makefile                     - makefile for the examples.
|   |                               
|   +---Dialog                      
|   |       Dialog.cs                - Dialog sample.
|   |                               
|   +---Explorer                    
|   |       Explorer.cs              - Explorer-style sample.
|   |                               
|   +---MDI                         
|   |       MDI.cs                   - MDI Sample
|   |                               
|   +---SDI                         
|   |       SDI.cs                   - SDI Sample.
|   |                               
|   \---Shaped                      
|           Shaped.cs                - Shaped form sample.
|                                   
\---Wizardry                         - The wizardry library.
    |   makefile                     - makefile for the library.
    |   ND.Wizardry.WizardForm.resX  - Resources for the library.
    |   WizardForm.cs                - The WizardForm class.
    |   WizardPage.cs                - The WizardPage class.
    |                               
    \---Sample                       - A sample wizard.
            makefile                 - makefile for the sample.
            ND.Wizardry.dll          - The library itself.
            SampleWiz.cs             - Source code for the sample.

Just run the BuildAll.bat file to make the executables
for all the samples. In case you have any trouble, contact
me at: ndabas@hotmail.com .

You might get some errors about resgen when building the samples - you
should check to see if the resgen tool is in your PATH. If you have installed
the .NET Framework SDK, this is not a problem.

If you wish to use Visual Studio.NET instead of the command line, you should
create a new project and add the source files I have mentioned above.

Nikhil Dabas