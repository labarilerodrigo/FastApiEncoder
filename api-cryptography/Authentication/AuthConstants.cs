﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastApiEncoder.Authentication
{
    public static class DataProtectorConstants {
        public const string ApiKeySectionName = "Authentication:ApiKey";
        public const string ApiKeyHeaderName = "X-Api-Key";

    }
}
