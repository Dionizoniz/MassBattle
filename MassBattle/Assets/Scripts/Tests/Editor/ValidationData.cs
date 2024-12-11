using System.Text;

namespace MassBattle.Tests.Editor
{
    public class ValidationData
    {
        private readonly StringBuilder _error = new();

        public bool IsValid { get; private set; } = true;
        public string ErrorMessage => _error.ToString();

        public void AddErrorMessage(string errorMessage)
        {
            _error.AppendLine(errorMessage);
            IsValid = false;
        }
    }
}
