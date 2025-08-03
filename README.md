# Experimental Architectural Pattern for Unity: Root–Logic & View

The **"FeatureRoot"** architectural pattern is used in my projects and is currently under development.

**IMPORTANT:** It is experimental.

This pattern unifies the Root and Logic layers into a single manageable part while separating out the View layer. The structure is not final and will evolve as needed.

During development, it requires a clear separation between Gameplay and UI logic.

- `Root` — the control point of the logic and provides shared information.
- `Logic` — the layer containing system implementations.
- `View` — subscribes to Root events and displays its state.

#### Advantages:
- Clear separation of concerns between logic and visual representation,
- Easy scalability,
- Transparent control point of logic via the Root,
- Uses vContainer for dependency injection and lifecycle management.

#### Disadvantages:
- A lot of code is needed to separate `Root`, `Logic` and `View` for each feature,
- The architectural approach has not yet proven its full value,
- Not suitable for small projects as it is overkill.

# Folder Structure per Scene:

    Gameplay/
    ├── Character/...
    │   └── ...
    ├── GameplayFlow.cs
    └── GameplayScope.cs
    
- `Scope` — a class inheriting from `LifetimeScope`, esponsible for the Register phase (registering dependencies and injecting parameters).
- `Flow` — implements the `IStartable`, interface, acts as the single entry point for the scene and handles initial logic and service startup.
- Inside the folder, there is a folder for features, e.g., "Character/", containing Root, Logic, and View.

# Example of Character Implementation (Gameplay):

    Character/
    ├── Logic/
    │   └── CharacterMovementLogic.cs
    ├── CharacterRoot.cs
    └── CharacterView.cs
    
- `CharacterRoot` — contains character movement logic and initializes it.
- `CharacterMovementLogic` — implements movement, hiding details from the `Root`.
- `CharacterView` — subscribes to Root events and displays its state.
- `GameplayScope` registers `CharacterRoot` with parameters and `CharacterView`.

# UI Components Pattern
In the UI, a set of interfaces and base classes is used to simplify window and screen management.

- `IUIView` — interface with `Show()` and `Hide()` methods.
- `BaseUIView` — abstract class inheriting from MonoBehaviour implementing the `IUIView` interface.
- `BaseScreen<TRoot>` — base class for main UI windows that players switch between.
- `BasePopup<TRoot>` — base class for animated pop-up windows layered on top.
- `BaseHUD<TRoot>` — base class for always visible HUD information.
