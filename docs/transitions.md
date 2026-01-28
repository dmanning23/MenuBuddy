---
layout: default
title: Transitions
---

# Transitions

MenuBuddy includes a transition system for animating screens as they appear and disappear. Transitions control the position and alpha of screens over time.

## IScreenTransition Interface

```csharp
public interface IScreenTransition
{
    float OnTime { get; set; }         // Duration (seconds) to transition on
    float OffTime { get; set; }        // Duration (seconds) to transition off
    float TransitionPosition { get; }  // Current position (0.0 = off, 1.0 = on)
    float Alpha { get; }              // Current alpha (0.0 = transparent, 1.0 = opaque)
    TransitionState State { get; set; }

    event EventHandler OnStateChange;

    Color AlphaColor(Color color);     // Apply alpha to a color
    void Restart(bool transitionOn);   // Restart the transition
    bool Update(GameClock gameTime, bool transitionOn); // Returns true if still transitioning
}
```

## TransitionState Enum

| State | Description |
|-------|-------------|
| `Hidden` | Screen is not visible, transition is complete (off) |
| `TransitionOn` | Screen is animating into view |
| `Active` | Screen is fully visible |
| `TransitionOff` | Screen is animating out of view |

## Transition Flow

```
Hidden ──► TransitionOn ──► Active ──► TransitionOff ──► Hidden
                                           │
                                           ▼
                                     (if exiting)
                                      Removed
```

When a screen is added, it transitions from `Hidden` to `Active` through `TransitionOn`. When a screen exits or is covered, it transitions from `Active` to `Hidden` through `TransitionOff`.

## Configuring Transition Timing

```csharp
public class MyScreen : WidgetScreen
{
    public MyScreen() : base("My Screen")
    {
        // Set transition durations (in seconds)
        Transition.OnTime = 0.5f;   // Half second to appear
        Transition.OffTime = 0.3f;  // 300ms to disappear
    }
}
```

## TransitionWipeType Enum

Controls the direction of screen transitions.

### Pop Transitions

Items enter and exit from the same direction:

| Type | Enter | Exit |
|------|-------|------|
| `PopLeft` | Slides in from left | Slides out to left |
| `PopRight` | Slides in from right | Slides out to right |
| `PopTop` | Slides in from top | Slides out to top |
| `PopBottom` | Slides in from bottom | Slides out to bottom |

### Slide Transitions

Items enter from one direction and exit to the opposite:

| Type | Enter | Exit |
|------|-------|------|
| `SlideLeft` | Slides in from left | Slides out to right |
| `SlideRight` | Slides in from right | Slides out to left |
| `SlideTop` | Slides in from top | Slides out to bottom |
| `SlideBottom` | Slides in from bottom | Slides out to top |

### No Transition

| Type | Behavior |
|------|----------|
| `None` | No movement or animation |

## Setting the Default Transition

Set the global default transition type in `InitStyles()`:

```csharp
protected override void InitStyles()
{
    base.InitStyles();
    StyleSheet.DefaultTransition = TransitionWipeType.SlideLeft;
}
```

## Transition Objects

MenuBuddy provides several transition object types for animating individual elements:

### BaseTransitionObject

The abstract base for all transition-aware objects.

### PointTransitionObject

Animates a `Point` value between two positions over time.

### PathTransitionObject

Animates an object along a defined path.

### DirectionTransitionObject

Animates in a specific direction based on `TransitionWipeType`.

### WipeTransitionObject

Implements the wipe-based transition effects (slide/pop).

## Using Transitions with Alpha

The `IScreenTransition.Alpha` property provides the current opacity level during transitions. Use it to fade elements:

```csharp
public override void Draw(GameTime gameTime)
{
    // Alpha is automatically applied to screen rendering
    // You can also use it manually:
    Color fadeColor = Transition.AlphaColor(Color.White);

    ScreenManager.SpriteBatchBegin();
    // Draw with fadeColor for manual alpha control
    ScreenManager.SpriteBatchEnd();
}
```

## Background Fading

Screens can fade the background behind them during transitions:

```csharp
public override void Draw(GameTime gameTime)
{
    // Fade background to 66% opacity
    FadeBackground();

    // Or with a custom alpha
    FadeBackground(0.8f);

    base.Draw(gameTime);
}
```

## Next Steps

- [Screens](screens) -- Screen lifecycle and transition states
- [Styling](styling) -- Set default transition types
- [Architecture](architecture) -- How transitions fit into the system
