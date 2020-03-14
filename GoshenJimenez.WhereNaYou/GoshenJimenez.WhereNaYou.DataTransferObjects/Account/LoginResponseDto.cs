using System;
using System.Collections.Generic;
using System.Text;

namespace GoshenJimenez.WhereNaYou.DataTransferObjects.Account
{
    public class LoginResponseDto
    {
        public Guid? Id { get; set; }

        public string UserName { get; set; }

        //public string SessionToken { get; set; }
    }
}
