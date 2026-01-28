---
layout: default
title: Getting Started
---

# Getting Started

This guide walks you through installing MenuBuddy, setting up your game class, and creating your first menu screen.

## Prerequisites

- .NET 8.0 or later
- MonoGame 3.8+
- A MonoGame project (DesktopGL, Android, iOS, or Windows Universal)

## Installation

Install via NuGet:

```
dotnet add package MenuBuddy
```

Or via the NuGet Package Manager:

```
Install-Package MenuBuddy
```

Or visit: [nuget.org/packages/MenuBuddy](https://www.nuget.org/packages/MenuBuddy/)

## Step 1: Create Your Game Class

MenuBuddy provides three game base classes depending on your input type. Choose one and extend it:

```csharp
using MenuBuddy;
using Microsoft.Xna.Framework;

// For desktop with controller/gamepad support
public class MyGame : ControllerGame
{
    public MyGame()
    {
        VirtualResolution = new Point(1280, 720);
        ScreenResolution = new Point(1280, 720);
    }

    public override IScreen[] GetMainMenuScreenStack()
    {
        return new IScreen[] { new MainMenuScreen() };
    }
}
```

Alternative game base classes:

```csharp
// For mouse-based desktop input
public class MyGame : MouseGame { ... }

// For mobile/touch devices
public class MyGame : TouchGame { ... }
```

### Game Class Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `VirtualResolution` | `Point` | 1280x720 | The logical resolution used for game logic and rendering |
| `ScreenResolution` | `Point` | 1280x720 | The actual display resolution |
| `Fullscreen` | `bool` | `false` | Whether to run in fullscreen mode |
| `Letterbox` | `bool` | `false` | Whether to use letterboxing when aspect ratios differ |
| `UseDeviceResolution` | `bool?` | `null` | If set, uses the device's native resolution |
| `ContentRootDirectory` | `string` | `"Content"` | Root directory for content loading |
| `LoadContentWithLoadingScreen` | `bool` | `true` | Whether to show a loading screen during initial load |

## Step 2: Create a Menu Screen

The simplest way to create a menu is to extend `MenuStackScreen`, which provides a title and a vertical stack of menu entries:

```csharp
using MenuBuddy;
using InputHelper;
using System.Threading.Tasks;

public class MainMenuScreen : MenuStackScreen
{
    public MainMenuScreen() : base("Main Menu")
    {
    }

    public override async Task LoadContent()
    {
        await base.LoadContent();

        // Add a "Start Game" button
        var startButton = new MenuEntry("Start Game", Content);
        startButton.OnClick += (sender, e) =>
        {
            ScreenManager.AddScreen(new GameplayScreen());
        };
        AddMenuEntry(startButton);

        // Add an "Options" button
        var optionsButton = new MenuEntry("Options", Content);
        optionsButton.OnClick += (sender, e) =>
        {
            ScreenManager.AddScreen(new OptionsScreen());
        };
        AddMenuEntry(optionsButton);

        // Add an "Exit" button
        var exitButton = new MenuEntry("Exit", Content);
        exitButton.OnClick += (sender, e) =>
        {
            ScreenManager.Game.Exit();
        };
        AddMenuEntry(exitButton);
    }
}
```

## Step 3: Run Your Game

In your `Program.cs` or entry point:

```csharp
using var game = new MyGame();
game.Run();
```

## Step 4: Customize Styles (Optional)

Override `InitStyles()` in your game class to customize the appearance:

```csharp
public class MyGame : MouseGame
{
    // ... constructor and GetMainMenuScreenStack ...

    protected override void InitStyles()
    {
        base.InitStyles();

        StyleSheet.LargeFontResource = @"Fonts\MyTitleFont";
        StyleSheet.MediumFontResource = @"Fonts\MyBodyFont";
        StyleSheet.SmallFontResource = @"Fonts\MySmallFont";

        StyleSheet.NeutralTextColor = Color.White;
        StyleSheet.HighlightedTextColor = Color.Yellow;
        StyleSheet.NeutralBackgroundColor = new Color(0, 0, 50, 128);
    }
}
```

See the [Styling](styling) page for a full list of configurable properties.

## Step 5: Add More Screens

Create additional screens by extending the appropriate base class:

```csharp
// A custom widget screen (no menu navigation, just widgets)
public class GameplayScreen : WidgetScreen
{
    public GameplayScreen() : base("Gameplay")
    {
        CoverOtherScreens = true;
    }

    public override async Task LoadContent()
    {
        await base.LoadContent();
        // Add your gameplay UI widgets here
    }
}

// A simple options screen with a cancel button
public class OptionsScreen : MenuStackScreen
{
    public OptionsScreen() : base("Options") { }

    public override async Task LoadContent()
    {
        await base.LoadContent();

        // Add options entries here...

        // Add a back button
        AddCancelButton();
    }
}
```

## Platform-Specific Setup

Use conditional compilation for cross-platform games:

```csharp
#if __IOS__ || ANDROID || WINDOWS_UAP
public class Game1 : TouchGame
#else
public class Game1 : ControllerGame
#endif
{
    public Game1()
    {
#if DESKTOP
        IsMouseVisible = true;
#endif
        VirtualResolution = new Point(1280, 720);
        ScreenResolution = new Point(1280, 720);
    }

    public override IScreen[] GetMainMenuScreenStack()
    {
        return new IScreen[] { new MainMenuScreen() };
    }
}
```

## Next Steps

- [Architecture](architecture) -- Understand how MenuBuddy is structured
- [Screens](screens) -- Learn about the screen lifecycle and types
- [Widgets](widgets) -- Explore the full widget library
- [Layouts](layouts) -- Position widgets with layouts
