namespace al.performancemanagement.DAL.Helpers
{
    public partial class Result<T> : Result
    {
        public Result()
            : base()
        {

        }

        public Result(T model)
            : base()
        {
            this.Model = model;
        }

        public Result(string message)
            : base(message)
        {
        }

        public T Model { get; set; }
    }

    public partial class Result
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public string ResultType { get; set; }
        public string ResultCode { get; set; }

        public Result(string message)
        {
            Successful = false;
            Message = message;
            ResultCode = ErrorCodes.General_Data_Error;
        }
        public Result()
        {
            Successful = true;
            Message = string.Empty;
            ResultCode = ErrorCodes.No_Error;
        }
    }
}
