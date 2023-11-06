INFO OVER PROJECT:
The textures are in MyGame > bin > Debug > net6.0 > Textures
This has been done beacuse Content.mgcb WIL crash the hole framework of MonoGame.
So i just give the .exe my texture by putting it in the same files as de .exe and geting it by TitleContainer.OpenStream($"{path}");
see at DrawClass.cs from line 8 function name: "public static Texture2D GetTexture(string path, SpriteBatch graph)".

-------------------------------
------------Updates------------
-------------------------------
1)06/11	Start up git up (project has already colison, map gerator, and some textures)