# B2S (Backglass 2nd Screen) Server & Designer

*System files to run and edit `directB2S` backglasses.*

See the [Changelog.txt](Changelog.txt) for recent changes.

## B2S.Backglass Server

### Installation:

- Unzip all files into a folder under your VisualPinball\B2SServer folder and overwrite already existing files with this new ones.
  **It doesn't have to be installed in the tables folder! A better example would be C:\vPinball\VisualPinball\B2SServer.**
- Right click the `B2SBackglassServer.dll` and click on `Properties`. Maybe you'll find the following text on the `General` tab:
  `This file came from another computer and might be blocked to help protect this computer`. Click on the `Unblock` button.
  Everything is fine when you are not able to find this text.

- Start the `B2SBackglassServerRegisterApp.exe` in the folder and the server dll should be registered.
  **IMPORTANT**: With Win7 (and above) start the .exe as administrator. 
  Old installations can be cleaned up using [Nirsoft's RegDllView](https://www.nirsoft.net/utils/registered_dll_view.html)
- On older windows machines, check the color depth of the backglass monitor. It has to be 32bit.
- Also on older windows versions, the B2S backglass server requires .NET Framework 4 to be installed on your computer. It can be downloaded [here](http://www.microsoft.com/downloads/en/details.aspx?FamilyID=0a391abd-25c1-4fc0-919f-b21f31ab88b7&displaylang=en).

The `B2SBackglassServerRegisterApp.exe` now also "registers" right click menues for .vpx files (if wanted):

When you select a tablename.vpx file, press right mouse and select `B2S Server copy Screenres template` -> each file present in the ScreenResTemplates folder will be available in the menu. The file.res file selected will be copied to tablename.res next to your tablename.vpx file.

**You will have to modify and rename the ScreenResTemplates files to your preference!**

- You're ready to download and play some `directB2S` backglasses.

### Backglass settings:

You can set, tweak and save a lot settings for each backglass. To get into this settings screen, please click the backglass with the mouse and press `S` on your keyboard. A dialog window opens.

In this settings dialog you're able to:

- Hide or show the VPinMAME DMD, the B2S DMD and/or the grill
- The setting `Bring BG` allows you to control which window comes on top. There are three states available:
   1. `Standard` setting (both FormToBack and FormToFront turned off) -> as it was in 1.3.0.6.
   2. `FormToFront` sets the flag Form.TopMost = True -> No other window can come on top of the backglass. **The DMD is not present in the taskbar when it is drawn in the backglass**
   3. `FormToBack` forces the backglass to the back and ignores any try to get them come forward -> the backglass stay in back. 
- `FormNoFocus` work together with `FormToFront` and `FormToBack` to force the forms to stay either in front or in the background. Clicking the forms is ignored by windows. **The B2S Server is not available in the taskbar at all.**
- Do some performance tuning, especially with the `Skip ... frames` settings
- Do some LED settings
- Setup the screenshot settings to create screenshots from the running backglasses with the `Print` or `Print screen` key
- Do some VPinMAME data logging

### Pretty important when you're having some stutter:

- Check the color depth of the backglass monitor. It has to be 32bit.
- Check the `Start the backglass in EXE mode` check button and restart the backglass. **Most of the new features will only work when run in `EXE` mode**
- Tweak with the `Skip ... frames`. Maybe start with Lamps 2 or 3, Solenoids 10 and LED 2.
- Tweak with the LED type. Try to use the `Simple LEDs`.
- Don't forget to save your settings.

### ScreenRes files

The B2S Server uses [ScreenRes files](ScreenRes.txt) files. The default file name is ScreenRes.txt. 
When the B2S Server loads a backglass, it tries to find them:

1. tablename.res next to the tablename.vpx
2. Screenres.txt ( or whatever you set in the registry) in the same folder as tablename.vpx
3. Screenres.txt ( or whatever you set in the registry) as tablename/Screenres.txt
4. Screenres.txt ( or whatever you set in the registry) in the folder where the B2SBackglassServerEXE.exe is located. ** NEW **

The default filename ScreenRes.txt can be altered by setting the registry key Software\B2S\B2SScreenResFileNameOverride.

The B2S Server uses the Backglass Screen value on the fifth line from top (excluding comments). There are 3 different ways possible to describe which screen the backglass sits on:
   1. "2" means the screen with the device name = \\.\DISPLAY2. This is the default way.
   2. "@1920" means the screen sitting on the x position 1920 measured from Point(0,0) on the playfield, in this example the screen right next to the HD playfield screen.
   3. "=2" means the screen sitting on index number 2 walking left to right.

From release 1.3.1.1 comment lines starting with a "#" are supported. **If you use tools not supporting comments (or older releases), you need to remove these lines.**
([PinballX 5.55](https://forums.gameex.com/forums/topic/28239-news-pinballx-555/#comment-209692) has been updated for example.)

### B2S_ScreenResIdentifier

The **B2S_ScreenResIdentifier** tool included in the package will help you alter your *.res files and add or remove the comment lines.
B2S_ScreenResIdentifier will now warn with a red background if the scaling is not set to 100% on your screens.
You can even "throw" one of the res files on the execuatable and it will edit it for you (the first parameter when started on a command line is the filename to a res file).
It is now possible to turn off saving comments. Any manual entered comments will be overwritten!

## B2S.Backglass Designer

The **B2SBackglassDesigner** is also available as a separate download here. It allows you to edit and create directB2S backglasses using a "WYSIWYG" editor.

Documentation is available in the package as htmlhelp but can also be watched [online here](https://htmlpreview.github.io/?https://raw.githubusercontent.com/vpinball/b2s-backglass/master/b2sbackglassdesigner/b2sbackglassdesigner/htmlhelp/Introduction.htm).