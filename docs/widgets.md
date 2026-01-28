---
layout: default
title: Widgets
---

# Widgets

Widgets are the interactive UI elements that make up your menus and screens. All widgets implement `IWidget`, which provides common capabilities like visibility, scaling, highlighting, transitions, and tap detection.

## IWidget Interface

```csharp
public interface IWidget : IScreenItem, IScalable, IHighlightable, ITransitionable, IBackgroundable
{
    bool IsHidden { get; set; }
    bool IsTappable { get; }
    bool WasTapped { get; }
    bool IsTapHeld { get; }
}
```

## Labels

Labels display text on screen. They support multiple font sizes, shadow effects, password masking, and auto-sizing.

```csharp
// Simple label
var label = new Label("Hello World", Content);
AddItem(label);

// Label with specific font size
var title = new Label("Game Title", Content, FontSize.Large);
AddItem(title);

// Label with custom position
var positioned = new Label("Score: 0", Content)
{
    Position = new Point(100, 50),
    Horizontal = HorizontalAlignment.Left
};
AddItem(positioned);
```

**FontSize Enum:**

| Value | Usage | Default Resource |
|-------|-------|-----------------|
| `Large` | Titles, headers | `StyleSheet.LargeFontResource` |
| `Medium` | Standard widgets, body text | `StyleSheet.MediumFontResource` |
| `Small` | Message boxes, captions | `StyleSheet.SmallFontResource` |

**Key Properties:**

| Property | Type | Description |
|----------|------|-------------|
| `Text` | `string` | The displayed text |
| `FontSize` | `FontSize` | The font size category |
| `IsPassword` | `bool` | Mask text with asterisks |
| `Pulsate` | `bool` | Whether the label pulsates when highlighted |
| `HasShadow` | `bool` | Whether to draw a text shadow |

### MenuTitle

A specialized label used for screen titles. Automatically uses `FontSize.Large` and supports shrink-to-fit.

```csharp
var title = new MenuTitle("Options Menu", Content);
title.ShrinkToFit(Resolution.TitleSafeArea.Width);
AddItem(title);
```

## Buttons

Buttons are widgets that respond to click events. MenuBuddy provides several button types depending on how you want to arrange the button's content.

### Common Button Properties

| Property | Type | Description |
|----------|------|-------------|
| `Size` | `Vector2` | The button dimensions |
| `IsQuiet` | `bool` | If true, suppress click/highlight sounds |
| `HighlightedSound` | `SoundEffect` | Sound played on highlight |
| `ClickedSound` | `SoundEffect` | Sound played on click |
| `Description` | `string` | Accessibility description |

### Common Button Events

```csharp
button.OnClick += (sender, e) =>
{
    // Handle click. e.PlayerIndex gives the player who clicked.
};
```

### StackLayoutButton

A button whose child widgets are arranged in a stack (vertical or horizontal).

```csharp
var button = new StackLayoutButton();
button.Size = new Vector2(300, 80);
button.AddItem(new Label("Play", Content));
button.OnClick += (sender, e) => StartGame();
AddItem(button);
```

### RelativeLayoutButton

A button whose child widgets are positioned relative to the button bounds.

```csharp
var button = new RelativeLayoutButton();
button.Size = new Vector2(200, 50);
button.AddItem(new Label("Save", Content)
{
    Horizontal = HorizontalAlignment.Center,
    Vertical = VerticalAlignment.Center
});
button.OnClick += (sender, e) => SaveGame();
AddItem(button);
```

### DragDropButton

A button that supports drag-and-drop operations.

```csharp
var draggable = new DragDropButton();
draggable.Size = new Vector2(100, 100);
draggable.AddItem(new Label("Drag Me", Content));
AddItem(draggable);
```

### CancelButton

A standard back/cancel button, typically placed in a corner. Its appearance is configured via `StyleSheet`.

```csharp
// Usually added via WidgetScreen's helper method
AddCancelButton();

// Or with a custom size
AddCancelButton(customSize: 48);
```

### MenuEntry

A simple clickable menu entry for use with `MenuStackScreen.AddMenuEntry()`.

```csharp
var entry = new MenuEntry("Start Game", Content);
entry.OnClick += (sender, e) =>
{
    ScreenManager.AddScreen(new GameplayScreen());
};
AddMenuEntry(entry);
```

### MenuEntryBool

A menu entry that toggles a boolean value with left/right input.

```csharp
var fullscreen = new MenuEntryBool("Fullscreen", Content);
fullscreen.IsChecked = false;
AddMenuEntry(fullscreen);
```

### MenuEntryInt

A menu entry that cycles through integer values with left/right input.

```csharp
var volume = new MenuEntryInt("Volume", 0, 100, Content);
volume.Value = 50;
AddMenuEntry(volume);
```

### ContinueMenuEntry

A "Press A to continue" style entry that exits the screen when activated.

