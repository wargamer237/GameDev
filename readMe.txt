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
9) added all of the enemys
10) 02/01 working prototype. No screen between transistions screens.

----------------------------------------------------------------------
WHAT I NEEDED TO DO
----------------------------------------------------------------------
SOLO GAME DEV PROJECT
```
VEREIST: ( alles moet tenminstens er in zitten)
github link invite 	: done
Start scherm 		: done
Animatie/spites		: done
2 levels uit start cherm: done
    - moelijks graad	: not realy got same map with harder set up.
3 verschilende vijanden  : done
key inputs		 : done //also mouse input for menu
death screen		 : done // Player has animations then jump back to menu 
Basic physics		 : REALY DONE // UHM BASIC.. xd
    - collisions
    - Acceleratie en momentum van held
Basic AI: 		 : done
    - elke enemy ander AI
Sprites and TileSet + Background : done
    - niet gekleurde blokjes	 
Minstens 2 Design Patterns  	 :I think I got it
Toepassen van SOLID		 :I think I got it
    - Extra folder om klassen in te plaatsen
    - Minstens 3 bewijzen van SOLID gebruik
Minstens 2 uitgezerte exra's	 :moving platform
		I GOT: Moving platforms, Camara movment "with matrix"
    - powerups
    - fog of war
    - bos hp bar
    - levens visuwaliseren
    - helper
    - bounce efects
double code verminderen		:done //could be better but i did oke here (creatures base.function), MyUtils files
look and feel			: Got hard and easter level but SAME MAP and a test map.
    - uitwerking van minimale vereisten
    - uiterken van meer levels
geluid/muziek/efects 		: done // go 1 sound and that when you win To show that i can do it but (the momogame file prolem)
Verder OOD			: done // ye Creatures, Blocks
----------------------------------------------
INLEVEREN:
YT VIDEO MAX 10min NO VID NO POINTS
github link
code zipen
Stukje document waar je SOLID hebt gebruikt
```