# MenuBuddy

[![NuGet](https://img.shields.io/nuget/v/MenuBuddy.svg)](https://www.nuget.org/packages/MenuBuddy/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A complete MonoGame library for building menu systems and managing game state transitions. Based on the famous NetworkStateManagement samples from the golden age of XNA.

## Features

- **Screen-based architecture** - Stack-based screen management with automatic transitions
- **Multiple input modes** - Support for controller, mouse, and touch input
- **Rich widget library** - Labels, buttons, sliders, dropdowns, text input, checkboxes, and more
- **Flexible layouts** - Absolute, relative, stack, scroll, padded, and tree layouts
- **Customizable styling** - Global stylesheet for consistent appearance across your UI
- **Smooth transitions** - Built-in fade, slide, and wipe transitions between screens
- **Multi-platform** - Works on DesktopGL, iOS, Android, and Windows Universal

## Installation

Install via NuGet:

```
dotnet add package MenuBuddy
```

Or visit: https://www.nuget.org/packages/MenuBuddy/

## Quick Start

### 1. Create Your Game Class

Extend one of the provided game base classes depending on your input type:

```csharp
using MenuBuddy;
using Microsoft.Xna.Framework;

// For desktop with controller support
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

// For mobile/touch devices, use TouchGame instead:
// public class MyGame : TouchGame

// For mouse-based input:
// public class MyGame : MouseGame
```

### 2. Create a Menu Screen

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

        // Add menu entries
        var startButton = new MenuEntry("Start Game", Content);
        startButton.OnClick += (sender, e) =>
        {
            ScreenManager.AddScreen(new GameplayScreen());
        };
        AddMenuEntry(startButton);

        var optionsButton = new MenuEntry("Options", Content);
        optionsButton.OnClick += (sender, e) =>
        {
            ScreenManager.AddScreen(new OptionsScreen());
        };
        AddMenuEntry(optionsButton);

        var exitButton = new MenuEntry("Exit", Content);
        exitButton.OnClick += (sender, e) =>
        {
            ScreenManager.Game.Exit();
        };
        AddMenuEntry(exitButton);
    }
}
```

### 3. Run Your Game

```csharp
using var game = new MyGame();
game.Run();
```

## Core Concepts

### Screens

Screens are the fundamental building blocks of MenuBuddy. Each screen represents a distinct game state (main menu, options, gameplay, pause menu, etc.).

| Screen Type | Description |
|-------------|-------------|
| `Screen` | Abstract base class for all screens |
| `WidgetScreen` | Screen that can contain and manage widgets |
| `MenuScreen` | Widget screen with keyboard/controller navigation |
| `MenuStackScreen` | Menu screen with vertical stack layout |
| `MessageBoxScreen` | Modal dialog with OK/Cancel options |
| `OkScreen` | Simple alert dialog with OK button |
| `LoadingScreen` | Shows progress while loading content |
| `ErrorScreen` | Displays exception information |

### Screen Lifecycle

```
LoadContent() -> Update() -> Draw() -> UnloadContent()
```

Screens support transitions when being added or removed:

```csharp
// Add a screen with transition
await ScreenManager.AddScreen(new OptionsScreen(), controllingPlayer);

// Remove screen with exit transition
screen.ExitScreen();

// Clear all screens
ScreenManager.ClearScreens();
```

### Widgets

Widgets are interactive UI elements that can be added to screens.

#### Labels

```csharp
// Simple label
var label = new Label("Hello World", Content);
AddItem(label);

// Label with font size
var titleLabel = new Label("Game Title", Content, FontSize.Large);
AddItem(titleLabel);
```

#### Buttons

```csharp
// Stack layout button (content stacked vertically/horizontally)
var button = new StackLayoutButton();
button.AddItem(new Label("Click Me", Content));
button.OnClick += (sender, e) => HandleClick();
AddItem(button);

// Relative layout button (content positioned relatively)
var relButton = new RelativeLayoutButton();
relButton.Size = new Vector2(200, 50);
relButton.AddItem(new Label("Button", Content));
AddItem(relButton);
```

#### Sliders

```csharp
var slider = new Slider()
{
    Min = 0,
    Max = 100,
    SliderPosition = 50,
    HandleSize = new Vector2(64, 64),
    Size = new Vector2(512, 128)
};

slider.OnDrag += (sender, e) =>
{
    var value = slider.SliderPosition;
    // Handle value change
};

