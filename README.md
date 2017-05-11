# McNaughtonJig
A programmatic means to produce the cutting drawing for a [Gavin Brunton's](https://www.youtube.com/channel/UClXgFhhTjQxrOJQIT3Go61g) [McNaughton Center Saver](http://www.kelton.co.nz/McNaughton%20Center%20Saver.html) jig.

It's a program that spits out a SVG file of the jig.  All of its measurements are in millimeters.

![Sample layout](/Help/Layout.png?raw=true)
[Download the .svg](/Sample.svg?raw=true)

Here's how to use it!  It's fairly primitive right now because you need to be able to some small code updates to customize the jig.

1. Clone the repository and build it.  Visual Studio Community edition works.
2. In the JigGenerator.Console project edit Program.cs to your liking (see below).  You'll probably want to change where it writes the file.
3. In Constants.cs, change the keft width to 0.01mm.  I have it checked in as 0.1 in order to be able to see the lines in the SVG editor.   Ponoko requires all lines to be 0.01mm wide.
3. Run the console app 
4. Open the .svg file an editor like Adbobe Illustrator (not free) or [Inkscape](https://inkscape.org/en/) (free).
5. Find the groups that contain all the `text` elements and convert them to paths (see below for more information)


## Customizations
You can: 
* Change the bolt diameter
* Cutter radii
* Protractor arm length
* Generate different turret post mounts to account for the different spacing

## Limitations
The program is unable to produce text as paths, only as text elements.  Laser cutting services like Ponoko.com need everything as basic shapes or paths.  

Here's how to convert text to paths in Inkscape.  There are two sets of text elements produced, the first is the turret mount label and  the second are the protractor angle labels.  For the protractor:
1. Select the `group` containing the text
2. In the XML explorer observe that each label is a `text` element; those will be changing soon
3. Path > Object to Path.  
![object to paths 1](/Help/ObjectToPaths1.png?raw=true)

Now notice that each letter is its own path:

![object to paths 2(/Help/ObjectToPaths2.png?raw=true)