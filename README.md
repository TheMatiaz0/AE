# Horror Game Job Assignment

![AE Gameplay](/AE_Gameplay.gif)

A first-person horror puzzle game with claustrophobic camera and environment-based puzzles. Created as Unity developer recruitment task in April 2025.

The solution here is very abstract, allowing designers to quickly create all kinds of puzzles they desire based on this toolkit.

Unity version: 6000.0.41f1

## Features
- FPS Controller - Inspired with Resident Evil 7, restricts player from having full control over the character.
- Activator/Activable system - Designer-first approach for creating puzzles. **No need to touch the code to create new puzzles**.
- Jumpscare examples - There are multiple chain reactions with UniTask and DOTween that are based on the Activator/Activable system.
- Inventory system - Item can be picked up from the ground, placed into socket or drop to ground.
- Raycast detection - Detects item that can be picked up or puzzle element that can be interacted (ex. candle stand that you can fix).
- URP Decals - used Unity URP's Decals system to setup blood puddles that affect environment.

## Assets used
- [DOTween](https://github.com/Demigiant/dotween)
- [UniTask](https://github.com/Cysharp/UniTask)
- [SerializeReferenceExtensions](https://github.com/mackysoft/Unity-SerializeReferenceExtensions)

## Controls
All controls are created with [New Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.14/manual/index.html) from Unity.
| Action | Key |
| ------------- | ------------- |
| Movement  | W/S/A/D or Arrow keys  |
| Look | Mouse |
| Interact | Left Mouse Button or E |
| Drop Item | Right Mouse Button or Q |

## Gameplay Walkthrough
- Stab laying knight with sword.
- Fix candle stand (to make it standing not laying).
- Find candle (hint: it's upstairs).
- Put candle inside candle stand.
- Put 8 skulls to the treasure chest.

The door behind you will open, allowing you to start the game again.

## Known Issues
- Sometimes items can glitch while throwing and picking next to each other (causing them to be unresponsive).
- Hands can get stuck visually when raycast element in front of player gets disabled.
