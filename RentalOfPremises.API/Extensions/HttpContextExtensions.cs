namespace RentalOfPremises.API.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Метод извлечения UserId из клеймов
        /// </summary>
        /// <param name="context"></param>
        /// <returns name="userId">В случае успешного извлечения возвращает userId типа Guid. В ином случае Guid.Empty</returns>
        public static Guid GetUserId(this HttpContext context)
        {
            var userIdClaim = context.User.FindFirst("userId");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Guid.Empty;
            }
            return userId;
        }
    }
}
