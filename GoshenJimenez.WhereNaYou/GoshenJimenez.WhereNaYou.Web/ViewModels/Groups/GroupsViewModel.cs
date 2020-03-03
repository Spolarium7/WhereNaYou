using GoshenJimenez.WhereNaYou.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoshenJimenez.WhereNaYou.Web.ViewModels.Groups
{
    public class GroupsViewModel
    {
        public Page<GroupViewModel> Groups {get;set;}
    }
}
