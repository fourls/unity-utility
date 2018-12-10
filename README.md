# Common Unity Scripts

## Management
### [StateMachine](Management/StateMachine.cs)
A simple file that contains a StateMachine and State class to build a simple state driven system.

### [TransitionManager](Management/TransitionManager.cs)
A transition manager that fades in and out with a customisable set of functions to execute on start, middle and finish.

### [Cutscene](Management/Cutscene.cs)
A script that triggers `PlayableDirector.Play(<timeline>)` when an object passes within its bounds.

## Interaction
### [Interactable](Interaction/Interactable.cs)
A general class all interactables will inherit from. Contains a `Use(Player player)` function that will be overriden for any specific interaction.

### [Button](Interaction/Button.cs)
A script building off [Interactable](Interaction/Interactable.cs) that triggers any interactable it is linked to when used.

### [MovingWall](Interaction/MovingWall.cs)
An [Interactable](Interaction/Interactable.cs) that uses a CinemachineDollyCart to move a wall when interacted with. Can be used for doors, panels, etc.

### 3D [Holdable](Interaction/Holdable.cs)
Used in conjunction with [ItemHoldablePlayer](PlayerControllers/3D/ItemHoldablePlayerController.cs), this inherits from [Interactable](Interaction/Interactable.cs) to make an object that can be held in front of the player.

## Player Control

### 2D [PlatformerPlayerController](PlayerControllers/2D/PlatformerPlayerController.cs)
A simple and extendable 2D platformer player controller. Calculates jump velocity from a given height and a ground check.

### 3D [FirstPersonPlayerMovement](PlayerControllers/3D/FirstPersonPlayerMovement.cs)
A class that provides a solid 3D first person player movement system. Calculates jump velocity from a given height and a ground check.

### 3D [CameraLook](PlayerControllers/3D/CameraLook.cs)
A script that takes mouse input to create a movable first person perspective.

### 3D [ItemHoldablePlayer](PlayerControllers/3D/ItemHoldablePlayerController.cs)
A class inheriting from [StateMachine](Management/StateMachine.cs) that uses [Holdable](Interaction/Holdable.cs) to carry objects in front of the player (usually in conjunction with [FirstPersonPlayerMovement](PlayerControllers/3D/FirstPersonPlayerMovement.cs)).

## Misc

### [CustomCanvasScaler](Misc/CustomCanvasScaler.cs)
A custom canvas scaler that adapts to the Unity Pixel Perfect Camera.

### [DynamicCircle](Misc/DynamicCircle.cs)
A script to display a circle that can vary in size with a fixed border width.