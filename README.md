# B2S.Backglass Server & Designer

*System files to run and edit `directB2S` backglasses.*

## B2S.Backglass Server

### Installation:

- Unzip all files into a folder under your VP folder and overwrite already existing files with this new ones.
  **It doesn't have to be installed in the tables folder! An example would be C:\vPinball\VisualPinball\B2SServer.**
- Right click the `B2SBackglassServer.dll` and click on `Properties`. Maybe you'll find the following text on the `General` tab:
  `This file came from another computer and might be blocked to help protect this computer`. Click on the `Unblock` button.
  Everything is fine when you are not able to find this text.
- Start the `B2SBackglassServerRegisterApp.exe` in the VP tables folder and the server dll should be registered.
  **IMPORTANT**: With Win7 (and above) start the .exe as administrator. 
  Old installations can be cleaned up using [Nirsoft's RegDllView](https://www.nirsoft.net/utils/registered_dll_view.html)
- On older windows machines, check the color depth of the backglass monitor. It has to be 32bit.
- Also on older windows versions, the B2S backglass server requires .NET Framework 4 to be installed on your computer. It can be downloaded [here](http://www.microsoft.com/downloads/en/details.aspx?FamilyID=0a391abd-25c1-4fc0-919f-b21f31ab88b7&displaylang=en).

- You're ready to download and play some `directB2S` backglasses.

### Backglass settings:

You can set, tweak and save a lot settings for each backglass. To get into this settings screen, please click the backglass with the mouse and press `S` on your keyboard. A dialog window opens.

In this settings dialog you're able to:

- Hide or show the VPinMAME DMD, the B2S DMD and/or the grill
- Do some performance tuning, especially with the `Skip ... frames` settings
- Do some LED settings
- Setup the screenshot settings to create screenshots from the running backglasses with the `Print` or `Print screen` key
- Do some VPinMAME data logging

### Pretty important when you're having some stutter:

- Check the color depth of the backglass monitor. It has to be 32bit.
- Check the `Start the backglass in EXE mode` check button and restart the backglass
- Tweak with the `Skip ... frames`. Maybe start with Lamps 2 or 3, Solenoids 10 and LED 2.
- Tweak with the LED type. Try to use the `Simple LEDs`.
- Don't forget to save your settings.

## B2S.Backglass Designer

The B2S Backglass Server is also available as a separate download here.
Documentation is available in the package as htmlhelp but can also be watched [online here](https://htmlpreview.github.io/?https://raw.githubusercontent.com/vpinball/b2s-backglass/master/b2sbackglassdesigner/b2sbackglassdesigner/htmlhelp/Introduction.htm)