namespace MarketProject.CustomerService.Common
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure { get => !IsSuccess; }
        public ResultError Error { get; private set; } = ResultError.Create();

        protected Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        protected Result(bool isSuccess, ResultError error)
        {
            /*if (isSuccess && error != null)
                throw new InvalidOperationException();

            if (!isSuccess && error == null)
                throw new InvalidOperationException();*/

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value, true);
        }

        public static Result Failure(ResultError error)
        {
            return new Result(false, error);
        }

        public static Result<T> Failure<T>(ResultError error)
        {
            return new Result<T>(default, false, error);
        }

        public static Result<T> Failure<T>(string errorMessage)
        {
            return new Result<T>(default, false, errorMessage);
        }

        /// <summary>
        /// It process an action and returns <see cref="Success"/> or <see cref="Failure"/>
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public static Result Process(Action act)
        {
            try
            {
                act();

                return Success();
            }
            catch (Exception e)
            {
                var error = ResultError.Create(e.Message, e);
                return Failure(error);
            }
        }

        /// <summary>
        /// It process a method and returns <see cref="Success{T}(T)"/> or <see cref="Failure{T}(ResultError)"/>
        /// </summary>
        /// <typeparam name="Tout"></typeparam>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Result<Tout> Process<Tout>(Delegate method, params object[] args) where Tout : notnull
        {
            try
            {
                if (method is null)
                    throw new InvalidOperationException();

                var result = method.DynamicInvoke(args);

                if (result is null)
                    throw new InvalidOperationException();

                var res = (Tout)result;

                return Success(res);
            }
            catch (Exception e)
            {
                var error = ResultError.Create();

                if (e.InnerException is not null)
                    error = ResultError.Create(e.InnerException.Message, e.InnerException);
                
                return Failure<Tout>(error);
            }
        }
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException(Error.Message);

                return _value;
            }
        }

        public Result(T value, bool isSuccess)
            : base(isSuccess)
        {
            _value = value;
        }

        public Result(T value, bool isSuccess, ResultError error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public Result(T value, bool isSuccess, string errorMessage)
            : base(isSuccess, ResultError.Create(errorMessage))
        {
            _value = value;
        }
    }
}
