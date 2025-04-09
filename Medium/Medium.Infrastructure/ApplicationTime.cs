using Medium.Domain;

namespace Medium.Infrastructure
{
    public class ApplicationTime : IApplicationTime
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
