﻿using CleanArchitecture.Core.Featuers.Authorization.Querys.Result;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Core.Mapping.Student //Note That Must be The same name space in StudentProfile

{
    public partial class StudentProfile
    {
        public void GetStudentByIdMapping()
        {
            CreateMap<IdentityRole, GetRolesListResponse>();

        }
    }
}
