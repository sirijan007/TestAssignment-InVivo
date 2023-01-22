# TestAssignment-InVivo

Outline:

This is a solution to the assignment given by InVivo.
Created in Unity Version 2021.3.16f1

Summary:

The app starts with a crossfade transition into the Home Object, which has buttons to either quit and to navigate to contents section. Upon reaching contents section, the 1st section is displayed by default which houses objects which are greyed out, signifying inactive state. On clicking an object will make it appear colourful, a 'stretching' animation will be played and a label will appear with its name. If another object is clicked, the earlier selected one will be set inactive with a subtle animation. Further, on navigating to another content section or back to home will reset everything to its initial state.

Folder Structure:

- Animations
  This contains all the animations and animation controllers used in the project.

- DataSets-Resources-DataFiles
  This contains a .txt file which contains all the data to be loaded in content scenes. Using this file is makes it easy to load different data just by modifying it. This avoids tight coupling of data with the scripts.

- Materials
  This contains 5 unique materials, which has one for the inactive state of the object.

- Prefabs
  This contains all the 4 prefabs representing each of the contents viz. cube, sphere, cylinder & capsule.

- Scenes
  This contains the only scene in the project.

- Scripts
  - LevelManager.cs : This contains the references of transition, home & content objects. Also, this script is responsible to set data in Home & Content objects.
  - ContentDataReader.cs : This is the helper file which reads the data from contentsData.txt and pushes it to LevelManager.cs.
  - TransitionManager.cs : This is a standalone script to play transition effects when and where required.
  - HomeManager.cs : This is the script which is responsible for population data related to home object and navigation to content object.
  - ContentManager.cs : This is the script which is responsible for population data related to content object and navigation to sub sections of various content objects.
  - SingleContent.cs : This is responsible for working of various features of a single object of a particular content section.
