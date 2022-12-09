# Knights_Nemesis
Top Down, Grid Movement, and Turn Based game
Inspired by Pokemon Mystery Dungeon and built in Unity
Semester Project for CS 4700


Contributions:
  Andrew- 
    Player and enemy health script and bars: Created health script of a maximum HP and universal damage and heal functions,  later added to the UnitController by Seth.  Applied sprite for health and created slider and fill box to denote the player's HP.  
    Boss room and a final boss stage: used different assets for tilemap and different attack functions.
      created arrow system that launches continuous arrows across the map during the boss fight
    Level 4 - designed and implemented maze route 
    Player animations and design: located dark knight icon to use as portrait for player, and found a top-down sprite sheet that had the shape of a knight, and drew over the spritesheet to fit our corrupted knight design.  Created and implemented animations for the cardinal directions of the player, including running animations for the left and right.  
    Music and attack SFX: chose and added audio system to use the tracks I felt fit the theme of the game.  
    Damage UI - created universal damage numbers UI that spawn when the player or enemy receives damage from any source.  
    
    
    
  Dylan-
    
  Ju-Hoon-
    
  Seth-
    Motion System- Uses seperate game objects called movepoints to ensure that movement is gridbased, while also allowing for constant speeds.
    Unit Hierarchies- Designed the player and NPCs to be consistent, all have similar attributes (like health and certain methods). Player is given control over most components individually, and enemies were given their options in a method rather than update, while allows for basic AI to be created and adjusted between different characters. Also the structure that most characters prefabs follow, to make things consistent and easy to work with.
    Turnbased gameplay- Implemented the timer, and lists that allow for units to move individually, while also allowing grace to speed up gameplay when enemies are not around.
    Level 3- Designed the level, and added some extra components such as a mini boss room with specific components specific
    Seth Enemy- Edited my basic enemy to create a new type of enemy. This enemy only moves every other turn, and its move changes based on it's health. Also work towards some basic animation for the sprite
    Mini Boss Enemy- Created a designated room for an enemy boss, gave it a new moveset with RNG and healing, something the other enemies don't have. Balanced it for that point in the game, and made sure active strategy is required. Starting and finishing the battle effect the map, and unlock a new move that the player keeps unlocked between levels.
    Balancing and XP- Allowed the player the maintain stats between levels which allows for XP to be maintained. Therefore I created an XP system which allows for proper scaling through out the game, incentivizing advancing to further levels for higher XP gains, while making the game harder by spawning harder enemies and increasing their health.
    Passive NPC- Created an NPC that moves randomly throughout certain levels. These NPCs are connected to the spawners on each level, killing one will reward the player with a lot of XP, but make the game harder by increasing spawnrates and the maximum enemies per floor.
