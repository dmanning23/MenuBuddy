---
layout: default
title: Styling
---

# Styling

MenuBuddy uses a global `StyleSheet` class with static properties for consistent theming across your entire UI. All widgets read their default appearance from `StyleSheet`, so you can customize the look of your game in one place.

## Setting Up Styles

Override `InitStyles()` in your game class:

```csharp
public class MyGame : MouseGame
{
    public MyGame()
    {
        VirtualResolution = new Point(1280, 720);
        ScreenResolution = new Point(1280, 720);
    }

    protected override void InitStyles()
    {
        base.InitStyles();

        // Fonts
        StyleSheet.LargeFontResource = @"Fonts\TitleFont";
        StyleSheet.MediumFontResource = @"Fonts\BodyFont";
        StyleSheet.SmallFontResource = @"Fonts\CaptionFont";
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

        // Options
        StyleSheet.HasOutline = true;
        StyleSheet.DefaultTransition = TransitionWipeType.SlideLeft;
        StyleSheet.ClearColor = new Color(0.0f, 0.1f, 0.2f);
    }

    public override IScreen[] GetMainMenuScreenStack()
    {
        return new IScreen[] { new MainMenuScreen() };
    }
}
```

## Font Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `LargeFontResource` | `string` | `@"Fonts\ArialBlack72"` | Content path for the large font (titles) |
| `MediumFontResource` | `string` | `@"Fonts\ArialBlack48"` | Content path for the medium font (widgets) |
| `SmallFontResource` | `string` | `@"Fonts\ArialBlack24"` | Content path for the small font (message boxes) |
| `LargeFontSize` | `int` | `144` | Large font size (used with FontPlus) |
| `MediumFontSize` | `int` | `96` | Medium font size (used with FontPlus) |
| `SmallFontSize` | `int` | `48` | Small font size (used with FontPlus) |
| `UseFontPlus` | `bool` | `false` | Use FontBuddy's FontPlus rendering |

## Color Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `NeutralTextColor` | `Color` | `White` | Default text color |
| `HighlightedTextColor` | `Color` | `White` | Text color when highlighted |
| `SelectedTextColor` | `Color` | `Yellow` | Text color when selected |
| `NeutralOutlineColor` | `Color` | `(0.8, 0.8, 0.8, 0.5)` | Default outline color |
| `HighlightedOutlineColor` | `Color` | `(0.8, 0.8, 0.8, 0.7)` | Outline color when highlighted |
| `NeutralBackgroundColor` | `Color` | `(0.0, 0.0, 0.2, 0.5)` | Default background color |
| `HighlightedBackgroundColor` | `Color` | `(0.0, 0.0, 0.2, 0.7)` | Background when highlighted |
| `TextShadowColor` | `Color` | `Black` | Text shadow color |
| `MessageBoxTextColor` | `Color` | `White` | Text color in message boxes |
| `ClearColor` | `Color` | `(0.0, 0.1, 0.2)` | Screen clear/background color |

## Sound Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `HighlightedSoundResource` | `string` | `@"MenuMove"` | Sound when an item is highlighted |
| `ClickedSoundResource` | `string` | `@"MenuSelect"` | Sound when an item is clicked |
| `CancelButtonSoundResource` | `string` | Same as Clicked | Sound for the cancel button |

## Image Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ButtonBackgroundImageResource` | `string` | `@"AlphaGradient"` | Button background texture |
| `MessageBoxBackgroundImageResource` | `string` | `@"gradient"` | Message box background |
| `MessageBoxOkImageResource` | `string` | -- | OK button image in message boxes |
| `MessageBoxCancelImageResource` | `string` | -- | Cancel button image in message boxes |
| `CancelButtonImageResource` | `string` | `@"Cancel"` | Cancel/back button image |
| `CheckedImageResource` | `string` | `@"Checked"` | Checkbox checked state image |
| `UncheckedImageResource` | `string` | `@"Unchecked"` | Checkbox unchecked state image |
| `TreeExpandImageResource` | `string` | `@"Expand"` | Tree expand icon |
| `TreeCollapseImageResource` | `string` | `@"Collapse"` | Tree collapse icon |
| `DropdownImageResource` | `string` | `@"Collapse"` | Dropdown arrow icon |
| `LoadingScreenHourglassImageResource` | `string` | `@"hourglass"` | Loading screen animation |
| `FadeBackgroundImageResource` | `string` | `""` | Background fade overlay image |

## Display Options

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `HasOutline` | `bool` | `true` | Enable outline rendering on widgets |
| `DefaultTransition` | `TransitionWipeType` | `SlideLeft` | Default screen transition type |
| `SmallFontHasShadow` | `bool` | `false` | Whether small font text has shadows |

## Cancel Button Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `CancelButtonOffset` | `Point` | `(-64, 0)` | Position offset for the cancel button |
| `CancelButtonHasOutline` | `bool` | `true` | Whether the cancel button has an outline |
| `CancelButtonSize` | `int?` | `null` | Custom cancel button size (null = auto) |

## Example Themes

### Dark Theme

```csharp
protected override void InitStyles()
{
    base.InitStyles();

    StyleSheet.NeutralTextColor = new Color(200, 200, 200);
    StyleSheet.HighlightedTextColor = Color.White;
    StyleSheet.SelectedTextColor = new Color(100, 180, 255);
    StyleSheet.NeutralBackgroundColor = new Color(20, 20, 30, 200);
    StyleSheet.HighlightedBackgroundColor = new Color(40, 40, 60, 220);
    StyleSheet.NeutralOutlineColor = new Color(60, 60, 80, 150);
    StyleSheet.HighlightedOutlineColor = new Color(100, 100, 140, 200);
    StyleSheet.ClearColor = new Color(10, 10, 20);
}
```

### Bright Theme

```csharp
protected override void InitStyles()
{
    base.InitStyles();

    StyleSheet.NeutralTextColor = new Color(50, 50, 50);
    StyleSheet.HighlightedTextColor = new Color(0, 100, 200);
    StyleSheet.SelectedTextColor = new Color(200, 50, 0);
    StyleSheet.NeutralBackgroundColor = new Color(240, 240, 245, 220);
    StyleSheet.HighlightedBackgroundColor = new Color(220, 230, 255, 240);
    StyleSheet.NeutralOutlineColor = new Color(180, 180, 190, 150);
    StyleSheet.HighlightedOutlineColor = new Color(100, 150, 220, 200);
    StyleSheet.ClearColor = new Color(245, 245, 250);
}
```

### Retro Theme

```csharp
protected override void InitStyles()
{
    base.InitStyles();

    StyleSheet.NeutralTextColor = new Color(0, 255, 0);       // Green
    StyleSheet.HighlightedTextColor = new Color(255, 255, 0); // Yellow
    StyleSheet.SelectedTextColor = new Color(255, 100, 0);    // Orange
    StyleSheet.NeutralBackgroundColor = new Color(0, 0, 0, 200);
    StyleSheet.HighlightedBackgroundColor = new Color(0, 30, 0, 220);
    StyleSheet.TextShadowColor = new Color(0, 100, 0);
    StyleSheet.ClearColor = Color.Black;
    StyleSheet.HasOutline = false;
}
```

## Next Steps

- [Transitions](transitions) -- Configure transition effects
- [Widgets](widgets) -- Widgets that use style properties
- [Getting Started](getting-started) -- Initial project setup
