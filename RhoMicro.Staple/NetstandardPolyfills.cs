// SPDX-License-Identifier: MPL-2.0

#if NETSTANDARD2_0

namespace RhoMicro.Staple;

internal static class NetstandardPolyfills
{
    extension(File)
    {
        public static async Task<String> ReadAllTextAsync(String path, CancellationToken ct)
        {
            using var fs = File.OpenRead(path);
            using var reader = new StreamReader(fs);
            var result = await reader.ReadToEndAsync().WaitAsync(ct);

            return result;
        }
    }

    public static async Task<T> WaitAsync<T>(this Task<T> task, CancellationToken ct)
    {
        var cancelTaskSource = new TaskCompletionSource<Object?>();
        ct.Register(() => cancelTaskSource.TrySetResult(null));

        await Task.WhenAny(
            cancelTaskSource.Task,
            task);

        ct.ThrowIfCancellationRequested();

        return task.Result;
    }
}

#endif
