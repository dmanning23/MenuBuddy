---
layout: default
title: Screens
---

# Screens

Screens are the fundamental building blocks of MenuBuddy. Each screen represents a distinct game state -- main menu, options, gameplay, pause menu, dialogs, and so on. Screens are managed as a stack, with transitions animating them on and off.

## Screen Types

### Screen (Abstract Base)

The base class for all screens. Provides lifecycle management, transitions, and state tracking.

```csharp
public abstract class Screen : IScreen, IDisposable
```

**Key Properties:**

| Property | Type | Description |
|----------|------|-------------|
| `ScreenName` | `string` | Display name of the screen |
| `ScreenManager` | `ScreenManager` | The manager this screen belongs to |
| `Content` | `ContentManager` | Content manager for loading assets |
| `Time` | `GameClock` | Game clock for timing and animations |
| `Transition` | `IScreenTransition` | Transition controller |
| `TransitionState` | `TransitionState` | Current transition state |
| `CoverOtherScreens` | `bool` | Whether screens below should transition off |
| `CoveredByOtherScreens` | `bool` | Whether this screen transitions off when covered |
| `IsActive` | `bool` | Whether the screen can receive input |
| `HasFocus` | `bool` | Whether the screen currently has focus |
| `IsExiting` | `bool` | Whether the screen is exiting |
| `ControllingPlayer` | `int?` | Player index that controls this screen, or null for any |
| `Layer` | `int` | Draw order layer (higher = on top) |

**Lifecycle Methods:**

| Method | Description |
|--------|-------------|
| `LoadContent()` | Load graphics assets (async) |
| `UnloadContent()` | Release graphics assets |
| `Update(GameTime, bool, bool)` | Run per-frame logic |
| `Draw(GameTime)` | Render the screen |
| `ExitScreen()` | Begin exit transition |
| `OnBackButton()` | Handle back/escape button |

### WidgetScreen

Extends `Screen` with an `AbsoluteLayout` container for widgets. Adds click, highlight, drag, and drop handling.

```csharp
public class WidgetScreen : Screen, IWidgetScreen
```

**Additional Properties:**

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Highlightable` | `bool` | `true` | Whether highlight checking is enabled |
| `Clickable` | `bool` | `true` | Whether click checking is enabled |
| `Modal` | `bool` | `false` | Whether this screen blocks input to screens below |
| `AttractModeTime` | `float` | `15.0` | Seconds before attract mode triggers |
| `TimeSinceInput` | `CountdownTimer` | -- | Countdown to attract mode |

**Key Methods:**

```csharp
// Add a widget to the screen
public virtual void AddItem(IScreenItem item);

// Remove a widget from the screen
public bool RemoveItem(IScreenItem item);

// Add a standard cancel button (configurable via StyleSheet)
protected CancelButton AddCancelButton(int? customSize = null);
```

### MenuScreen

Extends `WidgetScreen` with keyboard/controller navigation. Menu items can be navigated with up/down, selected with a button press, and support left/right actions.

```csharp
public class MenuScreen : WidgetScreen, IMenuScreen
```

**Additional Properties:**

| Property | Type | Description |
|----------|------|-------------|
| `SelectedIndex` | `int` | Index of the currently selected menu item |
| `SelectedItem` | `IScreenItem` | The currently selected menu item (controller mode) |

**Key Methods:**

```csharp
// Add a navigable menu item with optional tab order
public void AddMenuItem(IScreenItem menuItem, int tabOrder = 0);

// Remove a menu item
public void RemoveMenuItem(IScreenItem entry);
public virtual void RemoveMenuItem(int index);

// Set the selected item programmatically
public void SetSelectedIndex(int index);
public void SetSelectedItem(IScreenItem item);

// Called when cancel/back is pressed (default: exits screen)
public virtual void Cancelled(object obj, ClickEventArgs e);
```

### MenuStackScreen (Abstract)

Extends `MenuScreen` with a title label and a `StackLayout` for vertically stacked menu entries. This is the most common base class for menus.

```csharp
public abstract class MenuStackScreen : MenuScreen
```

**Additional Properties:**

| Property | Type | Description |
|----------|------|-------------|
| `MenuTitle` | `Label` | The title label at the top of the screen |
| `MenuEntries` | `StackLayout` | The stack layout containing menu entries |
| `MenuTitlePosition` | `Point` | Position of the title |
| `MenuEntryPosition` | `Point` | Position of the entry stack |

**Key Methods:**

```csharp
// Add a menu entry to the stack
protected void AddMenuEntry(IScreenItem menuEntry);

// Add a "Press to continue" button that exits the screen
protected ILeftRightItem AddContinueButton();
```

**Example:**

```csharp
public class OptionsScreen : MenuStackScreen
{
    public OptionsScreen() : base("Options") { }

