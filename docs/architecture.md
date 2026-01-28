---
layout: default
title: Architecture
---

# Architecture

MenuBuddy uses a layered architecture inspired by the classic XNA NetworkStateManagement sample. This page describes the major systems and how they relate to each other.

## High-Level Overview

```
┌─────────────────────────────────────────────┐
│                DefaultGame                   │
│  (ControllerGame / MouseGame / TouchGame)    │
├─────────────────────────────────────────────┤
│              ScreenManager                   │
│         (manages screen stack)               │
├─────────────────────────────────────────────┤
│    Screen  →  WidgetScreen  →  MenuScreen    │
│                                              │
│    ┌──────────────────────────────────────┐  │
│    │             Layouts                  │  │
│    │  (Absolute, Stack, Relative, ...)    │  │
│    │                                      │  │
│    │  ┌────────────────────────────────┐  │  │
│    │  │          Widgets               │  │  │
│    │  │  (Label, Button, Slider, ...)  │  │  │
│    │  └────────────────────────────────┘  │  │
│    └──────────────────────────────────────┘  │
├─────────────────────────────────────────────┤
│   StyleSheet    │  Transitions  │   Input    │
└─────────────────────────────────────────────┘
```

## Core Systems

### 1. Game Layer

`DefaultGame` is the abstract base class for all MenuBuddy games. It sets up the graphics device, resolution management, input handling, and screen manager. Three concrete subclasses are provided:

| Class | Input Type | Use Case |
|-------|------------|----------|
| `ControllerGame` | Gamepad | Console or desktop with controller |
| `MouseGame` | Mouse/pointer | Desktop applications |
| `TouchGame` | Touch | Mobile devices (iOS, Android) |

The game class owns a single `ScreenManager` instance and delegates rendering and input to it.

### 2. Screen Manager

`ScreenManager` is a `DrawableGameComponent` that maintains the screen stack. It:

- Manages the lifecycle of all screens (add, remove, clear)
- Routes update and draw calls to active screens
- Handles input delegation to the topmost screen
- Provides shared services (`SpriteBatch`, `DrawHelper`, `Input`)

Screens are stored in a `ScreenStack`, sorted by `Layer` and `SubLayer` values. Higher layers are drawn on top of lower layers.

### 3. Screen Hierarchy

Screens form an inheritance chain, each level adding capabilities:

```
IScreen
  └─ Screen (abstract)
       └─ WidgetScreen
            └─ MenuScreen
                 └─ MenuStackScreen (abstract)
```

| Level | Adds |
|-------|------|
| `Screen` | Lifecycle, transitions, state management |
| `WidgetScreen` | AbsoluteLayout container, click/highlight/drag handling, modal support |
| `MenuScreen` | Keyboard/controller navigation (up/down/left/right/select/cancel), tab ordering |
| `MenuStackScreen` | Title label, StackLayout for menu entries, `AddMenuEntry()` convenience method |

Specialized screens built on these bases:

| Screen | Purpose |
|--------|---------|
| `MessageBoxScreen` | Modal dialog with OK/Cancel buttons |
| `OkScreen` | Simple alert dialog with OK button |
| `LoadingScreen` | Progress indicator while loading content |
| `ErrorScreen` | Displays exception details |
| `TextEditScreen` | Modal text input dialog |
| `DropdownScreen<T>` | Modal dropdown selection list |
| `NumPadScreen` | Numeric input keypad |

### 4. Widget System

Widgets are interactive UI elements that live inside layouts. The base `IWidget` interface extends several capability interfaces:

```
IWidget
  ├─ IScreenItem      (position, rect, name, load/draw)
  ├─ IScalable        (scale factor)
  ├─ IHighlightable   (highlight state, check highlight)
  ├─ ITransitionable  (transition position)
  └─ IBackgroundable  (background drawing)
```

Widgets include: `Label`, `Image`, `StackLayoutButton`, `RelativeLayoutButton`, `Slider`, `IntSlider`, `Dropdown<T>`, `Checkbox`, `TextEdit`, `NumEdit`, `Shim`, and more.

### 5. Layout System

Layouts are containers that control widget positioning. All layouts implement `ILayout`:

```
ILayout
  ├─ IScreenItemContainer  (AddItem, RemoveItem)
  ├─ IScreenItem           (position, rect)
  ├─ IScalable             (scale)
  ├─ IClickable            (click handling)
  ├─ IHighlightable        (highlight propagation)
  ├─ IDraggable            (drag handling)
  ├─ IDroppable            (drop handling)
  └─ ITransitionable       (transition)
```

| Layout | Positioning Strategy |
|--------|---------------------|
| `AbsoluteLayout` | Items placed at explicit positions |
| `StackLayout` | Items stacked vertically or horizontally |
| `RelativeLayout` | Items positioned relative to container |
| `ScrollLayout` | Scrollable content area |
| `PaddedLayout` | Adds padding around content |
| `Tree` | Hierarchical tree structure |

Layouts can be nested -- a `StackLayout` can contain a `RelativeLayout` which contains widgets.

### 6. StyleSheet

`StyleSheet` is a static class that provides global theming. All widgets read their default colors, fonts, sounds, and images from `StyleSheet` properties. Override `InitStyles()` in your game class to customize.

### 7. Transition System

Transitions animate screens as they enter and exit. `IScreenTransition` controls the transition position (0.0 to 1.0), alpha, and timing. `TransitionWipeType` determines the direction of motion (slide, pop, or none).

### 8. Input System

Input is handled by `IInputHandler` implementations:

- `ControllerInputHandler` -- gamepad input via HadoukInput
- `MouseInputHandler` -- mouse/pointer input via MouseBuddy
- Touch input via TouchScreenBuddy

The input system translates raw device input into semantic events (highlight, click, drag, drop) that are dispatched to screens and widgets.

## Key Design Patterns

### Composition Over Inheritance

Screens contain layouts, which contain widgets. Rather than creating deep class hierarchies, MenuBuddy favors composition. A button is a layout that contains other widgets (labels, images) and adds click behavior.

### Event-Driven Interaction

All user interactions flow through `EventHandler` delegates:

```csharp
button.OnClick += (sender, e) => { /* handle click */ };
slider.OnDrag += (sender, e) => { /* handle drag */ };
dropdown.OnSelectedItemChange += (sender, e) => { /* handle selection */ };
```

### Static Global Styling

`StyleSheet` uses static properties so that all widgets in the application share the same visual theme without passing configuration through constructors.

### Async Content Loading

Screen content loading is asynchronous (`Task LoadContent()`), enabling loading screens that show progress while assets are being loaded.

### Stack-Based Screen Management

Screens are managed as a stack. Adding a new screen pushes it on top; exiting a screen pops it off. Screens can optionally cover those below them, triggering transition-off animations.

## Class Diagram (Simplified)

```
DefaultGame ──owns──► ScreenManager ──manages──► ScreenStack
                                                      │
                                                   [IScreen]
                                                      │
                                              ┌───────┴───────┐
                                              │               │
                                         WidgetScreen    (other screens)
                                              │
                                       AbsoluteLayout
                                              │
                                       ┌──────┼──────┐
                                       │      │      │
                                    Widget  Layout  Widget
                                             │
                                          [nested]
```

## Next Steps

- [Screens](screens) -- Deep dive into the screen system
- [Widgets](widgets) -- Full widget reference
- [Layouts](layouts) -- Layout system details
