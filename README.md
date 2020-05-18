# Blendo build machine

## About
Blendo build machine gives teammates a one-click way of creating a new build.

The purpose of this program is:
- Make it frictionless & easy for teammates to acquire the latest build.
- Remove the bottleneck of waiting for someone else to create a new build.

Pre-compiled binaries are available at [my itch.io page](https://blendogames.itch.io/blendobuildmachine).

[![screenshot of Blendo build machine](screenshot.png)](screenshot.png)

## What does it do?
When you click the big **Create build** button:
1. It downloads latest data & code from your project's Subversion source control repository.
2. After the download is done, it uses command-line compiler tools to create a new build.

**Important note:** this program assumes you're using Subversion source control & have Microsoft Visual Studio compiler tools installed. If you want it to use other tools, you'll need to modify my source code.

This is written in C# and a .sln solution for Visual Studio 2010 is provided. Windows only.

## License
This source code is licensed under the zlib license. Read the license details here: [LICENSE.md](https://github.com/blendogames/blendobuildmachine/blob/master/LICENSE.md)

## Credits
by [Brendon Chung](http://blendogames.com)
