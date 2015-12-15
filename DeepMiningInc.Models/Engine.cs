using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using Windows.UI;

using DeepMiningInc.Engine.Numerics;

using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

using Windows.UI.Xaml.Input;

using DeepMiningInc.Engine.Events;

namespace DeepMiningInc.Engine
{
    public sealed class Engine
    {
        private Vector2 _lastMousePos;
        private Vector2 _lastMouseScrollPos;

        public static Engine Current { get; } = new Engine();

        public CoordinateSystem CoordinateSystem { get; set; }

        public IObservable<EarlyUpdateEngineEventArgs> EarlyUpdate => EarlyUpdateSubject;

        public IEnumerable<EngineFeature> Features { get; }

        public Game Game { get; }

        public IObservable<PointerMovedCanvasEventArgs> GlobalPointerMoved { get; private set; }

        public IObservable<PointerWheelChangedCanvasEventArgs> GlobalPointerWheelChanged { get; private set; }

        public IObservable<DrawCanvasEventArgs> Draw { get; private set; }

        public bool IsInitialized { get; private set; }

        private Color _clearColor;

        public Color ClearColor
        {
            get
            {
                return _clearColor;
            }

            set
            {
                _clearColor = value;
                if (Control != null)
                {
                    Control.ClearColor = _clearColor;
                }
            }
        }

        public bool IsPaused => Control?.Paused ?? true;

        public ITextureManager TextureManager { get; } = new TextureManager();

        public IObservable<UpdateEngineEventArgs> Update => UpdateSubject;

        internal CanvasAnimatedControl Control { get; private set; }

        internal Subject<EarlyUpdateEngineEventArgs> EarlyUpdateSubject { get; }

        internal Subject<UpdateEngineEventArgs> UpdateSubject { get; }

        private Engine()
        {
            Features = new List<EngineFeature>();

            // TODO remove debug code
            Game = DebugDataGenerator.CreateGame();

            EarlyUpdateSubject = new Subject<EarlyUpdateEngineEventArgs>();
            UpdateSubject = new Subject<UpdateEngineEventArgs>();
        }

        public void AddFeature(EngineFeature feature)
        {
            (Features as List<EngineFeature>).Add(feature);
            if (IsInitialized)
            {
                feature.Attach(this);
            }
        }

        public void Attach(CanvasAnimatedControl control)
        {
            Control = control;
            Control.Paused = true;

            control.CreateResources += OnCreateResources;
            control.Update += OnUpdate;

            Draw = new Subject<DrawCanvasEventArgs>();
            GlobalPointerMoved = new Subject<PointerMovedCanvasEventArgs>();
            GlobalPointerWheelChanged = new Subject<PointerWheelChangedCanvasEventArgs>();

            control.Draw += (_, __) => (Draw as Subject<DrawCanvasEventArgs>)?.OnNext(new DrawCanvasEventArgs(_, __));

            control.PointerMoved += (_, __) => (GlobalPointerMoved as Subject<PointerMovedCanvasEventArgs>)?.OnNext(new PointerMovedCanvasEventArgs(_ as ICanvasAnimatedControl, __));
            control.PointerWheelChanged += (_, __) => (GlobalPointerWheelChanged as Subject<PointerWheelChangedCanvasEventArgs>)?.OnNext(new PointerWheelChangedCanvasEventArgs(_ as ICanvasAnimatedControl, __));
        }

        public void Pause()
        {
            if (Control == null)
            {
                throw new InvalidOperationException("Cannot stop engine when not attached to AnimatedCanvasControl!");
            }

            Control.Paused = true;
        }

        public void RemoveFeature(EngineFeature feature)
        {
            if (!Features.Contains(feature))
            {
                throw new InvalidOperationException("Cannot remove feature that is not added to this Engine.");
            }

            feature.UnAttach(this);
            (Features as List<EngineFeature>).Remove(feature);
        }

        public void StartOrResume()
        {
            if (Control == null)
            {
                throw new InvalidOperationException("Cannot start engine when not attached to AnimatedCanvasControl!");
            }

            if (!IsInitialized)
            {
                Initialize();
            }

            Control.ClearColor = _clearColor;
            Control.Paused = false;
        }

        private void OnCreateResources(ICanvasAnimatedControl control, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(control).AsAsyncAction());
        }

        private async Task CreateResourcesAsync(ICanvasAnimatedControl control)
        {
            await (TextureManager as TextureManager)?.LoadResourcesAsync(control);
        }

        private void Initialize()
        {
            foreach (var feature in Features)
            {
                feature.Attach(this);
            }

            IsInitialized = true;
        }

        private void OnUpdate(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            EarlyUpdateSubject.OnNext(new EarlyUpdateEngineEventArgs(sender, args));
            UpdateSubject.OnNext(new UpdateEngineEventArgs(sender, args));
        }
    }
}
