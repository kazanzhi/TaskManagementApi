namespace TaskManagementApi.Common
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }

        public ServiceResponse(T Data, string Message, bool Status)
        {
            this.Data = Data;
            this.Message = Message;
            this.Status = Status;
        }
    }
}
