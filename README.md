# Proto AshTech Engine
Proto AshTech Engine 

Create a good base engine to then use to port tileworld code over to

* File IO
* Input Handeler
* Game State Management
* Sprite and Texture Atlas Support

## Game State Manager

in your main game class add the following to start the state manager
```c#
GameStateManager gameStateManager;

public MainGame() {
  graphics = new GraphicsDeviceManager(this);
  Content.RootDirectory = "Content";

  //start up the state manager
  gameStateManager = new GameStateManager(this);
  Components.Add(gameStateManager);
}
```
in the Load Content function load any states you have created
```c#
protected override void LoadContent()
{
    // Create a new SpriteBatch, which can be used to draw textures.
    spriteBatch = new SpriteBatch(GraphicsDevice);

    gameStateManager.AddState(new GameStates.TitleState(), "title");
}
```
