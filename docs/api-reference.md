---
layout: default
title: API Reference
---

# API Reference

Complete reference for MenuBuddy's public interfaces, classes, and enums.

## Interfaces

### IScreen

The base contract for all screens.

```csharp
public interface IScreen
{
    // Properties
    ScreenManager ScreenManager { get; set; }
    ContentManager Content { get; }
    string ScreenName { get; set; }
    GameClock Time { get; }
    IScreenTransition Transition { get; }
    TransitionState TransitionState { get; }
    bool CoverOtherScreens { get; set; }
    bool CoveredByOtherScreens { get; set; }
    int? ControllingPlayer { get; set; }
    bool IsActive { get; }
    bool HasFocus { get; }
    bool IsExiting { get; }
    int Layer { get; set; }
    int SubLayer { get; set; }

    // Methods
    Task LoadContent();
    void UnloadContent();
    void Update(GameTime gameTime, bool otherWindowHasFocus, bool covered);
    bool UpdateTransition(IScreenTransition screenTransition, GameClock gameTime);
    void Draw(GameTime gameTime);
    void ExitScreen();
    bool OnBackButton();
}
```

### IScreenManager

Screen management operations.

```csharp
public interface IScreenManager : IDrawable, IGameComponent, IUpdateable
{
    // Properties
    DefaultGame DefaultGame { get; }
    IInputHandler Input { get; }
    SpriteBatch SpriteBatch { get; }
    Color ClearColor { set; }
    DrawHelper DrawHelper { get; }
    ScreenStackDelegate MainMenuStack { get; }

    // SpriteBatch helpers
    void SpriteBatchBegin(SpriteSortMode sortMode = SpriteSortMode.Deferred);
    void SpriteBatchBegin(BlendState blendState, SpriteSortMode sortMode = SpriteSortMode.Deferred);
    void SpriteBatchEnd();

    // Screen management
    Task AddScreen(IScreen screen, int? controllingPlayer = null);
    Task AddScreen(IScreen[] screens, int? controllingPlayer = null);
    Task SetTopScreen(IScreen screen, int? controllingPlayer);
    void RemoveScreen(IScreen screen);
    void RemoveScreens<T>() where T : IScreen;
    Task ErrorScreen(Exception ex);
    IScreen FindScreen(string screenName);
    List<T> FindScreens<T>() where T : IScreen;
    void BringToTop<T>() where T : IScreen;
    void ClearScreens();
    bool OnBackButton();
}
```

### IWidget

Base interface for displayable, interactive UI elements.

```csharp
public interface IWidget : IScreenItem, IScalable, IHighlightable, ITransitionable, IBackgroundable
{
    bool IsHidden { get; set; }
    bool IsTappable { get; }
    bool WasTapped { get; }
    bool IsTapHeld { get; }
}
```

### ILayout

Container for arranging screen items.

```csharp
public interface ILayout : IScreenItemContainer, IScreenItem, IScalable,
    IClickable, IHighlightable, IDraggable, IDroppable, ITransitionable
{
    List<IScreenItem> Items { get; }
    bool RemoveItems<T>() where T : IScreenItem;
    void Clear();
}
```

### IScreenTransition

Controls screen transition animation.

```csharp
public interface IScreenTransition
{
    float OnTime { get; set; }
    float OffTime { get; set; }
    float TransitionPosition { get; }
    float Alpha { get; }
    TransitionState State { get; set; }

    event EventHandler OnStateChange;

    Color AlphaColor(Color color);
    void Restart(bool transitionOn);
    bool Update(GameClock gameTime, bool transitionOn);
}
```

### Supporting Interfaces

