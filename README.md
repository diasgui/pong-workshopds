## Requisitos
Backend:
* docker
* postgrees
* redis
* elixir

Frontend:
* Unity

### Instalação
#### Ubuntu / Debian / Mint

Unity Hub: https://public-cdn.cloud.unity3d.com/hub/prod/UnityHub.AppImage

Backend: sudo apt-get install docker docker.io pgcli redis-tools elixir

### Arch

[...]

### Windows

Unity Hub: https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.exe

Backend: [...]

### MacOs

Unity Hub: https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.dmg

Backend: [...]

## Backend-Client Interface
* [ ] create
* [ ] authenicate
* [ ] changeName
* [ ] learderboard
* [ ] findmatch
* [ ] matchEnded

## Backend Database
* [ ] Players

## Game UI
* [ ] Menu
* [ ] Leaderboard
* [ ] Waiting Match
* [ ] Loading screen

## Game Code
* [ ] AppDelegate
* [ ] GameInitializer
* [ ] ViewControllerFactory
* [ ] View (abs)
* [ ] Controller (abs)
* [ ] MatchFactory
* [ ] Gameplay
* [ ] PlayerClient
* [ ] PlayerCache

# References
* SimpleJSON: A lib to parse json text (https://github.com/Bunny83/SimpleJSON).
We use this lib to parse our requests responses on client side.
* Anti-if: A design pattern that helps with code maintenance (https://code.joejag.com/2016/anti-if-the-missing-patterns.html).
We adopt this design pattern and show some use cases on this application.

# TODO
* [ ] Use `UnitWebRequest` instead of `WWW`
