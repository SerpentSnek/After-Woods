# After Woods #

## Summary ##

After Woods is a 2D side-view platformer focused around exploration and survival. Set in a post-apocalyptic wasteland, After Woods follows the journey of [PLACEHOLDER] trying to find their way home. While on a routine supply run by traveling through the underground caverns, [PLACEHOLDER] feels like they are being watched. Suddenly, they hear footsteps approaching them at an alarming rate, belonging to a mysterious, threatening beast. Panicking, [PLACEHOLDER] runs as fast as they can away from the beast and after a long chase, finally evades it by escaping to the surface. But [PLACEHOLDER] cannot stay there because soon, the radiation poisoning will overtake them. Even worse, looking around, [PLACEHOLDER] realizes that they have never seen this place before. Can you guide [PLACEHOLDER] home, avoiding the beast, mutated creatures, and death from radiation?

## Project Resources

[Web-playable version of your game.](https://itch.io/)  
[Trailor](https://youtube.com)  
[Press Kit](https://dopresskit.com/)  
[Proposal: make your own copy of the linked doc.](https://docs.google.com/document/d/1qwWCpMwKJGOLQ-rRJt8G8zisCa2XHFhv6zSWars0eWM/edit?usp=sharing)  

## Gameplay Explanation ##

**In this section, explain how the game should be played. Treat this as a manual within a game. Explaining the button mappings and the most optimal gameplay strategy is encouraged.**


**Add it here if you did work that should be factored into your grade but does not fit easily into the proscribed roles! Please include links to resources and descriptions of game-related material that does not fit into roles here.**

# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least four such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The game's background consists of procedurally generated terrain produced with Perlin noise. The game can modify this terrain at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## Producer

**Describe the steps you took in your role as producer. Typical items include group scheduling mechanisms, links to meeting notes, descriptions of team logistics problems with their resolution, project organization tools (e.g., timelines, dependency/task tracking, Gantt charts, etc.), and repository management methodology.**

## User Interface and Input

**Describe your user interface and how it relates to gameplay. This can be done via the template.**
**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

## Animation and Visuals

**List your assets, including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Game Logic

**Document the game states and game data you managed and the design patterns you used to complete your task.**

# Sub-Roles

## Audio - Esther Cheng

### Background Music

For the soundtrack of the game, since I don't have much experience with making my own music and don't have the creativity to do so, I relied on finding copyright free songs on YouTube. When trying to figure out what the general direction for music was for the game, I asked for feedback from my group members and we decided on a more atmospheric, melachnoic vibe of the game. The main inspiration for the soundtrack was the soundtrack for `Rain World` as it fit perfectly with the type of survival game we were going for. 

**Main Menu**

[`Lost Memories by Ghostrifter`](https://youtu.be/GTxsbUzHgcA?si=vsM2N83MzWeuuMj7) is the song I chose for the main menu because the track not only fits the lo-fi, electronic sound we were looking for but it has the right amount of excitement and tenseness to keep the player on the edge of their seat when they're waiting to start the game.

**Overall Game**

The following tracks were picked to play over each of the stages:

[`Solitude by Ghostrifter`](http://bit.ly/ghostrifter-yt)

Creative Commons — Attribution-NoDerivs 3.0 Unported — CC BY-ND 3.0

Free Download: https://hypeddit.com/hbqndr


[`Neon Drive by Ghostrifter`](http://bit.ly/ghostrifter-yt)

Creative Commons — Attribution-NoDerivs 3.0 Unported — CC BY-ND 3.0

Free Download: https://hypeddit.com/pto3rz


[`Soon We'll Fly by Ghostrifter`](http://bit.ly/ghostrifter-yt)

Creative Commons — Attribution-NoDerivs 3.0 Unported — CC BY-ND 3.0

Free Download: https://hypeddit.com/r6dqhh


[`Twilight Voyage by Ghostrifter`](http://bit.ly/ghostrifter-yt)

Creative Commons — Attribution-NoDerivs 3.0 Unported — CC BY-ND 3.0

Free Download: https://hypeddit.com/ahl2eq

After discovering the artist, Ghostrifter, I found that their sound fit perfectly with the game as most of what they dabble in is either lo-fi or synthwave, which is the main reason why I chose to revolve the soundtrack around their songs. Since we envisioned the game to be something that a player would want to cozy up to, the tracks had the right amount of calmness to them as well as a lot of suspense to match the survival aspect.

### Sound Effects

[`Monster Footsteps Sound effects | No Copyright`](https://youtu.be/UM7VjF_FIwM?si=xR0REuBLpwTAibt7) by *Film Masters* - Stomping for the beast

The rest of the sound effects in the game were found on [Pixabay](https://pixabay.com/), a site with royalty-free sound effects usable in projects. Here is the [content license summary](https://pixabay.com/service/license-summary/).

I made sure to find sounds that were realistic and could be easily envisioned with the animation of the game.

The sound implementation mostly consisted of sorting sounds into 5 categories: main menu background music, background music for the stages, player sounds, mob sounds, and beast sounds. Then I created a SoundManager prefab for each category and assigning all sounds under the category as a sound component. Depending on what sound it was adding, I would locate and proceed to call the sound through playing the corresponding sound's array index in its sound manager. This gave the me an easier way to find and add in audio, since I would not have to spend time counting Audio Source components to find the indexing of each sound. The exceptions were sounds that were always played upon use, where I opted to directly add the SoundManager prefab to the particular scene as the Audio Source componenets are set to "Play on Awake."

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**

## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**

## Game Feel and Polish

**Document what you added to and how you tweaked your game to improve its game feel.**