| Interface | Description |
|-----------|-------------|
| `IScreenItem` | Base for anything that can appear on screen (position, rect, name, load/draw) |
| `IScreenItemContainer` | Can contain `IScreenItem` children (AddItem, RemoveItem) |
| `IScalable` | Supports scaling |
| `IHighlightable` | Supports highlight state and checking |
| `IClickable` | Supports click detection |
| `IDraggable` | Supports drag gestures |
| `IDroppable` | Supports drop targets |
| `ITransitionable` | Supports transition animation |
| `IBackgroundable` | Supports background rendering |
| `IHasContent` | Has a `LoadContent(IScreen)` method |
| `ILeftRightItem` | Supports left/right navigation actions |
| `IButton` | Button behavior (OnClick event, Clicked method) |
| `ICheckbox` | Checkbox behavior (IsChecked) |
| `IMenuScreen` | Menu screen operations (AddMenuItem, HandleInput) |
| `IWidgetScreen` | Widget screen operations (AddItem, CheckClick, etc.) |
| `IInputHandler` | Input processing |

## Classes

### Game Classes

| Class | Extends | Description |
|-------|---------|-------------|
| `DefaultGame` | `Game` | Abstract base for MenuBuddy games |
| `ControllerGame` | `DefaultGame` | Gamepad/keyboard input |
| `MouseGame` | `DefaultGame` | Mouse/pointer input |
| `TouchGame` | `DefaultGame` | Touch input |

**DefaultGame Key Members:**

| Member | Type | Description |
|--------|------|-------------|
| `Graphics` | `GraphicsDeviceManager` | Graphics device manager |
| `ResolutionComponent` | `ResolutionComponent` | Resolution scaling |
| `GameType` | `GameType` | Input type |
| `VirtualResolution` | `Point` | Logical resolution |
| `ScreenResolution` | `Point` | Display resolution |
| `Fullscreen` | `bool` | Fullscreen mode |
| `Letterbox` | `bool` | Letterboxing |
| `ContentRootDirectory` | `string` | Content root path |
| `GetMainMenuScreenStack()` | `IScreen[]` | Abstract: initial screens |
| `InitStyles()` | `void` | Virtual: configure StyleSheet |
| `InitInput()` | `void` | Abstract: initialize input |

### Screen Classes

| Class | Extends | Description |
|-------|---------|-------------|
| `Screen` | -- | Abstract base screen |
| `WidgetScreen` | `Screen` | Screen with widget container |
| `MenuScreen` | `WidgetScreen` | Screen with keyboard/controller navigation |
| `MenuStackScreen` | `MenuScreen` | Abstract menu with title and stack layout |
| `MessageBoxScreen` | -- | Modal OK/Cancel dialog |
| `OkScreen` | -- | Modal OK-only dialog |
| `LoadingScreen` | -- | Loading progress screen |
| `ErrorScreen` | -- | Exception display screen |
| `TextEditScreen` | -- | Text input dialog |
| `DropdownScreen<T>` | -- | Dropdown selection dialog |
| `NumPadScreen` | -- | Numeric input keypad |

### Widget Classes

| Class | Description |
|-------|-------------|
| `Widget` | Abstract base widget |
| `Label` | Text display |
| `MenuTitle` | Large font title label |
| `Image` | Sprite/texture display |
| `BackgroundImage` | Tiled background |
| `BouncyImage` | Animated image |
| `BaseButton` | Abstract button base |
| `StackLayoutButton` | Button with stack layout content |
| `RelativeLayoutButton` | Button with relative layout content |
| `DragDropButton` | Drag-and-drop button |
| `CancelButton` | Standard back/cancel button |
| `MenuEntry` | Simple menu entry |
| `MenuEntryBool` | Boolean toggle menu entry |
| `MenuEntryInt` | Integer selection menu entry |
| `ContinueMenuEntry` | "Press to continue" entry |
| `BaseSlider<T>` | Generic slider base |
| `Slider` | Float slider |
| `IntSlider` | Integer slider |
| `BaseTextEdit` | Text input base |
| `TextEdit` | Text input widget |
| `TextEditWithDialog` | Text input with dialog |
| `NumEdit` | Numeric input widget |
| `Dropdown<T>` | Generic dropdown |
| `DropdownItem<T>` | Dropdown option |
| `Checkbox` | Boolean toggle checkbox |
| `Shim` | Invisible spacer |
| `Background` | Background renderer |
| `ContextMenu` | Context menu |
| `Hamburger` | Hamburger menu button |
| `TreeItem` | Hierarchical tree node |

