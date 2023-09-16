using Kysect.CommonLib.BaseTypes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kysect.TerminalUserInterface.ProgressTracking;

public class CollectionProgressTracker<T>
{
    private readonly IProgressTrackerFactory _progressTrackerFactory;

    public IReadOnlyCollection<T> Values { get; }

    public CollectionProgressTracker(IProgressTrackerFactory progressTrackerFactory, IReadOnlyCollection<T> values)
    {
        _progressTrackerFactory = progressTrackerFactory;
        Values = values;
    }

    public CollectionProgressTracker<TResult> Select<TResult>(string operation, Func<T, TResult> selector)
    {
        operation.ThrowIfNull();
        selector.ThrowIfNull();

        var result = new List<TResult>(Values.Count);

        using IProgressTracker progressTracker = _progressTrackerFactory.Create(operation, Values.Count);

        foreach (T value in Values)
        {
            TResult modified = selector(value);
            result.Add(modified);
            progressTracker.OnUpdate();
        }

        return new CollectionProgressTracker<TResult>(_progressTrackerFactory, result);
    }

    public CollectionProgressTracker<TResult> SelectParallel<TResult>(string operation, Func<T, TResult> selector)
    {
        using IProgressTracker progressTracker = _progressTrackerFactory.Create(operation, Values.Count);

        var results = Values
            .AsParallel()
            .Select(v =>
            {
                TResult modified = selector(v);
                progressTracker.OnUpdate();
                return modified;

            })
            .ToList();

        return new CollectionProgressTracker<TResult>(_progressTrackerFactory, results);
    }

    public CollectionProgressTracker<TResult> SelectMany<TResult>(string operation, Func<T, IReadOnlyCollection<TResult>> selector)
    {
        operation.ThrowIfNull();
        selector.ThrowIfNull();

        var result = new List<TResult>(Values.Count);

        using (IProgressTracker progressTracker = _progressTrackerFactory.Create(operation, Values.Count))
        {
            foreach (T value in Values)
            {
                result.AddRange(selector(value));
                progressTracker.OnUpdate();
            }
        }

        return new CollectionProgressTracker<TResult>(_progressTrackerFactory, result);
    }

    public CollectionProgressTracker<TResult> SelectManyParallel<TResult>(string operation, Func<T, IReadOnlyCollection<TResult>> selector)
    {
        using IProgressTracker progressTracker = _progressTrackerFactory.Create(operation, Values.Count);

        var results = Values
            .AsParallel()
            .SelectMany(v =>
            {
                IReadOnlyCollection<TResult> modified = selector(v);
                progressTracker.OnUpdate();
                return modified;
            })
            .ToList();

        return new CollectionProgressTracker<TResult>(_progressTrackerFactory, results);
    }

    public CollectionProgressTracker<T> Where(string operation, Predicate<T> predicate)
    {
        operation.ThrowIfNull();
        predicate.ThrowIfNull();

        var result = new List<T>();

        using (IProgressTracker progressTracker = _progressTrackerFactory.Create(operation, Values.Count))
        {
            foreach (T value in Values)
            {
                if (predicate(value))
                    result.Add(value);
                progressTracker.OnUpdate();
            }
        }

        return new CollectionProgressTracker<T>(_progressTrackerFactory, result);
    }

    public CollectionProgressTracker<T> ApplyParallel(string operation, Action<T> applicator)
    {
        using IProgressTracker progressTracker = _progressTrackerFactory.Create(operation, Values.Count);

        var results = Values
            .AsParallel()
            .Select(v =>
            {
                applicator(v);
                progressTracker.OnUpdate();
                return v;
            })
            .ToList();

        return new CollectionProgressTracker<T>(_progressTrackerFactory, results);
    }
}