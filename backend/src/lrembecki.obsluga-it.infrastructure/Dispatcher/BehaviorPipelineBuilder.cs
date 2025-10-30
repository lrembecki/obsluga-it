using lrembecki.shared.application.Abstractions;

namespace lrembecki.obsluga_it.infrastructure.Dispatcher;

internal class BehaviorPipelineBuilder
{
    private readonly List<Func<BehaviorHandlerDelegate, BehaviorHandlerDelegate>> _components = [];

    public BehaviorPipelineBuilder Use(Func<BehaviorHandlerDelegate, BehaviorHandlerDelegate> middleware)
    {
        _components.Add(middleware);
        return this;
    }

    public BehaviorHandlerDelegate Build(BehaviorHandlerDelegate finalHandler)
    {
        BehaviorHandlerDelegate app = finalHandler;

        for (int i = _components.Count - 1; i >= 0; i--)
        {
            app = _components[i](app);
        }

        return app;
    }
}

