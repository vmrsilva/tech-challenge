namespace TechChallange.Api.Response
{
    public record BaseResponsePagedDto<T> : BaseResponseDto<T>
    {
        public int TotalPages
        {
            get => CurrentTake == 0 ? 0 : (int)Math.Ceiling((decimal)TotalItems / (TotalItems == 0 ? 1 : (decimal)CurrentTake));
        }
        public int TotalItems { get; init; }
        public int CurrentTake { get; init; }
    }
}
