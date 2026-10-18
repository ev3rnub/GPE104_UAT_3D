# GPE104 Unity Project




---

## Project Overview

This is the an ongoing project for GPE104. The goal of this project is to demonstrate understanding of:

1. Demonstrate simple gameplay programming concepts through creative projects and presentations.

2. Write clear and efficient code for use in an industry-standard game engine, utilizing programming languages relevant to professional game development, and following appropriate coding standards and professional practices.

3. Effectively communicate code functionality and usage to technical and non-technical team members through the use of written, visual, oral, and video materials and presentations.

4. Apply current game development techniques such as collision detection, object motion, and the creation and manipulation of game objects and game data through code, using an industry standard game engine.

5. Apply object-oriented programming concepts (including abstraction, inheritance, polymorphism, and encapsulation) and common programming patterns (such as the singleton pattern and the object/component pattern) to build game projects.

6. Utilize industry standard source control software for tracking and managing changes to code, and for distributing source code and other assets as part of a development team.

## Objective
The objective of this assignment is to demonstrate your ability to perform the following tasks:

Creating a 3D project and importing 3D models, textures, and other assets.
Use Unity's physics system to push our ship in the world and apply gravity. 
Add functionality to allow us to yaw, pitch and do a barrel roll.
Move the camera, using code, so that it smoothly follows the player while looking at a point a designer-defined distance in front of the player, giving the player control over the camera's offset from the player.
Move and adapt any applicable code created in the previous project (health, shooting, death, menus, settings) to this new project.

## Unity Issues/bugs:
- Sometimes the Master volume slider can't be adjusted/moved. Not sure why. 

## Project Features Timeline
- 10/14/2026:08:54PM - Ported over alot of the scripts from the previous 2d project. Modified existing scripts/code structure to use 3d model/assets in place of sprite. The player spaceship can shoot, use forces to move, can die and destroy astroids. I added a control as an extra to stop all angular motion via the X key. 

- 10/17/2026:01:29AM - Added a UFO, EnemyAI, partially implemented GUI, OptionsGUI, MainMenu, win/lose. Setup Audio stuffs. MainMenu has sound. Added asteroid sfx to astroids. 

- 10/18/2026:12:06PM - Got the GUI mostly implemented. Started on implementing astornaut pick up functionality. 

- 10/18/2026:14:18PM - Completed basic GUI implementation, player height restriction, astronaut pickup, endgame/wingame. Ran into an issue with a collider that hung me up for 5 hours. :(.