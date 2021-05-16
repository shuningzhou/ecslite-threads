// ----------------------------------------------------------------------------
// The MIT License
// Threads support for LeoECS Lite https://github.com/Leopotam/ecslite-threads
// Copyright (c) 2021 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

namespace Leopotam.EcsLite.Threads {
    public abstract class EcsThreadSystem<TThread, T1> : EcsThreadSystemBase
        where TThread : struct, IEcsThread<T1>
        where T1 : struct {
        EcsFilter _filter;
        EcsPool<T1> _pool1;

        public override void Run (EcsSystems systems) {
            if (_filter == null) {
                var world = GetWorld (systems);
                _pool1 = world.GetPool<T1> ();
                _filter = GetFilter (world);
            }
            TThread thread = default;
            var data1 = _pool1.GetRawItems ();
            thread.Init (_filter.GetRawEntities (), data1);
            SetData (systems, ref thread);
            ThreadService.Run (ref thread, _filter.GetEntitiesCount (), GetChunkSize (systems));
        }

        protected virtual void SetData (EcsSystems systems, ref TThread thread) { }
    }

    public abstract class EcsThreadSystem<TThread, T1, T2> : EcsThreadSystemBase
        where TThread : struct, IEcsThread<T1, T2>
        where T1 : struct
        where T2 : struct {
        EcsFilter _filter;
        EcsPool<T1> _pool1;
        EcsPool<T2> _pool2;

        public override void Run (EcsSystems systems) {
            if (_filter == null) {
                var world = GetWorld (systems);
                _pool1 = world.GetPool<T1> ();
                _pool2 = world.GetPool<T2> ();
                _filter = GetFilter (world);
            }
            TThread thread = default;
            thread.Init (
                _filter.GetRawEntities (),
                _pool1.GetRawItems (),
                _pool2.GetRawItems ());
            SetData (systems, ref thread);
            ThreadService.Run (ref thread, _filter.GetEntitiesCount (), GetChunkSize (systems));
        }

        protected virtual void SetData (EcsSystems systems, ref TThread thread) { }
    }

    public abstract class EcsThreadSystem<TThread, T1, T2, T3> : EcsThreadSystemBase
        where TThread : struct, IEcsThread<T1, T2, T3>
        where T1 : struct
        where T2 : struct
        where T3 : struct {
        EcsFilter _filter;
        EcsPool<T1> _pool1;
        EcsPool<T2> _pool2;
        EcsPool<T3> _pool3;

        public override void Run (EcsSystems systems) {
            if (_filter == null) {
                var world = GetWorld (systems);
                _pool1 = world.GetPool<T1> ();
                _pool2 = world.GetPool<T2> ();
                _pool3 = world.GetPool<T3> ();
                _filter = GetFilter (world);
            }
            TThread thread = default;
            thread.Init (
                _filter.GetRawEntities (),
                _pool1.GetRawItems (),
                _pool2.GetRawItems (),
                _pool3.GetRawItems ());
            SetData (systems, ref thread);
            ThreadService.Run (ref thread, _filter.GetEntitiesCount (), GetChunkSize (systems));
        }

        protected virtual void SetData (EcsSystems systems, ref TThread thread) { }
    }

    public abstract class EcsThreadSystem<TThread, T1, T2, T3, T4> : EcsThreadSystemBase
        where TThread : struct, IEcsThread<T1, T2, T3, T4>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct {
        EcsFilter _filter;
        EcsPool<T1> _pool1;
        EcsPool<T2> _pool2;
        EcsPool<T3> _pool3;
        EcsPool<T4> _pool4;

        public override void Run (EcsSystems systems) {
            if (_filter == null) {
                var world = GetWorld (systems);
                _pool1 = world.GetPool<T1> ();
                _pool2 = world.GetPool<T2> ();
                _pool3 = world.GetPool<T3> ();
                _pool4 = world.GetPool<T4> ();
                _filter = GetFilter (world);
            }
            TThread thread = default;
            thread.Init (
                _filter.GetRawEntities (),
                _pool1.GetRawItems (),
                _pool2.GetRawItems (),
                _pool3.GetRawItems (),
                _pool4.GetRawItems ());
            SetData (systems, ref thread);
            ThreadService.Run (ref thread, _filter.GetEntitiesCount (), GetChunkSize (systems));
        }

        protected virtual void SetData (EcsSystems systems, ref TThread thread) { }
    }

    public abstract class EcsThreadSystemBase : IEcsRunSystem {
        public abstract void Run (EcsSystems systems);
        protected abstract int GetChunkSize (EcsSystems systems);
        protected abstract EcsFilter GetFilter (EcsWorld world);
        protected abstract EcsWorld GetWorld (EcsSystems systems);
    }

    public interface IEcsThreadBase {
        void Execute (int fromIndex, int beforeIndex);
    }

    public interface IEcsThread<T1> : IEcsThreadBase
        where T1 : struct {
        void Init (
            int[] entities,
            EcsPool<T1>.PoolItem[] pool);
    }

    public interface IEcsThread<T1, T2> : IEcsThreadBase
        where T1 : struct
        where T2 : struct {
        void Init (
            int[] entities,
            EcsPool<T1>.PoolItem[] pool1,
            EcsPool<T2>.PoolItem[] pool2);
    }

    public interface IEcsThread<T1, T2, T3> : IEcsThreadBase
        where T1 : struct
        where T2 : struct
        where T3 : struct {
        void Init (
            int[] entities,
            EcsPool<T1>.PoolItem[] pool1,
            EcsPool<T2>.PoolItem[] pool2,
            EcsPool<T3>.PoolItem[] pool3);
    }

    public interface IEcsThread<T1, T2, T3, T4> : IEcsThreadBase
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct {
        void Init (
            int[] entities,
            EcsPool<T1>.PoolItem[] pool1,
            EcsPool<T2>.PoolItem[] pool2,
            EcsPool<T3>.PoolItem[] pool3,
            EcsPool<T4>.PoolItem[] pool4);
    }
}