```csharp
// Usually added via MenuStackScreen's helper method
AddContinueButton();
```

## Images

### Image

Displays a sprite texture.

```csharp
var img = new Image(Content.Load<Texture2D>("mySprite"))
{
    Position = new Point(400, 300),
    Size = new Vector2(128, 128)
};
AddItem(img);
```

### BackgroundImage

A tiled background image that fills the available area.

### BouncyImage

An image widget with a bouncing animation effect.

## Sliders

Sliders allow the user to select a value by dragging a handle along a track.

### Slider (float)

```csharp
var slider = new Slider()
{
    Min = 0f,
    Max = 100f,
    SliderPosition = 50f,
    HandleSize = new Vector2(64, 64),
    Size = new Vector2(512, 128)
};

slider.OnDrag += (sender, e) =>
{
    float value = slider.SliderPosition;
    // Use the value
};

AddItem(slider);
```

### IntSlider (integer)

```csharp
var intSlider = new IntSlider()
{
    Min = 0,
    Max = 10,
    SliderPosition = 5,
    HandleSize = new Vector2(64, 64),
    Size = new Vector2(512, 128)
};

intSlider.OnDrag += (sender, e) =>
{
    int value = intSlider.SliderPosition;
};

AddItem(intSlider);
```

**Slider Properties:**

| Property | Type | Description |
|----------|------|-------------|
| `Min` | `T` | Minimum value |
| `Max` | `T` | Maximum value |
| `SliderPosition` | `T` | Current value |
| `HandleSize` | `Vector2` | Size of the draggable handle |
| `Size` | `Vector2` | Overall slider dimensions |

## Text Input

### TextEdit

Opens a modal text editing dialog when tapped.

```csharp
var textEdit = new TextEdit("Default text", Content);
textEdit.Size = new Vector2(350, 128);
textEdit.Position = Resolution.ScreenArea.Center;
textEdit.HasOutline = true;
AddItem(textEdit);
```

### TextEditWithDialog

An alternative text input that uses a custom dialog variant.

### NumEdit

A numeric-only input that opens a `NumPadScreen` for number entry.

## Dropdowns

Generic dropdown selection widgets.

### Dropdown\<T\>

```csharp
var dropdown = new Dropdown<string>(this);
dropdown.Size = new Vector2(350, 128);
dropdown.Position = Resolution.ScreenArea.Center;

string[] options = { "Easy", "Normal", "Hard" };
foreach (var option in options)
{
    var item = new DropdownItem<string>(option, dropdown)
    {
        Size = new Vector2(350, 64)
    };
    item.AddItem(new Label(option, Content, FontSize.Small));
    dropdown.AddDropdownItem(item);
}

dropdown.SelectedItem = "Normal";

dropdown.OnSelectedItemChange += (sender, e) =>
{
    string selected = e.SelectedItem;
    // Handle selection change
};

AddItem(dropdown);
```

**Key Properties:**

| Property | Type | Description |
|----------|------|-------------|
| `SelectedItem` | `T` | The currently selected value |
| `Size` | `Vector2` | Dropdown dimensions |

**Events:**

| Event | Args | Description |
|-------|------|-------------|
| `OnSelectedItemChange` | `SelectionChangeEventArgs<T>` | Fired when the selection changes |

## Checkboxes

Toggle widgets with checked/unchecked states.

```csharp
var checkbox = new Checkbox();
checkbox.IsChecked = true;

checkbox.OnClick += (sender, e) =>
{
    bool isChecked = checkbox.IsChecked;
    // Handle toggle
};

AddItem(checkbox);
```

The checked and unchecked images are configured via `StyleSheet.CheckedImageResource` and `StyleSheet.UncheckedImageResource`.

## Shim

An invisible spacer widget used to add space between other widgets in a layout.

```csharp
// Add vertical space in a stack layout
stack.AddItem(new Shim(0, 20)); // width=0, height=20
```

## Context Menu

A popup context menu that appears on right-click or long press.

## Hamburger

A hamburger menu button that opens a slide-out menu panel.

## Tree Items

Hierarchical tree node widgets used with the `Tree` layout.

## Common Widget Patterns

### Positioning

```csharp
widget.Position = new Point(x, y);
widget.Horizontal = HorizontalAlignment.Center;
widget.Vertical = VerticalAlignment.Center;
```

### Sizing

```csharp
widget.Size = new Vector2(width, height);
```

### Visibility

```csharp
widget.IsHidden = true;  // Hide without removing
widget.IsHidden = false; // Show again
```

### Tap Detection

```csharp
if (widget.WasTapped)
{
    // Rising edge: tapped this frame
}

if (widget.IsTapHeld)
{
    // Tap is being held down
}
```

## Next Steps

- [Layouts](layouts) -- Position and arrange widgets
- [Styling](styling) -- Customize widget appearance
- [Screens](screens) -- Add widgets to screens
