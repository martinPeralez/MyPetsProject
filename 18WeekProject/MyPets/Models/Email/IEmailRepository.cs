using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public interface IEmailRepository
    {
        public void Send(string to, string subject, string body);
    }
}
