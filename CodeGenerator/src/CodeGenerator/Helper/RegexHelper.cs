namespace CodeGenerator.Helper
{
    public class RegexHelper
    {
        public const string NameValidRegex = "^\\b[a-zA-Z0-9_]+\\b$";
        public const string AbsolutePathRegex = "^[a-zA-Z]:\\(((?![<>:\" /\\|?*]).)+((?<![ .])\\)?)*$";
        public const string DirectoryPathRegex = "^(((?![<>:\" /\\|?*]).)+((?<![.])\\)?)*$";
        public const string ProjectNameRegex = "^\\b[a-zA-Z0-9_.]+\\b$";
    }
}