### Layout Classes

| Class | Description |
|-------|-------------|
| `Layout` | Abstract base layout |
| `AbsoluteLayout` | Manual positioning |
| `StackLayout` | Vertical/horizontal stacking |
| `RelativeLayout` | Relative positioning |
| `ScrollLayout` | Scrollable container |
| `PaddedLayout` | Padded container |
| `Tree` | Hierarchical tree layout |

### Transition Classes

| Class | Description |
|-------|-------------|
| `ScreenTransition` | Main screen transition implementation |
| `BaseTransitionObject` | Abstract transition object |
| `PointTransitionObject` | Animates Point values |
| `PathTransitionObject` | Animates along paths |
| `DirectionTransitionObject` | Direction-based transition |
| `WipeTransitionObject` | Wipe transition effects |

### Input Classes

| Class | Description |
|-------|-------------|
| `BaseInputHandler` | Base input handler |
| `ControllerInputHandler` | Gamepad input |
| `MouseInputHandler` | Mouse input |
| `ControllerInputHelper` | Controller utilities |
| `MouseScreenInputChecker` | Mouse screen detection |
| `TouchInputHelper` | Touch input utilities |

### Other Classes

| Class | Description |
|-------|-------------|
| `ScreenManager` | DrawableGameComponent that manages the screen stack |
| `ScreenStack` | Internal screen collection |
| `StyleSheet` | Static global styling properties |
| `DrawHelper` | Rendering utilities |

## Enums

### GameType

```csharp
public enum GameType
{
    Controller,
    Mouse,
    Touch
}
```

### TransitionState

```csharp
public enum TransitionState
{
    Hidden,
    TransitionOn,
    Active,
    TransitionOff
}
```

### TransitionWipeType

```csharp
public enum TransitionWipeType
{
    PopLeft,      // In left, out left
    PopRight,     // In right, out right
    PopTop,       // In top, out top
    PopBottom,    // In bottom, out bottom
    SlideLeft,    // In left, out right
    SlideRight,   // In right, out left
    SlideTop,     // In top, out bottom
    SlideBottom,  // In bottom, out top
    None          // No transition
}
```

### FontSize

```csharp
public enum FontSize
{
    Small,
    Medium,
    Large
}
```

### HorizontalAlignment

```csharp
public enum HorizontalAlignment
{
    Left,
    Center,
    Right
}
```

### VerticalAlignment

```csharp
public enum VerticalAlignment
{
    Top,
    Center,
    Bottom
}
```

### StackAlignment

```csharp
public enum StackAlignment
{
    Top,
    Bottom,
    Left,
    Right
}
```

## Delegates

### ScreenStackDelegate

```csharp
// Delegate for providing the main menu screen stack
public delegate IScreen[] ScreenStackDelegate();
```

## Event Args

| Class | Properties | Used By |
|-------|------------|---------|
| `ClickEventArgs` | `PlayerIndex` | Button click events |
| `HighlightEventArgs` | Position, InputHelper | Highlight checking |
| `DragEventArgs` | Drag position/delta | Drag operations |
| `DropEventArgs` | Drop position | Drop operations |
| `SelectionChangeEventArgs<T>` | `SelectedItem` | Dropdown selection |

## NuGet Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| MonoGame.Framework.DesktopGL | 3.8.* | Game framework |
| FontBuddy | 5.* | Font rendering |
| InputHelper | 5.* | Input state management |
| StateMachineBuddy | 5.* | State machine |
| GameTimer | 5.* | Timing utilities |
| HadoukInput | 5.* | Controller input |
| MatrixExtensions | 5.* | Matrix utilities |
| MouseBuddy | 5.* | Mouse input |
| PrimitiveBuddy | 5.* | Primitive rendering |
| RandomExtensions.dmanning23 | 5.* | Random utilities |
| ResolutionBuddy | 5.* | Resolution management |
| TouchScreenBuddy | 5.* | Touch input |
| Vector2Extensions | 5.* | Vector utilities |
| XmlBuddy | 5.* | XML utilities |
| FilenameBuddy | 5.* | Filename utilities |
