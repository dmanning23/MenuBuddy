---
layout: default
title: Layouts
---

# Layouts

Layouts are containers that control how widgets are positioned and arranged. They can be nested inside each other and inside screens to create complex UI structures.

## ILayout Interface

All layouts implement `ILayout`:

```csharp
public interface ILayout : IScreenItemContainer, IScreenItem, IScalable,
    IClickable, IHighlightable, IDraggable, IDroppable, ITransitionable
{
    List<IScreenItem> Items { get; }
    bool RemoveItems<T>() where T : IScreenItem;
    void Clear();
}
```

**Common Methods (from IScreenItemContainer):**

| Method | Description |
|--------|-------------|
| `AddItem(IScreenItem)` | Add an item to the layout |
| `RemoveItem(IScreenItem)` | Remove a specific item |
| `RemoveItems<T>()` | Remove all items of a type |
| `Clear()` | Remove all items |

## AbsoluteLayout

Items are placed at explicit positions within the layout bounds. This is the default layout used by `WidgetScreen`.

```csharp
var layout = new AbsoluteLayout()
{
    Position = new Point(0, 0),
    Size = new Vector2(1280, 720)
};

// Items are positioned explicitly
var label = new Label("Top Left", Content)
{
    Position = new Point(10, 10),
    Horizontal = HorizontalAlignment.Left,
    Vertical = VerticalAlignment.Top
};
layout.AddItem(label);

var centered = new Label("Center", Content)
{
    Position = new Point(640, 360),
    Horizontal = HorizontalAlignment.Center,
    Vertical = VerticalAlignment.Center
};
layout.AddItem(centered);

AddItem(layout);
```

**Properties:**

| Property | Type | Description |
|----------|------|-------------|
| `Position` | `Point` | Top-left corner of the layout |
| `Size` | `Vector2` | Width and height of the layout |
| `Horizontal` | `HorizontalAlignment` | Default horizontal alignment for children |
| `Vertical` | `VerticalAlignment` | Default vertical alignment for children |

## StackLayout

Arranges items in a vertical or horizontal stack, automatically positioning each item after the previous one.

```csharp
// Vertical stack (top-aligned)
var stack = new StackLayout()
{
    Alignment = StackAlignment.Top,
    Horizontal = HorizontalAlignment.Center,
    Position = new Point(Resolution.TitleSafeArea.Center.X, 100)
};

stack.AddItem(new Label("Item 1", Content));
stack.AddItem(new Label("Item 2", Content));
stack.AddItem(new Label("Item 3", Content));

AddItem(stack);
```

**StackAlignment Enum:**

| Value | Behavior |
|-------|----------|
| `Top` | Stack items downward from the position |
| `Bottom` | Stack items upward from the position |
| `Left` | Stack items rightward from the position |
| `Right` | Stack items leftward from the position |

**Properties:**

| Property | Type | Description |
|----------|------|-------------|
| `Alignment` | `StackAlignment` | Direction items stack |
| `Position` | `Point` | Starting position of the stack |
| `Horizontal` | `HorizontalAlignment` | Horizontal alignment of items |

**Example -- Horizontal stack:**

```csharp
var toolbar = new StackLayout()
{
    Alignment = StackAlignment.Left,
    Vertical = VerticalAlignment.Center,
    Position = new Point(50, Resolution.TitleSafeArea.Center.Y)
};

toolbar.AddItem(new Image(saveIcon));
toolbar.AddItem(new Shim(10, 0)); // spacer
toolbar.AddItem(new Image(loadIcon));
toolbar.AddItem(new Shim(10, 0));
toolbar.AddItem(new Image(settingsIcon));

AddItem(toolbar);
```

## RelativeLayout

Items are positioned relative to the layout container bounds using alignment properties.

```csharp
var layout = new RelativeLayout();
layout.Size = new Vector2(500, 400);
layout.Position = Resolution.ScreenArea.Center;

// Center a label in the layout
var label = new Label("Centered", Content)
{
    Horizontal = HorizontalAlignment.Center,
    Vertical = VerticalAlignment.Center
};
layout.AddItem(label);

// Top-right corner
var icon = new Image(closeTexture)
{
    Horizontal = HorizontalAlignment.Right,
    Vertical = VerticalAlignment.Top
};
layout.AddItem(icon);

AddItem(layout);
```

## ScrollLayout

A scrollable container for content that exceeds the visible area. Supports both mouse/touch scrolling.

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

## PaddedLayout

Adds padding around the contained items.

```csharp
var padded = new PaddedLayout()
{
    Position = new Point(100, 100),
    Size = new Vector2(400, 300)
};

padded.AddItem(new Label("Padded content", Content));
AddItem(padded);
```

## Tree

A hierarchical tree layout with expandable/collapsible nodes. Tree expand/collapse icons are configured via `StyleSheet.TreeExpandImageResource` and `StyleSheet.TreeCollapseImageResource`.

```csharp
var tree = new Tree();

var root = new TreeItem("Root");
root.AddItem(new Label("Root", Content));

var child1 = new TreeItem("Child 1");
child1.AddItem(new Label("Child 1", Content));
root.AddChild(child1);

var child2 = new TreeItem("Child 2");
child2.AddItem(new Label("Child 2", Content));
root.AddChild(child2);

tree.AddItem(root);
AddItem(tree);
```

## Alignment Enums

### HorizontalAlignment

| Value | Description |
|-------|-------------|
| `Left` | Align to the left edge |
| `Center` | Center horizontally |
| `Right` | Align to the right edge |

### VerticalAlignment

| Value | Description |
|-------|-------------|
| `Top` | Align to the top edge |
| `Center` | Center vertically |
| `Bottom` | Align to the bottom edge |

## Nesting Layouts

Layouts can be nested to create complex structures:

```csharp
// A scroll layout containing a stack of buttons
var scroll = new ScrollLayout()
{
    Position = Resolution.ScreenArea.Center,
    Size = new Vector2(400, 500)
};

var stack = new StackLayout()
{
    Alignment = StackAlignment.Top,
    Horizontal = HorizontalAlignment.Center
};

for (int i = 0; i < 20; i++)
{
    var button = new StackLayoutButton();
    button.Size = new Vector2(380, 60);
    button.AddItem(new Label($"Option {i}", Content));
    button.OnClick += (s, e) => { /* handle */ };
    stack.AddItem(button);
}

scroll.AddItem(stack);
AddItem(scroll);
```

## Layout in Buttons

Buttons themselves are layouts. `StackLayoutButton` arranges its content in a stack, while `RelativeLayoutButton` uses relative positioning:

```csharp
// Button with icon and text side by side
var button = new StackLayoutButton()
{
    Size = new Vector2(250, 64)
};
button.Layout.Alignment = StackAlignment.Left;
button.AddItem(new Image(icon) { Size = new Vector2(48, 48) });
button.AddItem(new Shim(8, 0));
button.AddItem(new Label("Settings", Content));
button.OnClick += (s, e) => OpenSettings();
AddItem(button);
```

## Next Steps

- [Widgets](widgets) -- The elements that go inside layouts
- [Styling](styling) -- Customize layout appearance
- [Screens](screens) -- Screens that contain layouts
