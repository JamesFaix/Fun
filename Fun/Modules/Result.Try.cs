﻿using System;
using System.Threading.Tasks;

namespace Fun
{
    public static partial class Result
    {
        /// <summary>
        /// Calls <paramref name="generator"/> and returns <c>Some(x)</c> where <c>x</c> is the returned value of <paramref name="generator"/>. 
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static Result<T> Try<T>(Func<T> generator)
        {
            if (Equals(generator, null))
                return Error<T>(new ArgumentNullException(nameof(generator)));

            try
            {
                return Value(generator());
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        /// <summary>
        /// Calls <paramref name="generator"/> and returns the result.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static Result<T> Try<T>(Func<Result<T>> generator)
        {
            if (Equals(generator, null))
                return Error<T>(new ArgumentNullException(nameof(generator)));

            try
            {
                return generator();
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        /// <summary>
        /// Calls <paramref name="action"/> and returns <c>Some(Unit)</c>.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static Result<Unit> Try(Action action)
        {
            if (Equals(action, null))
                return Error<Unit>(new ArgumentNullException(nameof(action)));

            try
            {
                action();
                return Value(Unit.Value);
            }
            catch (Exception e)
            {
                return Error<Unit>(e);
            }
        }

        /// <summary>
        /// Awaits <paramref name="task"/>, and then returns <c>Some(x)</c> where <c>x</c> is the result of <paramref name="task"/>.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static async Task<Result<T>> Try<T>(Task<T> task)
        {
            if (Equals(task, null))
                return Error<T>(new ArgumentNullException(nameof(task)));

            try
            {
                return Value(await task);
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        /// <summary>
        /// Awaits the task created by <paramref name="generator"/>, and then returns <c>Some(x)</c> where <c>x</c>
        /// is the result of the task.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static async Task<Result<T>> TryAsync<T>(Func<Task<T>> generator)
        {
            if (Equals(generator, null))
                return Error<T>(new ArgumentNullException(nameof(generator)));

            try
            {
                return Value(await generator());
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        /// <summary>
        /// Awaits the task created by <paramref name="generator"/>, and then returns the result.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static async Task<Result<T>> TryAsync<T>(Func<Task<Result<T>>> generator)
        {
            if (Equals(generator, null))
                return Error<T>(new ArgumentNullException(nameof(generator)));

            try
            {
                return await generator();
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        /// <summary>
        /// Awaits <paramref name="task"/>, and then returns <c>Some(Unit)</c>.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static async Task<Result<Unit>> TryAsync(Task task)
        {
            if (Equals(task, null))
                return Error<Unit>(new ArgumentNullException(nameof(task)));

            try
            {
                await task;
                return Value(Unit.Value);
            }
            catch (Exception e)
            {
                return Error<Unit>(e);
            }
        }

        /// <summary>
        /// Awaits the task created by <paramref name="generator"/>, and then returns <c>Some(Unit)</c>.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static async Task<Result<Unit>> TryAsync(Func<Task> generator)
        {
            if (Equals(generator, null))
                return Error<Unit>(new ArgumentNullException(nameof(generator)));

            try
            {
                await generator();
                return Value(Unit.Value);
            }
            catch (Exception e)
            {
                return Error<Unit>(e);
            }
        }

        public static Task<Result<T>> TryAsync<T>(Func<T> generator)
        {
            return TryAsync(async () =>
            {
                var result = await Task.Run(generator);
                return Value(result);
            });
        }

        public static Task<Result<Unit>> TryAsync(Action action)
        {
            return TryAsync(async () =>
            {
                await Task.Run(action);
                return Value(Unit.Value);
            });
        }
    }
}
