## Inspiration


## What it does
VR-FilmMaker allows the user to create "cameras" to film any 3D scene. The user can define keyframes for each camera, which they will automatically interpolate between, or create a handheld shot where the user has complete control over the position and orientation of the camera. Multiple cameras can be created at once, and the user is able to specify which camera is active at any given time to determine the order that the clips will appear in for the final product. The user can freely navigate forward and backward in time, either by individual keyframes or by scrubbing. 

## How I built it
This project was built in Unity using C# and SteamVR. Animations were created using Blender. 

## Challenges I ran into
[https://steamcommunity.com/games/250820/announcements/detail/1696059027982397407]Recently, SteamVR decided to do a complete revamp on their input system. Though made to be more supportive of more modern controls, and supposedly becoming their new 2.0 version, this meant that their plugin and libraries went from fully supporting handheld controllers to completely deprecating the branch. Additionally, much of the documentation online had been done with the previous versions, requiring us to attempt to learn the format by scratch. Luckily, our objectives with the API were not too involved, so we got through such an obstacle with simple hard work!

However, this was not the last challenge we'd face as we met one that was out of our control: importing 3D animation projects into Unity. Using my idle computer back at my dorm, it took an ENTIRE 30 HOURS to export the project as a Unity compatible file and even more after. Nevertheless, we were able to make due with our own amateur test animation, and we were able to keep working on our ideas, easily being able to use our same project on any animation! (Once done loading of course.)

## Accomplishments that I'm proud of
We managed to accomplish quite a few goals the application was built for. We managed to import an animation, build cameras and be able to modify their position during recording, and make a smooth extrapolation of a smooth path between two timeframes, and it is possible to move forwards and backwards in time at the speed desired and view the recorded timeframes and pathways of the cameras.

## What I learned
For many of us, this was our first time ever using C# and Unity, and though development was sometimes painfully slow, I'm glad that we managed to pick it up to the extent that we were all able to contribute in some way to the development process. That being said, code organisation and documentation were issues that severely impacted our efficiency in making the application, and most of our development process was spent on fixing bugs that stemmed from not fully understanding how a lot of our code functioned. Better communication and spending more time planning how software will work will be key for future projects. 

## What's next for VR-FilmMaker
In the future, we hope to expand and improve the UI and user experience by allowing more complex animations to be imported and ran, as well as making transitions smoother and allow the user to use the application with a better frame rate. Moreover, we wish to implement more features we originally intended to include, such as a curved extrapolation between keyframes, running multiple cameras at once and allowing switching between each one, and displaying playback from each camera's POV within the application.