AddItem(slider);
```

#### Text Input

```csharp
var textEdit = new TextEdit("Default text", Content);
textEdit.Size = new Vector2(350, 128);
textEdit.Position = Resolution.ScreenArea.Center;
textEdit.HasOutline = true;
AddItem(textEdit);
```

#### Dropdowns

```csharp
var dropdown = new Dropdown<string>(this);
dropdown.Size = new Vector2(350, 128);
dropdown.Position = Resolution.ScreenArea.Center;

string[] options = { "Option 1", "Option 2", "Option 3" };
foreach (var option in options)
{
    var item = new DropdownItem<string>(option, dropdown)
    {
        Size = new Vector2(350, 64)
    };
    item.AddItem(new Label(option, Content, FontSize.Small));
    dropdown.AddDropdownItem(item);
}

dropdown.SelectedItem = "Option 1";
AddItem(dropdown);
```

#### Checkboxes

```csharp
var checkbox = new Checkbox();
checkbox.IsChecked = true;
checkbox.OnClick += (sender, e) =>
{
    bool isChecked = checkbox.IsChecked;
};
AddItem(checkbox);
```

### Layouts

Layouts control how widgets are positioned and arranged.

#### Stack Layout

Arranges items in a vertical or horizontal stack:

```csharp
var stack = new StackLayout()
{
    Alignment = StackAlignment.Top,
    Horizontal = HorizontalAlignment.Center,
    Position = new Point(Resolution.ScreenArea.Center.X, 100)
};

stack.AddItem(new Label("Item 1", Content));
stack.AddItem(new Label("Item 2", Content));
stack.AddItem(new Label("Item 3", Content));

AddItem(stack);
```

#### Scroll Layout

Provides scrollable content area:

```csharp
var scroll = new ScrollLayout()
{
    Position = Resolution.ScreenArea.Center,
    Size = new Vector2(400, 300)
};

// Add many items that exceed the visible area
for (int i = 0; i < 20; i++)
{
    scroll.AddItem(new Label($"Item {i}", Content));
}

AddItem(scroll);
```

#### Relative Layout

Position items relative to each other or the container:

```csharp
var layout = new RelativeLayout();
layout.Size = new Vector2(500, 400);

var label = new Label("Centered", Content)
{
    Horizontal = HorizontalAlignment.Center,
    Vertical = VerticalAlignment.Center
};

layout.AddItem(label);
AddItem(layout);
```

### Message Boxes

```csharp
// Confirmation dialog
var confirm = new MessageBoxScreen("Are you sure?");
confirm.OnSelect += (sender, e) =>
{
    // User clicked OK
};
confirm.OnCancel += (sender, e) =>
{
    // User clicked Cancel
};
await ScreenManager.AddScreen(confirm, controllingPlayer);

// Simple alert
var alert = new OkScreen("Operation complete!", Content);
await ScreenManager.AddScreen(alert, controllingPlayer);
```

### Loading Screens

Show a loading screen while content loads asynchronously:

```csharp
// Load screens with loading indicator
LoadingScreen.Load(ScreenManager, new IScreen[] { new GameplayScreen() });

// With custom message
LoadingScreen.Load(ScreenManager, null, "Loading level...", new GameplayScreen());
```

## Styling

Customize the appearance of your UI through the `StyleSheet` class:

```csharp
protected override void InitStyles()
{
    base.InitStyles();

    // Fonts
    StyleSheet.LargeFontResource = @"Fonts\MyLargeFont";
    StyleSheet.MediumFontResource = @"Fonts\MyMediumFont";
    StyleSheet.SmallFontResource = @"Fonts\MySmallFont";

    StyleSheet.LargeFontSize = 72;
    StyleSheet.MediumFontSize = 48;
    StyleSheet.SmallFontSize = 24;

    // Colors
    StyleSheet.NeutralTextColor = Color.White;
    StyleSheet.HighlightedTextColor = Color.Yellow;
    StyleSheet.SelectedTextColor = Color.Gold;
    StyleSheet.NeutralBackgroundColor = new Color(0, 0, 50, 128);
    StyleSheet.HighlightedBackgroundColor = new Color(0, 0, 100, 180);

    // Sounds
    StyleSheet.HighlightedSoundResource = @"Sounds\MenuMove";
    StyleSheet.ClickedSoundResource = @"Sounds\MenuSelect";

    // Images
    StyleSheet.ButtonBackgroundImageResource = @"Images\ButtonBg";
    StyleSheet.CheckedImageResource = @"Images\Checked";
    StyleSheet.UncheckedImageResource = @"Images\Unchecked";

    // Options
    StyleSheet.HasOutline = true;
    StyleSheet.DefaultTransition = TransitionWipeType.SlideLeft;
}
```

### Available Style Properties

| Property | Description |
|----------|-------------|
| `LargeFontResource` | Font for titles |
| `MediumFontResource` | Font for widgets |
| `SmallFontResource` | Font for message boxes |
| `NeutralTextColor` | Default text color |
| `HighlightedTextColor` | Text color when highlighted |
| `SelectedTextColor` | Text color when selected |
| `NeutralBackgroundColor` | Default background color |
| `HighlightedBackgroundColor` | Background when highlighted |
| `HighlightedSoundResource` | Sound when item highlighted |
| `ClickedSoundResource` | Sound when item clicked |
| `HasOutline` | Enable outline rendering |
| `DefaultTransition` | Default screen transition type |

## Screen Transitions

MenuBuddy supports various transition effects:

```csharp
// Configure transition times
screen.Transition.OnTime = 0.5f;  // Time to transition on
screen.Transition.OffTime = 0.5f; // Time to transition off

