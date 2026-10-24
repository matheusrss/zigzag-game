# ZigZag Game

A practice project built in Unity to learn game programming with C#.

This is a study project, not a finished game. I use it to try out new concepts as I learn them. 🦊

## What I'm learning

- **Physics:** moving objects with `Rigidbody` and detecting the ground with `Physics.Raycast`
- **Procedural generation:** spawning platforms at runtime with `Instantiate`, prefabs and `Random.Range`
- **Coroutines:** timing events with `IEnumerator` and `WaitForSeconds`
- **Triggers and collisions:** reacting to objects with `OnTriggerExit` and tags
- **Camera:** following a target smoothly with `Vector3.Lerp` in `LateUpdate`
- **Game state:** sharing data between scripts with `static` variables (game over)
- **Unity workflow:** prefabs, the Inspector, Gizmos and version control with Git and GitHub

## Tech

- Unity 6 (Universal 3D / URP)
- C#

## Running the project

1. Clone this repository.
2. Add the folder in Unity Hub (**Add → Add project from disk**).
3. Open it with the same Unity version (see `ProjectSettings/ProjectVersion.txt`).
4. Open `Assets/Scenes/SampleScene` and press **Play**.

If you get an `InvalidOperationException` about input, go to **Edit → Project Settings → Player → Other Settings** and set **Active Input Handling** to **Both**.

## Author

Made by **Yordy Fox** (aka [matheusrss](https://github.com/matheusrss)) while practicing game development.