using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SFA.DAS.ApprenticePortal.SharedUi.Identity
{
    [Serializable]
    public class InvalidHashedIdException : Exception
    {
        public InvalidHashedIdException(string? hashValue)
            : base($"Invalid hashed ID value '{hashValue}'")
        {
        }

        protected InvalidHashedIdException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
