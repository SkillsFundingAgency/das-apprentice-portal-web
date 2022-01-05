using System;

namespace SFA.DAS.ApprenticePortal.OuterApi.Mock.Models
{
    public static class Update
    {
        internal static Update<T> With<T>(T? value)
            where T : class 
            => new Update<T> { Value = value };
    }

    public class Update<T> where T : class
    {
        public T? Value { get; set; }
    }
}