---
layout: default
title: Input
---

# Input Handling

MenuBuddy supports three input modes -- controller (gamepad), mouse, and touch -- through a unified input system. The input mode is determined by which game base class you extend.

## Game Base Classes and Input

| Game Class | Input Type | `GameType` Value |
|------------|------------|------------------|
| `ControllerGame` | Gamepad/keyboard | `GameType.Controller` |
| `MouseGame` | Mouse/pointer | `GameType.Mouse` |
| `TouchGame` | Touch/tap | `GameType.Touch` |

Each game class initializes the appropriate input handler in its `InitInput()` method.

## Input Architecture

```
DefaultGame
    │
    ├── InputHelper (IInputHelper)
    │   └── Processes raw device input
    │
    └── ScreenManager
        │
        └── IInputHandler
            ├── ControllerInputHandler
            ├── MouseInputHandler
            └── (Touch via TouchScreenBuddy)
```

The input system translates raw device input into semantic events that are dispatched to screens:

| Event Type | Description | Interface |
|------------|-------------|-----------|
| Highlight | Cursor/pointer moved over an item | `IHighlightable` |
| Click | Item was pressed/clicked/tapped | `IClickable` |
| Drag | Item is being dragged | `IDraggable` |
| Drop | Item was dropped | `IDroppable` |

## Controller Input

With `ControllerGame`, menus support gamepad navigation:

| Action | Gamepad | Keyboard |
|--------|---------|----------|
| Move up | D-Pad Up / Left Stick Up | Up Arrow |
| Move down | D-Pad Down / Left Stick Down | Down Arrow |
| Move left | D-Pad Left / Left Stick Left | Left Arrow |
| Move right | D-Pad Right / Left Stick Right | Right Arrow |
| Select | A Button | Enter/Space |
| Cancel/Back | B Button | Escape |

Controller input works with `MenuScreen` and `MenuStackScreen`, which manage a `SelectedIndex` for keyboard/gamepad navigation.

### Controlling Player

Screens can be locked to a specific player:

```csharp
// Accept input from any player
await ScreenManager.AddScreen(new MainMenuScreen(), null);

// Lock to player 1 (index 0)
await ScreenManager.AddScreen(new GameplayScreen(), 0);
```

The `ControllingPlayer` property (on `IScreen`) determines which player can interact with a screen. When `null`, any player's input is accepted.

## Mouse Input

With `MouseGame`, input is pointer-based:

- **Highlight**: Moving the mouse over a widget triggers highlighting
- **Click**: Left mouse button click
- **Drag**: Click and hold while moving
- **Drop**: Release after dragging

Mouse input is dispatched through the `WidgetScreen` methods:

```csharp
// These are called automatically by the input system:
public virtual bool CheckHighlight(HighlightEventArgs highlight);
public virtual bool CheckClick(ClickEventArgs click);
public virtual bool CheckDrag(DragEventArgs drag);
public virtual bool CheckDrop(DropEventArgs drop);
```

## Touch Input

With `TouchGame`, touch gestures map to the same semantic events:

- **Highlight**: Touch/finger position
- **Click**: Tap
- **Drag**: Touch and slide
- **Drop**: Release after drag

## Event Arguments

### ClickEventArgs

```csharp
button.OnClick += (sender, ClickEventArgs e) =>
{
    int player = e.PlayerIndex; // Which player clicked
};
```

### HighlightEventArgs

Contains the pointer/cursor position and input helper reference.

### DragEventArgs

Contains drag position and delta information.

### DropEventArgs

Contains the drop position.

## Input in Custom Screens

### Handling Input in WidgetScreen

`WidgetScreen` automatically routes input events to its layout and child widgets. You can override the check methods to customize behavior:

```csharp
public class MyScreen : WidgetScreen
{
    public MyScreen() : base("My Screen") { }

    public override bool CheckClick(ClickEventArgs click)
    {
        // Custom click handling before default behavior
        if (ShouldHandleCustomClick(click))
        {
            HandleCustomClick(click);
            return true;
        }

        // Fall through to default widget click handling
        return base.CheckClick(click);
    }
}
```

### Handling Input in MenuScreen

`MenuScreen` adds keyboard/controller navigation on top of `WidgetScreen`'s pointer-based input. The `HandleInput()` method processes directional input, select, and cancel:

```csharp
public class MyMenuScreen : MenuScreen
{
    public override void Cancelled(object obj, ClickEventArgs e)
    {
        // Custom cancel behavior (default is ExitScreen)
        ShowConfirmationDialog();
    }
}
```

## Modal Screens

Modal screens block input from reaching screens below them:

```csharp
public class DialogScreen : WidgetScreen
{
    public DialogScreen() : base("Dialog")
    {
        Modal = true; // Block input to screens below
    }
}
```

When `Modal` is `true`, `CheckHighlight()`, `CheckClick()`, `CheckDrag()`, and `CheckDrop()` all return `true` even if no widget was interacted with, consuming the input event.

## Attract Mode

`WidgetScreen` includes an attract mode timer. When no input is received for `AttractModeTime` seconds (default 15), the `TimeSinceInput` countdown timer expires. You can use this to trigger attract mode animations or demos:

```csharp
public class MyScreen : WidgetScreen
{
    public MyScreen() : base("My Screen")
    {
        AttractModeTime = 30f; // 30 seconds until attract mode
    }

    public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool covered)
    {
        base.Update(gameTime, otherScreenHasFocus, covered);

        if (TimeSinceInput.HasTimeRemaining == false)
        {
            // Start attract mode
            StartAttractMode();
        }
    }
}
```

## Back Button

The back button (Android back button, Escape key) is handled by `ScreenManager.OnBackButton()`, which iterates screens from top to bottom until one handles it:

```csharp
public override bool OnBackButton()
{
    // Return true if your screen handles the back action
    ExitScreen();
    return true;
}
```

## Next Steps

- [Screens](screens) -- Screen types and their input handling
- [Widgets](widgets) -- Interactive widgets that respond to input
- [Getting Started](getting-started) -- Choose your input mode
