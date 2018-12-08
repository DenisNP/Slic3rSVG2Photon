**Slic3rSVG2Photon** is a C# console application which can convert SVG file produced by [Slic3r](https://slic3r.org/) to sequence of PNG images required for [PhotonFileEditor](https://github.com/Photonsters/PhotonFileEditor).

# What? #
There is very cool and cheap DLP 3D printer **Anycubic Photon**. Unfortunately, it has its own proprietary `.photon` format. Official **ANYCUBIC Photon Slicer** software can convert classic STL files to `.photon`, but it has very poor functionality. E.g. it has no such useful property like **infill** and many others.

Open source slicing program named [Slic3r](https://slic3r.org/) can produce slices for DLP-projection with many nice infill patterns. However, it can save result only to SVG file with each layer in `<g>` element.

So, **Slic3rSVG2Photon** can take this SVG file and render it to sequence of correct-sized PNG images. These images then can be loaded to another open source application named [PhotonFileEditor](https://github.com/Photonsters/PhotonFileEditor) to create a `.photon` file.

# Configuration #
There is `config` file in root folder with `PARAMETER=VALUE` lines inside. For this moment parameters are:

Parameter|Description|Default for Anycubic Photon
---------|-----------|---------------------------
BED_X|Short side of physical DLP printer display in mm|68.04
BED_Y|Long side of physical DLP printer display in mm|120.96
BED_Z|Maximum print height in mm|150
SCREEN_X|Short side DLP printer display resolution in pixels|1440
SCREEN_Y|Long side DLP printer display resolution in pixels|2560

So the application assumes that input SVG file is **in millimeters**, as Slic3r exports it. And result images will be created with `SCREEN_X * SCREEN_Y` dimensions.

## Command line arguments ##
The application accepts arguments:

`Slic3rSVG2Photon.exe --input PATH_TO_INPUT.svg --output FOLDER_TO_SAVE_RESULT --config PATH_TO_CONFIG`

Argument|Description|Default if not present
--------|-----------|----------------------
--input|Absolute or relative path to input .svg file|%APP_PATH%\print.svg
--output|Absolute or relative path to folder where result images will be saved, **should exist**|%APP_PATH%\out\
--config|Absolute or relative path to configuration file|%APP_PATH%\config

So the running example is:

`Slic3rSVG2Photon.exe --input C:\Documents\figure1.svg --output figure1` 

config will be default
 
# Running #
## General algorithm ##
1. Create or download your 3D model, save it to STL
2. Open **Anycubic Photon Slicer**, set layer height, exposure time and slice it. Save the result to, e.g. `file1.photon`
3. Open **Slic3r**. Go to `Printer Settings > Bed shape > Set` and set to your display physical size, `X: 68.04mm | Y: 120.96mm`
4. Place your STL as you want
5. Go to `Window > DLP Projector` and set `Layer height` and `First layer height` the same as you set in step 2. Layer height must be equal to First layer height
6. Set infill and support properties as you want then press OK
7. On the next window click `Export SVG` and save it to e.g. `print.svg`
8. Run **Slic3rSVG2Photon** with `--input PATH\TO\print.svg` (see information below about running) and get folder with many PNG images
9. Run **PhotonFileEditor** and open `file1.photon`
10. Choose `Edit > Import Bitmaps` and select output folder from step 8
11. Choose `File > Save As` and save it to e.g. file2.photon
12. Now you can load `file2.photon` to printer and get cool and economical print with infill

## Running application ##
### Windows ###
1. Download of compile binary. Lastest binary is in [bin/Release](https://github.com/DenisNP/Slic3rSVG2Photon/tree/master/Slic3rSVG2Photon/bin/Release) folder
2. Open **cmd.exe** or **Windows PowerShell**
3. `cd` to application directory
4. Run `./Slic3rSVG2Photon.exe --input PATH_TO.svg --output OUT_FOLDER`
5. Wait until its done

### Linux/MacOS ###
You can run C# .exe application using [Mono](https://www.mono-project.com/), but I haven't tested it. Please, if you test, send me PR with your addition to this README.

# TODO #
Write `PNG > .photon` conversion to get rid of PhotonFileEditor. You can help me with this if you brave enough to go deep into .photon format deconstruction which present [here](https://github.com/Photonsters/PhotonFileEditor/blob/master/PhotonFile.py)
