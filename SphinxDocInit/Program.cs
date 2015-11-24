using System;
using System.IO;
using System.Diagnostics;

namespace SphinxDocInit
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var path = args[0];
            Console.WriteLine("Creating documentation in directory " + path);

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Creating directory " + path + "...");
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(Path.Combine(path, "conf.py")))
            {
                Console.WriteLine("We need to create a base template using sphinx-quickstart...");

                // We need to run sphinx-quickstart.
                var startInfo = new ProcessStartInfo();
                startInfo.FileName = "sphinx-quickstart";
                startInfo.WorkingDirectory = path;
                var process = Process.Start(startInfo);
                process.WaitForExit();

                if (!File.Exists(Path.Combine(path, "conf.py")))
                {
                    Console.WriteLine("sphinx-quickstart did not create a template successfully, or was not found on your PATH.");
                }
            }

            string conf;
            using (var reader = new StreamReader(Path.Combine(path, "conf.py")))
            {
                conf = reader.ReadToEnd();
            }
            conf = conf.Replace(
                "#sys.path.insert(0, os.path.abspath('.'))",
                "sys.path.insert(0, os.path.abspath('./_ext'))");
            conf = conf.Replace(
                "extensions = []",
                "extensions = [\"netxml\"]");
            conf = conf.Replace("html_theme = 'alabaster'", @"on_rtd = os.environ.get('READTHEDOCS', None) == 'True'
if on_rtd:
  html_theme = 'default'
else:
  import sphinx_rtd_theme
  html_theme = ""sphinx_rtd_theme""
  html_theme_path = [sphinx_rtd_theme.get_html_theme_path()]".Replace("\r\n", "\n"));
            using (var writer = new StreamWriter(Path.Combine(path, "conf.py")))
            {
                writer.Write(conf);
            }

            var manifestStream1 = typeof(Program).Assembly.GetManifestResourceStream("SphinxDocInit.Resources._ext.netxml.py");
            Directory.CreateDirectory(Path.Combine(path, "_ext"));
            using (var stream = new FileStream(Path.Combine(path, "_ext", "netxml.py"), FileMode.Create))
            {
                manifestStream1.CopyTo(stream);
            }

            manifestStream1 = typeof(Program).Assembly.GetManifestResourceStream("SphinxDocInit.Resources.autobuild.sh");
            using (var stream = new FileStream(Path.Combine(path, "autobuild.sh"), FileMode.Create))
            {
                manifestStream1.CopyTo(stream);
            }

            if (Path.DirectorySeparatorChar == '/')
            {
                var startInfo = new ProcessStartInfo();
                startInfo.FileName = "chmod";
                startInfo.Arguments = "a+x autobuild.sh";
                startInfo.WorkingDirectory = path;
                var process = Process.Start(startInfo);
                process.WaitForExit();
            }

            Console.WriteLine("Documentation has been initialised.");
            Console.WriteLine("You MUST now place one of the .combined.xml files in the root of the documentation folder!");
        }
    }
}

