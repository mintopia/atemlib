CONTENTS OF THIS FILE
---------------------

 * Introduction
 * Usage
 * Requirements
 * Supported Images
 * Notes
 * Contact Details
 * Changelog
 * MIT License

INTRODUCTION
------------

Version: 2.0.1
Current Maintainer: Michael Smith <me@murray-mint.co.uk>
This fork maintained by Ian Morrish <ian_morrish@hotmail.com>

MediaUpload.exe allows you to upload images to specific slots in a
BlackMagic ATEM switcher's media pool.

MediaPool.exe lists all the media in the switcher's media pool.

SwitcherLib.dll can be used from PowerShell script to automate ATEM functions
Demo video https://youtu.be/h1GUKFynT9g

MEDIAUPLOAD.EXE USAGE
---------------------

MediaUpload.exe [options] <hostname> <slot> <filename>

Arguments:

 hostname            - The hostname or IP address of the switcher
 slot                - The slot to upload to
 filename            - The filename of the image to upload

Options:

 -h, --help          - Help information
 -d, --debug         - Enable debug output
 -v, --version       - View version information
 -n, --name          - Set the name of the image in the media pool

Example:

To upload myfile.png to Slot 1 on a switcher at 192.168.0.254:

    MediaUpload.exe 192.168.0.254 1 myfile.png

MEDIAPOOL.EXE USAGE
-------------------

MediaPool.exe [options] <hostname>

Arguments:

 hostname        - The hostname or IP of the ATEM switcher

Options:

 -h, --help      - This help message
 -d, --debug     - Debug output
 -v, --version   - Version information
 -f, --format    - The output format. Either xml, csv, json or text

Example:

To see what's in the media pool for a switcher at 192.168.0.254:

    MediaPool.exe 192.168.0.254

To view the output in JSON format:

    MediaPool.exe -f json 192.168.0.254

REQUIREMENTS
------------

 - Microsoft .NET Framework 4.5

   http://www.microsoft.com/en-gb/download/details.aspx?id=30653

 - Blackmagic ATEM Switchers Update 6.2

   https://www.blackmagicdesign.com/uk/support/family/atem-live-production-switchers


SUPPORTED IMAGES
------------------

The Windows GD+ library is used for image manipulation. This currently
supports:

  PNG
  BMP
  JPEG
  GIF
  TIFF

Alpha channels are supported and will be included in the images sent to the
switcher.

Images will need to be the same resolution as the switcher. Running in
debug mode you can see the detected resolution on the switcher.

NOTES
-----

This has been tested with a Blackmagic Design ATEM Production Studio 4K. I do
not have access to any other switchers to test with, but if they use version
6.2 of the SDK, then they should work.

SwitcherLib.dll has been tested with PowerShell on Windows 10 and a Television Studio only

CONTACT DETAILS
---------------

If you're using this for anything interesting, I'd love to hear about it.

 * Web: http://www.murray-mint.co.uk
 * Email: me@murray-mint.co.uk
 * Twitter: @MurrayMintUK
 

CHANGELOG
---------

Version 2.0.0 - 2014-12-24:
 * Rebuilt from decompiled source of original binary
 * Added enumerating of the media pool

Version 1.0.1 - 2014-09-22:
 * Moved switcher functions into a separate library to allow development of
   more tools
 * Slight change to arguments
 * Add support for specifying the name of the image when uploading it

Version 1.0.0 - 2014-09-21:
 * Initial version


MIT LICENSE
-----------

Copyright (C) 2014 by Michael Smith

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
