using Diploma.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.Models.ViewModels
{
    public class RolesDetailViewModel
    {
        public ApplicationRole Role { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public RolesDetailViewModel()
        {

        }

        public RolesDetailViewModel(ApplicationRole role,List<ApplicationUser> users)
        {
            Role = role;
            Users = users;
        }
    }
}