    public override async Task LoadContent()
    {
        await base.LoadContent();

        var volumeEntry = new MenuEntryInt("Volume", 0, 100, Content);
        AddMenuEntry(volumeEntry);

        var fullscreenEntry = new MenuEntryBool("Fullscreen", Content);
        AddMenuEntry(fullscreenEntry);

        AddCancelButton();
    }
}
```

## Specialized Screens

### MessageBoxScreen

A modal confirmation dialog with OK and Cancel buttons.

```csharp
var confirm = new MessageBoxScreen("Are you sure you want to quit?");
confirm.OnSelect += (sender, e) =>
{
    // User clicked OK
    ScreenManager.Game.Exit();
};
confirm.OnCancel += (sender, e) =>
{
    // User clicked Cancel -- dialog dismissed automatically
};
await ScreenManager.AddScreen(confirm, controllingPlayer);
```

### OkScreen

A simple alert dialog with only an OK button.

```csharp
var alert = new OkScreen("Save complete!", Content);
await ScreenManager.AddScreen(alert, controllingPlayer);
```

### LoadingScreen

Displays a loading indicator while other screens load their content asynchronously.

```csharp
// Load screens with a loading indicator
LoadingScreen.Load(ScreenManager, new IScreen[] { new GameplayScreen() });

// With a custom message
LoadingScreen.Load(ScreenManager, null, "Loading level...", new GameplayScreen());
```

### ErrorScreen

Displays exception information in a recoverable way.

```csharp
try
{
    // risky operation
}
catch (Exception ex)
{
    await ScreenManager.ErrorScreen(ex);
}
```

## Screen Lifecycle

### State Flow

```
              LoadContent()
                  │
                  ▼
    ┌──── TransitionOn ◄────┐
    │           │            │
    │           ▼            │
    │        Active          │
    │           │            │
    │     ExitScreen() or    │
    │     covered by another │
    │           │            │
    │           ▼            │
    │     TransitionOff      │
    │           │            │
    │           ▼            │
    │        Hidden ─────────┘  (if covered, not exiting)
    │           │
    │           ▼
    │    UnloadContent()        (if exiting)
    │           │
    └───────────┘
```

### TransitionState Enum

| State | Description |
|-------|-------------|
| `Hidden` | Not visible, fully transitioned off |
| `TransitionOn` | Animating into view |
| `Active` | Fully visible and interactive |
| `TransitionOff` | Animating out of view |

### IsActive vs HasFocus

- `IsActive` is `true` when the screen can respond to input. A screen is active if the window has focus, it's not covered (or doesn't care about being covered), and its state is `TransitionOn` or `Active`.
- `HasFocus` is `true` when the screen is the topmost interactive screen. Like `IsActive`, but always `false` when covered by another screen.

## Screen Manager Operations

### Adding Screens

```csharp
// Add a single screen
await ScreenManager.AddScreen(new OptionsScreen(), controllingPlayer);

// Add multiple screens at once
await ScreenManager.AddScreen(
    new IScreen[] { new BackgroundScreen(), new MainMenuScreen() },
    controllingPlayer
);

// Set the top screen (replaces current top)
await ScreenManager.SetTopScreen(new PauseScreen(), controllingPlayer);
```

### Removing Screens

```csharp
// Graceful exit with transition
screen.ExitScreen();

// Immediate removal (no transition)
ScreenManager.RemoveScreen(screen);

// Remove all screens of a type
ScreenManager.RemoveScreens<OptionsScreen>();

// Clear all screens
ScreenManager.ClearScreens();
```

### Finding Screens

```csharp
// Find by name
IScreen screen = ScreenManager.FindScreen("Options");

// Find all of a type
List<MenuScreen> menus = ScreenManager.FindScreens<MenuScreen>();

// Bring a screen type to the top
ScreenManager.BringToTop<PauseScreen>();
```

### Back Button Handling

The back button (Android back, keyboard Escape) is routed through `OnBackButton()`. The `ScreenManager` calls this on screens from top to bottom until one returns `true`.

```csharp
public override bool OnBackButton()
{
    // Handle back navigation
    ExitScreen();
    return true; // We handled it
}
```

## Screen Interaction Properties

### CoverOtherScreens

When `true`, screens below this one in the stack will receive `covered = true` in their `Update()` call. This triggers their transition-off animation.

### CoveredByOtherScreens

When `true`, this screen will transition off when a screen above it sets `CoverOtherScreens = true`. When `false`, this screen stays visible even when covered (useful for background screens).

### Modal

When `true`, this screen consumes all click and highlight events, preventing them from reaching screens below.

### Layer

Controls draw order. Higher layer values are drawn on top. Default is `int.MaxValue`.

```csharp
public class BackgroundScreen : WidgetScreen
{
    public BackgroundScreen() : base("Background")
    {
        CoverOtherScreens = false;
        CoveredByOtherScreens = false;
        Layer = 0; // Drawn behind everything
    }
}
```

## Next Steps

- [Widgets](widgets) -- Add interactive elements to screens
- [Layouts](layouts) -- Control widget positioning
- [Transitions](transitions) -- Customize transition effects