// Set transition type via StyleSheet
StyleSheet.DefaultTransition = TransitionWipeType.SlideLeft;
```

Available transition types:
- `TransitionWipeType.SlideLeft`
- `TransitionWipeType.SlideRight`
- `TransitionWipeType.PopTop`
- `TransitionWipeType.PopBottom`
- `TransitionWipeType.PopLeft`
- `TransitionWipeType.PopRight`

## Screen Properties

Control how screens interact with each other:

```csharp
public class MyScreen : WidgetScreen
{
    public MyScreen() : base("My Screen")
    {
        // This screen covers screens below it
        CoverOtherScreens = true;

        // This screen can be covered by screens above it
        CoveredByOtherScreens = true;

        // Modal screens block input to screens below
        Modal = true;
    }
}
```

## Cancel Button

Add a standard cancel/back button to screens:

```csharp
public override async Task LoadContent()
{
    await base.LoadContent();

    // Adds cancel button in corner (position configurable via StyleSheet)
    AddCancelButton();
}
```

## Error Handling

Display errors gracefully:

```csharp
try
{
    // Game logic
}
catch (Exception ex)
{
    ScreenManager.ErrorScreen(ex);
}
```

## Platform-Specific Setup

Use conditional compilation for platform-specific behavior:

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
    }
}
```

## Dependencies

MenuBuddy depends on several companion libraries (automatically installed via NuGet):

- [FontBuddy](https://github.com/dmanning23/FontBuddy) - Font rendering
- [InputHelper](https://github.com/dmanning23/InputHelper) - Input state management
- [ResolutionBuddy](https://github.com/dmanning23/ResolutionBuddy) - Resolution management
- [PrimitiveBuddy](https://github.com/dmanning23/PrimitiveBuddy) - Primitive rendering
- [GameTimer](https://github.com/dmanning23/GameTimer) - Timing utilities
- And others...

## Sample Project

For a complete working example with all widgets and features demonstrated, see the [MenuBuddySample](https://github.com/dmanning23/MenuBuddySample) project.

The sample includes examples of:
- Main menu with navigation
- Options screens
- Scroll layouts
- Dropdowns
- Sliders
- Text input
- Tree layouts
- Context menus
- Drag and drop
- Loading screens
- Message boxes

## API Reference

### Game Base Classes

| Class | Input Type | Use Case |
|-------|------------|----------|
| `ControllerGame` | Gamepad | Console/desktop with controller |
| `MouseGame` | Mouse | Desktop applications |
| `TouchGame` | Touch | Mobile devices |

### Key Interfaces

| Interface | Description |
|-----------|-------------|
| `IScreen` | Base screen contract |
| `IScreenManager` | Screen management operations |
| `IWidget` | Base widget behavior |
| `ILayout` | Container layout behavior |

### Common Methods

```csharp
// ScreenManager
await ScreenManager.AddScreen(screen, player);
ScreenManager.ClearScreens();
ScreenManager.ErrorScreen(exception);

// Screen
screen.ExitScreen();
screen.AddItem(widget);
screen.RemoveItem(widget);

// Widget
widget.Position = new Point(x, y);
widget.Size = new Vector2(width, height);
widget.Horizontal = HorizontalAlignment.Center;
widget.Vertical = VerticalAlignment.Center;
```

## License

MIT License - see [LICENSE](LICENSE) for details.

## Contributing

Contributions are welcome! Please feel free to submit issues and pull requests on [GitHub](https://github.com/dmanning23/MenuBuddy).
