namespace Square.Api.Services.Contracts
{
    public interface ISquareService
    {
        Task<List<Models.Square>> GetSquaresAsync();
    }
}
