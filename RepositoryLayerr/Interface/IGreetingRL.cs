﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayerr.Interface
{
    public interface IGreetingRL
    {
        string GetGreetingMessage(string? firstName, string? lastName);
    }
}
