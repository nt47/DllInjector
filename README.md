###  Dll Injector Cheat C++ 
***
<p align="center">
   <img src="https://readme-spotify-status-rho.vercel.app/api/run-spotify-status.py" alt="s4nx Playing Now" width="500" />
<p align="center">


## Information 🌟
**External Game Project primarily written in C++, utilizing external libraries. I'm actively combating scammers while developing various cheats and tools for games, including Hack Cheat Driver Esp Aimbot Magic Bullet, Driver Injector Overlay, and Imgui.**

<details>
<summary><strong>Developed Cheat Hacks for the Following Games🚀</strong></summary>

- Rise Online 🌄
- Apex Legends 🏆
- Bloodhunt 🔍
- Call of Duty: Cold War ☢️
- Call of Duty: Vanguard ⚔️
- Call of Duty: Warzone/MW (Modern Warfare) 🪂
- Dayz 🧟
- Dead By Daylight 🌑
- Destiny 2 ⚔️
- Enlisted 🛡️
- Escape From Tarkov 🏙️
- Fortnite 🛸
- Halo Infinite 🔥
- HyperFlick 🎯
- New Critical Hit 💥
- New World 🌍
- Mir 4 🌌
- Noble 🏰
- Playerunknown's Battlegrounds (PUBG) 🏆
- Steam 🎮
- Rainbow Six Siege 🌈
- Rijin 🐉
- Rogue Company 💣
- Rust 🔧
- Scum 🧟
- Splitgate 🌀
- Super People 🦸
- Unleashed 🌟
- Valorant 🔫
- Spoofer 🔧
- DLL Injector 💉
</details>

**Be careful when purchasing hacking cheats to avoid scams. More projects to come.**

**You can easily employ the mapper by incorporating the compiled binaries into your project. Refer to the provided "Injection.h" header for more comprehensive guidance. Ensure that the compiled binaries are present in your program's working directory. Upon the initial launch, the injection module will download PDB files for both the native version of "ntdll.dll" and, when executed on x64, the wow64 version, to resolve symbol addresses. To initiate the download, use the exported "StartDownload" function. It's important to note that the injector can only operate once the downloads have been completed. The injection module exports "GetSymbolState" and "GetImportState," which will return "INJ_ERROR_SUCCESS (0)" once the PDB download and the resolution of all required addresses are finished. Furthermore, you can utilize "GetDownloadProgress" to track the download's progress as a percentage. If you intend to unload the injection module during the download process, make use of "InterruptDownload," as failing to do so might lead to your process becoming deadlocked due to the DLL.**



![image](https://github.com/nt47/DllInjector/blob/main/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202024-03-20%20205828.png)
## Features
<details>
<summary>Features (Show Details)</summary>

- **AIMBOT** 🔫
  Description: Automatically targets opponents.

- **ESP** 👁️
  Description: Enables you to see opponents even behind walls.

- **SPOOFER** 🛡️
  Description: Helps bypass in-game cheat detection systems.

- **DRIVER** 🚗
  Description: Uses a driver to control in-game processes.

- **INJECTOR** 💉
  Description: Injects cheat code into the game process.

</details>

<details>
<summary>Injection Methods 🧬 (Show Details)</summary>

- **LoadLibraryExW**
  Description: Injects an external DLL file into the target process.

- **LdrLoadDll**
  Description: Loads and associates a DLL file into the target process.

- **LdrpLoadDll**
  Description: Manages DLL loading operations.

- **LdrpLoadDllInternal**
  Description: Manages DLL loading operations as an internal function.

- **ManualMapping**
  Description: Manually loads DLL code into the target process.

</details>

<details>
<summary>Shellcode Execution Methods 🐚 (Show Details)</summary>

- **NtCreateThreadEx**
  Description: Injects code by creating a new thread.

- **Thread Hijacking**
  Description: Injects code by using an existing thread as the target.

- **SetWindowsHookEx**
  Description: Injects code using Windows event hooks.

- **QueueUserAPC**
  Description: Injects user-mode code into a thread.

- **KernelCallback**
  Description: Injects code using kernel callbacks.

- **FakeVEH**
  Description: Injects code by manipulating Virtual Exit Handlers.

</details>

<details>
<summary>Manual Mapping Features 🗺️ (Show Details)</summary>

- **Section Mapping**
  Description: Maps DLL sections into the target process's memory.

- **Base Relocation**
  Description: Places DLL code in the target process's memory appropriately.

- **Imports**
  Description: Properly resolves dependent DLLs.

- **Delayed Imports**
  Description: Handles dependencies in a delayed manner.

- **SEH Support**
  Description: Provides Structured Exception Handler support.

- **TLS Initialization**
  Description: Initializes Thread Local Storage.

- **Security Cookie Initialization**
  Description: Initializes the security cookie.

- **Loader Lock**
  Description: Provides synchronization using the loader lock.

- **Shift Image**
  Description: Injects code by redirecting image loading.

- **Clean Data Directories**
  Description: Cleans data directories, removing unnecessary information.

</details>


## ✨ DONATE Buy Me Coffee

Thanks:https://starcraft3.blizzard.com/en-us/


## Disclaimer 🚧
This project is intended solely for educational purposes. As the creator, I want to emphasize that I do not bear any responsibility for any potential harm or unlawful activities that may arise. My primary objective in undertaking this project was to deepen my understanding of reverse engineering, with no intention to disrupt the gaming experience for fellow enthusiasts. It's important to note that I won't be providing ongoing updates to the offsets due to these reasons. Please understand that the following code may not mirror the exact version used in my hackathon.
