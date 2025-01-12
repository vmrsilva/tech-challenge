namespace TechChallange.Api.Response
{
    public record BaseResponsePagedDto<T> : BaseResponseDto<T>
    {
        public int TotalPages
        {
            get => CurrentPage == 0 ? 0 : (int)Math.Ceiling((decimal)TotalItems / (TotalItems == 0 ? 1 : (decimal)CurrentPage));
        }
        public int TotalItems { get; init; }
        public int CurrentPage { get; init; }
    }
}
