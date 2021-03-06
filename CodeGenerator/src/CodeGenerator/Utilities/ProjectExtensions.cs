﻿using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Utilities
{
    public static class ProjectExtensions
    {
        public static Project GetProject(this IEnumerable<Project> projects, string projectPath)
        {
            return projects.Single(x => x.FullPath.Contains(projectPath));
        }
    }
}
