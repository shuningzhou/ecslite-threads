# Threads support for LeoEcsLite C# Entity Component System framework
Threads support for [LeoECS Lite](https://github.com/Leopotam/ecslite).

> Tested on unity 2020.3 (not dependent on it) and contains assembly definition for compiling to separate assembly file for performance reason.

> **Important!** Don't forget to use `DEBUG` builds for development and `RELEASE` builds in production: all internal error checks / exception throwing works only in `DEBUG` builds and eleminated for performance reasons in `RELEASE`.

# Table of content
* [Socials](#socials)
* [Installation](#installation)
    * [As unity module](#as-unity-module)
    * [As source](#as-source)
* [Example](#example)
    * [Component](#component)
    * [ThreadSystem](#threadsystem)
    * [Thread](#thread)
* [License](#license)

# Socials
[![discord](https://img.shields.io/discord/404358247621853185.svg?label=enter%20to%20discord%20server&style=for-the-badge&logo=discord)](https://discord.gg/5GZVde6)

# Installation

## As unity module
This repository can be installed as unity module directly from git url. In this way new line should be added to `Packages/manifest.json`:
```
"com.leopotam.ecslite.threads": "https://github.com/Leopotam/ecslite-threads.git",
```
By default last released version will be used. If you need trunk / developing version then `develop` name of branch should be added after hash:
```
"com.leopotam.ecslite.threads": "https://github.com/Leopotam/ecslite-threads.git#develop",
```

## As source
If you can't / don't want to use unity modules, code can be downloaded as sources archive of required release from [Releases page](`https://github.com/Leopotam/ecslite-threads-unity/releases`).

# Example

## Component
```csharp
struct C1 {
    public int Id;
}
```
## ThreadSystem
```csharp
class TestThreadSystem : EcsThreadSystem<TestThread, C1> {
    protected override int GetChunkSize (EcsSystems systems) {
        return 1000;
    }

    protected override EcsWorld GetWorld (EcsSystems systems) {
        return systems.GetWorld ();
    }
    
    protected override EcsFilter GetFilter (EcsWorld world) {
        return world.Filter<C1> ().End ();
    }

    // Optional additional initialization of job structure.
    protected override void SetData (ref TestThread thread) {
        thread.DeltaTime = Time.deltaTime;
    }
}
```
> **Important!** EcsThreadSystem supports up to 4 component pools.
 
## Thread
```csharp
struct TestThread : IEcsThread<C1> {
    public float DeltaTime;
    int[] _entities;
    EcsPool<C1>.PoolItem[] _pool;

    public void Init (int[] entities, EcsPool<C1>.PoolItem[] pool) {
        _entities = entities;
        _pool = pool;
    }

    public void Execute (int fromIndex, int beforeIndex) {
        for (int i = fromIndex; i < beforeIndex; i++) {
            var e = _entities[i];
            ref var c1 = ref _pool[e].Data;
            c1.Id = (c1.Id + 1) % 10000;
        }
    }
}
```

# License
The software is released under the terms of the [MIT license](./LICENSE.md).

No personal support or any guarantees.
