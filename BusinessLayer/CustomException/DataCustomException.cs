namespace BusinessLayer.CustomException
{
    public class DataCustomException:Exception
    {
        public DataCustomException(string message , Exception ex)
            : base(message,ex) { }
        public DataCustomException(string message)
            : base(message) { }
    }
}
