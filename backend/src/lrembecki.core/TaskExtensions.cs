namespace lrembecki.core;

public static class TaskExtensions
{
    public static Task<TDto> With<TDto>(this Task<TDto> task, Func<TDto, Task<TDto>> func)
        => task.ContinueWith(_ => func(_.Result)).Unwrap();
}
