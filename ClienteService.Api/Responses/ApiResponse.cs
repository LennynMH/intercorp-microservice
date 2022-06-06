namespace ClienteService.Api.Responses
{
    public class ApiResponse<T>
    {
        #region PROPERTYS
        public T Data { get; set; }
        #endregion

        #region CONSTRUCTOR
        public ApiResponse(T data)
        {
            Data = data;
        }
        #endregion
    }
}
