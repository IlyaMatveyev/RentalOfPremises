namespace RentalOfPremises.Application.DTOs.Pagination
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; } = 0;    //Всего записей
        public int PageNumder { get; set; } = 0;    //Номер страницы
        public int PageSize { get; set; } = 0;      //Кол-во записей на странице
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);  //Кол-во страниц
    }
}
