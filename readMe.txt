INFO OVER PROJECT:
The textures are in MyGame > bin > Debug > net6.0 > Textures
This has been done beacuse Content.mgcb WIL crash the hole framework of MonoGame.
So i just give the .exe my texture by putting it in the same files as de .exe and geting it by TitleContainer.OpenStream($"{path}");
see at DrawClass.cs from line 8 function name: "public static Texture2D GetTexture(string path, SpriteBatch graph)".

-------------------------------
------------Updates------------
-------------------------------
1)06/11	Start up git up (project has already colison, map gerator, and some textures)
2)06/11 Fixed the creating of a chunk where you move a chunk by y it woud shift 1 back from its row. 
3)13/11 Player has textures(stand,move,jump,fall)
Player has slowing down efect for movment and colions with rects by (my Colision class that use self made Vertics).
4)added moving platfroms and some test chunks.
5) fix that player woud face true platforms.
And change the set up of using player. Player is now a 'Creature' and he is in the same List as Creature. 
6) 20/11 Fix 98% of coliosn isues and fix up Creature class. 
7) 27/11 Added a enemy(rotot) and a attack to the enemy. (but gone be replaced with AttackCreature and become a child of that)
8) 27/11 Player has attacks + video in videoCurantState(change per video)
8) 05/12 Added Spikes as a enemy. stabs the player and has coldown when it stables.🔪🔪🔪🔪
And fix weard isues with colision.RectInRect(r1,r2);