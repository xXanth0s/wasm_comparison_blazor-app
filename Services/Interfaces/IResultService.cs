using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services.Interfaces
{
    public interface IResultService
    {

        public void SaveResult(Result result);
    }
}
