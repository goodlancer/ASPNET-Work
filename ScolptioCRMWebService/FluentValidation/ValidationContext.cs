namespace FluentValidation
{
    internal class ValidationContext
    {
        private object request;

        public ValidationContext(object request)
        {
            this.request = request;
        }
    }
}
