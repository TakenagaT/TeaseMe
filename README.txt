
TeaseMe v0.0.7 (January 2011)

TeaseMe is a small non commercial private "fun program" for adults 
by Takenaga and d3vi0n

Original thread:
http://www.milovana.com/forum/viewtopic.php?f=2&t=6993


Requirements
-------------------------
You'll need Microsoft .NET Framework to run the program. 
Download it here: http://www.microsoft.com


Description
-------------------------
A tease is basicly a slideshow with instructions and text.

TeaseMe gives you the possibility to create and play offline teases 
similar to the teases at http://www.milovana.com. 
All you need are images or movies and a script (as xml-file)
Teases are added into the "/teases/"-folder

see the samples for help on creating own teases


You can create your own teases with xml-Scripts and (new:) import flashteases from milovana.com.

Features
---------------
- play offline and enjoy short loadtimes
- sizable window with large imageframe
- create your own offline teases
- sort your tease into pages and add instructions
- (new:) support for html in instructiontext (including hyperlinks)
- use pictures, video with high resolution and audio as media
- (new:) support for online images, video, audio (use full url incl. http:// as id)
- combine a picture and a audio file on the same page
- use custom buttons and delays (hidden, secret, visible) for navigation
- add custom metronome to pages
- randomize navigation target, delay length and metronome bpm
- conditional manipulation with set/unset and if-set/if-not-set attributes
- (new:) support for set/unset multiple flags
- debug mode (Ctrl+Shift+D) for better testing
- (new:) import milovana flashteases
- (new:) play a detailed tutorial (sample.xml) and learn how it works

For more online teases visit http://www.milovana.com/


Update History
(January 8, 2012)
--------------------

(new:) v0.0.7 (by Takenaga)
- import milovana flashteases
- support for html in instructiontext (including hyperlinks)
- new detailed tutorial (sample.xml) for new players and creators replaces old samples
- support for set/unset multiple flags
- support for online images/video/audio (use full url incl. http:// as id)
- layout changed to support bigger images
- download splitted into basic version (with tutorial) and optional teasepack
- removed the about box again (the button just used space) 


-------------------------
v0.0.6 (by Takenaga)
- converted to C# 
- new XML-fomat, much easier to type and read.
- set, unset, if-set, if-not-set attributes on Page, Button and Delay for conditional manipulation.
- teases in the old 0.0.5 format can be opened and saved in the new format.
- debug mode (Ctrl+Shift+D) to see more details of the tease you are creating.
- more than 5 buttons are supported now (but who needs so many?)
- add audio to images.
- some layout tweaks.


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