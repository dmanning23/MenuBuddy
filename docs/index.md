---
layout: default
title: MenuBuddy
---

# MenuBuddy

A complete MonoGame library for building menu systems and managing game state transitions. Based on the NetworkStateManagement samples from the golden age of XNA.

[![NuGet](https://img.shields.io/nuget/v/MenuBuddy.svg)](https://www.nuget.org/packages/MenuBuddy/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Features

- **Screen-based architecture** -- Stack-based screen management with automatic transitions
- **Multiple input modes** -- Support for controller, mouse, and touch input
- **Rich widget library** -- Labels, buttons, sliders, dropdowns, text input, checkboxes, and more
- **Flexible layouts** -- Absolute, relative, stack, scroll, padded, and tree layouts
- **Customizable styling** -- Global stylesheet for consistent appearance across your UI
- **Smooth transitions** -- Built-in fade, slide, and wipe transitions between screens
- **Multi-platform** -- Works on DesktopGL, iOS, Android, and Windows Universal

## Quick Install

```
dotnet add package MenuBuddy
```

## Minimal Example

```csharp
using MenuBuddy;
using Microsoft.Xna.Framework;

public class MyGame : MouseGame
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

public class MainMenuScreen : MenuStackScreen
{
    public MainMenuScreen() : base("Main Menu") { }

    public override async Task LoadContent()
    {
        await base.LoadContent();

        var startButton = new MenuEntry("Start Game", Content);
        startButton.OnClick += (sender, e) =>
        {
            ScreenManager.AddScreen(new GameplayScreen());
        };
        AddMenuEntry(startButton);
    }
}
```

## Documentation

| Page | Description |
|------|-------------|
| [Getting Started](getting-started) | Installation, setup, and your first menu |
| [Architecture](architecture) | High-level design and class relationships |
| [Screens](screens) | Screen types, lifecycle, and management |
| [Widgets](widgets) | Labels, buttons, sliders, dropdowns, and more |
| [Layouts](layouts) | Absolute, stack, relative, scroll, padded, and tree layouts |
| [Styling](styling) | Global theming with the StyleSheet class |
| [Transitions](transitions) | Screen transition effects and animation |
| [Input](input) | Controller, mouse, and touch input handling |
| [API Reference](api-reference) | Complete interface and class reference |

## Dependencies

MenuBuddy depends on several companion libraries, all installed automatically via NuGet:

- [FontBuddy](https://github.com/dmanning23/FontBuddy) -- Font rendering
- [InputHelper](https://github.com/dmanning23/InputHelper) -- Input state management
- [ResolutionBuddy](https://github.com/dmanning23/ResolutionBuddy) -- Resolution management
- [PrimitiveBuddy](https://github.com/dmanning23/PrimitiveBuddy) -- Primitive rendering
- [GameTimer](https://github.com/dmanning23/GameTimer) -- Timing utilities
- [HadoukInput](https://github.com/dmanning23/HadoukInput) -- Controller input
- [MouseBuddy](https://github.com/dmanning23/MouseBuddy) -- Mouse input
- [TouchScreenBuddy](https://github.com/dmanning23/TouchScreenBuddy) -- Touch input
- [MatrixExtensions](https://github.com/dmanning23/MatrixExtensions) -- Matrix utilities
- [Vector2Extensions](https://github.com/dmanning23/Vector2Extensions) -- Vector utilities

## Sample Project

For a complete working example, see [MenuBuddySample](https://github.com/dmanning23/MenuBuddySample).

## License

MIT License -- see [LICENSE](https://github.com/dmanning23/MenuBuddy/blob/master/LICENSE.txt) for details.
