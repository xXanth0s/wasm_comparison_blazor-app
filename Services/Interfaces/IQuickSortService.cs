namespace BlazorApp.Services.Interfaces
{
    public interface IQuickSortService<T>
    {
        public void sort(ref T[] array, string key);
    }
}
