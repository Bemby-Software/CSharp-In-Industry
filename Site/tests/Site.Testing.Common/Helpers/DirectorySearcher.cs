using System;
using System.IO;

namespace Site.Testing.Common.Helpers
{
    public static class DirectorySearcher
    {
        public static string SearchForFullPath(string path, string suffix = "")
        {
            var current = new DirectoryInfo(Directory.GetCurrentDirectory());
            string fullPath = null;
            bool succesful = false;

            while (!succesful)
            {
                var candidate = current.Parent;

                if (candidate is null)
                    throw new ArgumentException("No path found");


                var fullCandidatePath = Path.Combine(candidate.FullName, path, suffix);
                
                if (Directory.Exists(fullCandidatePath))
                {
                    succesful = true;
                    fullPath = fullCandidatePath;
                }

                current = candidate;
            }

            return fullPath;
        }
    }
}