namespace MarketProject.CustomerService.Domain
{
    public class ResultError : Exception
    {
        private ResultError(){ }

        private ResultError(string message)
            : base(message)
        {

        }

        private ResultError(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public static ResultError Create()
        {
            return new ResultError();
        }

        public static ResultError Create(string message)
        {
            return new ResultError(message);
        }
        
        public static ResultError Create(string message, Exception innerException)
        {
            return new ResultError(message, innerException);
        }
    }
}
