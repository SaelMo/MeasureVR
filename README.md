# MeasureVR
A utility for taking measurements of 3D objects in virtual reality

## Import
In the data folder of this program there is a folder called MeasureVRImport. Put your obj file in there.

## Export
In the data folder of this program there is a folder called MeasureVRExport. You can find your measurements there.

## Controls
Select - Right Trigger
Grab - Left/Right Grip
Translate up/down - Right Grip + Right Secondary/Primary Button
Scale up/down - Left Grip + Right Secondary/Primary Button
Move - Left Thumbstick (When flight is toggled, movement is relative to left controller orientation)
Teleport - Left Trigger
Rotate - Right Thumbstick
Generate measure points - Right Primary button (When MultiMeasure tool is toggled on)

## UI
Fixed to the left controller is the UI menu, buttons may be selected to enact desired effect.

## Grabbing
Measure points may be grabbed by either hovering over point with controller, 
then pressing the grip button or by aiming the right ray at the center of a measure point and pressing the grip button.

## Movement
There are four main modes of transportation.
1 - Physically moving the headset in the real world
2 - Walking, by moving the left thumbstick with fly toggled off
3 - Flying, by moving the left thumbstick with fly toggled on
4 - Teleportation, by pressing the left trigger and aiming at desired location

## Tools
### Single-Distance Tool
Two points will appear once the tool is toggled on.
The points may be grabbed and moved. With the "Reset Points" button the two measure points will snap to the two controllers.
The distance between the points will be displayed on the UI menu.

### Multi-Distance Tool
Measure points will appear by pressing the right primary button when tool is toggled on.
Points may be grabbed and moved. With the "Reset Points" button the points will be reset.
The distance is measured along the lines connecting the points.
By pressing the "Close Loop" button a line will appear between the first and last measure points, this line is included in distance calculation

### TriAngle Tool
Three points will appear once the tool is toggled on.
The points may be grabbed and moved. With the "Reset Angles" button two measure points will snap to the two controllers with the third between them.
The angles are displayed according to the color of the measure points "R" represents the red point, "B" the blue and "G" the green.

## Custon Model
An OBJ file may be stored in the import folder, the first file in this folder will be loaded in the program upon selecting the "Import" button.
By pressing the "Delete" button the imported model will be deleted

## Logging data
Some data will be actively displayed while using a tool, by pressing the "Log" button the displayed data will appear in the Log window.
When the "Export" button is selected a file will be generated containing the data from the log window with timestampt and further information.
The exported file will appear in the export folder as a .txt

## Snapping
WIP (Expect some bugs)
By toggling the snap feature whenever a measure point is grabbed and the controller is aimed at the model, a yellow point will appear on the mesh, 
if the point is released it will appear where the yellow point was.

##################################################################################################################################################

HOW TO READ EXPORT:
Single dist: distance between points, x1, y1, z1, x2, y2, z2
Multi dist: distance of line, Area (defaults to 0), number of points, x1, y1, z1, x2, y2, z2, ... , xn, yn, zn
TriAngle: red angle, green angle, blue angle, xr, yr, zr, xg, yg, zg, xb, yb, zb
