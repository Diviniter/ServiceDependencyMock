﻿using System;

namespace YourApplication.Contexts.ExternalService
{
    [Serializable]
    public class GetMockContext
    {
        public string SessionId;

        public override int GetHashCode()
        {
            return 17874;
        }

        public override bool Equals(object obj)
        {
            var context = (GetMockContext)obj;
            return this.SessionId == context.SessionId;
        }
    }
}
