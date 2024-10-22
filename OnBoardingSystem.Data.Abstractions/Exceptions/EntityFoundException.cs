//-----------------------------------------------------------------------
// <copyright file="EntityFoundException.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Abstractions.Exceptions
{
    /// <summary>
    /// Exception when an Entity is unexpectedly found.
    /// </summary>
    public class EntityFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFoundException"/> class.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        public EntityFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFoundException"/> class.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        /// <param name="innerException">Inner Exception.</param>
        public EntityFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private EntityFoundException()
        {
        }
    }
}
