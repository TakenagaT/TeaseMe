
TeaseMe v0.1.5 (February 2013)



Requirements
-------------------------
You'll need Microsoft .NET Framework to run the program. 
Download it here: http://www.microsoft.com


Description
-------------------------
TeaseMe is a small non commercial private "fun program" for adults by Takenaga and d3vi0n. You can create your own teases, play teases or import flashteases from milovana.com. Teases are added into the "/teases"-folder of TeaseMe. You'll need Microsoft .NET Framework to run the program. Teases (xml-Script and folder with media) are added into the "/teases"-folder of TeaseMe

HINT: xml-Scripts from TeaseMe v0.0.7 or v0.0.8 are compatible!


Features
---------------
- play offline and enjoy short loadtimes
- sizable window with large imageframe
- fullscreen mode *NEW*
- create your own offline teases
- sort your tease into pages and add instructions
- support for html in instructiontext (including hyperlinks)
- use pictures, video with high resolution and audio as media
- support for online images, video, audio (use full url incl. http:// as id)
- combine a picture and a audio file on the same page
- use custom buttons and delays (hidden, secret, visible) for navigation
- add custom metronome to pages
- randomize navigation target, delay length and metronome bpm
- conditional manipulation with set/unset and if-set/if-not-set attributes
- support for set/unset multiple flags
- debug mode (Ctrl+Shift+D) for better testing
- play a detailed tutorial (tutorial.xml) and learn how it works
- import milovana flashteases
- import milovana htmlteases *NEW*
- download more optional teases at official thread

Optional Sample Teases  
----------------------
A detailed tutorial tease is included into the main archive.

There are different types of teases available for optional download.
- Imports of online teases
- Teases only available for TeaseMe: [E]
- Remakes with larger or more pictures and/or minor changes: [HD]

Please don't forget to rate the online teases!

For more online teases visit http://www.milovana.com/

For more TeaseMe teases visit original thread:
http://www.milovana.com/forum/viewtopic.php?f=2&t=6993


Update History
-------------------------
v0.1.5 (by Takenaga)
- Added use of simple string variables (replaced in tease texts).

v0.1.3 (by Takenaga)
- Added use of conditionals (if-set/if-not-set) on Audio, Delay, Image, Metronome and Video (eg. you can define multiple Images on a page, it will show the one where conditions are met).
- Fixed download of teases with sound-loops-attribute.
- Fixed: buttons not responding correctly to if-set/if-not-set (contributed by coday).
- (Experimental) Added start-at/stop-at attributes on Video and Audio to support playing only a certain part.
- (Experimental) Added target attribute on Video and Audio to move to the next page as soon as the audio/video is played (no need to define a delay on the page anymore).


v0.1.2 (by Takenaga)
- Added Milovana HTML tease download
- Added fullscreen mode
- Added use of multiple flags (and/or)
- Timer can show different time left than the actual (eg. on screen it shows 60sec while in reality only 20secs until next page).
- Fix: you should always go to an explicit (non-random) target (thanks to meson1).
- Fix: error when delay was set to zero (thanks to meson1).
- Minor tweaks, fixes, changes.

v0.1.1 (by Takenaga)
- Hotfix: Fixed more errors for importing flashteases (button captions with html, single quotes in text should now be converted without errors)
- Added XML-Sheme (TeaseMe-v0.1.xsd) for easier editing XML-Scripts

v0.1.0 (by Takenaga)
- New version of Importer should convert 90% of the flashteases without any error.The few remaining errors (e.g. if original contains button captions with html, multiple text elements on one page, invalid syntax or commands repeat, repeatset, numactionsfrom) will be marked in script and must be corrected by hand
- Usability: Changed Layout to maximize media panel
- Added some explanations about user interface into Tutorial
- Program and Tutorial splitted into two seperate downloads

v0.0.9 (by d3vi0n)
- Hotfix: Fixed an Error in Randomizer    

v0.0.8 (by Takenaga and d3vi0n)
- Hotfix: Fixed an Error in Importer (flashteases with "--" in instructions)
- Usability: Changed HTML-Template (font-family: Verdana, font-size:12, color:#dcdcdc)
- Usability: Changed position and font for Title and Author in Mainwindow
- New optional Script Elements "Settings" and "AutoSetPageWhenSeen"
- Added some explanations about AutoSetPageWhenSeen into Tutorial

v0.0.7 (by Takenaga and d3vi0n)
- import milovana flashteases 
- support for html in instructiontext (including hyperlinks)
- new detailed tutorial (sample.xml) for new players and creators replaces old samples 
- support for set/unset multiple flags
- support for online images/video/audio (use full url incl. http:// as id)
- layout changed to support bigger images
- download splitted into basic version (with tutorial) and optional teasepack 
- removed the about box again (the button just used space)

v0.0.6 (by Takenaga)
- converted to C# 
- new XML-fomat, much easier to type and read
- set, unset, if-set, if-not-set attributes on Page, Button and Delay for conditional manipulation
- teases in the old 0.0.5 format can be opened and saved in the new format
- debug mode (Ctrl+Shift+D) to see more details of the tease you are creating
- more than 5 buttons are supported now (but who needs so many?)
- some layout tweaks


v0.0.5 (by d3vi0n)
- fixed an error for randomizer 
- new script version 5 with new xml-structure
- GUI shows now URL of script
- Use new debugmode for better testing
- Use movies instead of images
- new tease "Meet Chayse Evans" based on movies

v0.0.4 (by d3vi0n)
- minor changes 
- fixed 2 errors in "Tease Club 1"
- new tease "Stroke For Kayden" using metronome

v0.0.3 Hotfix:
- fixed an error in tease "Tease Club 1"

v0.0.3 (by d3vi0n)
- new script version 3 with new tag <metronome> 
- added comments as help in "SamplePage.xml"
- included the AddOn-Scripts of old v0.0.2
- fixed an error in "My Naughty Neighbour 2"
- updated all 4 samples to new script version 3

v0.0.2 AddOn1:
- Adds 2 new sample scripts (My Naughty Neighbour 2 + 3)

v0.0.2 (by d3vi0n)
- minor changes
- updated xml-structure
- new scripttags to randomize targetpages and delaylength
- a second sample tease ("Tease Club")

v0.0.1 (by d3vi0n)
- First release with 1 sample tease ("My Naughty Neighbour 1")