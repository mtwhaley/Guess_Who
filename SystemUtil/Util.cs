using System.Diagnostics;
using System.Drawing;

namespace SystemUtil {
    class Util {

        private static readonly string[] VIABLE_COLORS = {"brown", "blue", "black", "blonde", "white", "red"};

        public static void ConfigureConsole() {
            Console.Title="Guess Who";
        }

        public static void Write(string text) {
            Console.ForegroundColor=ConsoleColor.Green;
            Console.Write(text);
            Console.ForegroundColor=ConsoleColor.White;

        }

        public static bool IsColor(string possibleColor) {
            if (Array.IndexOf(VIABLE_COLORS,possibleColor.ToLower())!=-1) {
                return true;
            }
            return false;
        }


        public static string ReadYesOrNo(string? repeatQuestion = null) {
            string? response = null;

            while (!Util.IsValidYesOrNo(response)) {
                if (response!=null) {
                    Write("\n\n... I'm not sure what that means...\n");
                    if (repeatQuestion != null) Write(repeatQuestion+"\n");
                }
                Util.Write("\t");
                
                response = Console.ReadLine();

            }
            Util.Write("\n\n");
            return response;

        }
        public static bool IsValidYesOrNo(string? evalString) {
            if (evalString is null) return false;
            if (IsAcceptance(evalString) || IsRejection(evalString)) {
                return true;
            }
            return false;
        }

        
        public static bool HasNegation(string evalString) {
            if (Array.IndexOf(Constants.NEGATIONS, evalString.ToLower())!=-1) {
                return true;
            }
            return false;
        }
        public static bool IsAcceptance(string evalString) {
            if (Array.IndexOf(Constants.ACCEPTANCES, evalString.ToLower())!=-1) {
                return true;
            }
            return false;
        }
        public static bool IsRejection(string evalString) {
            if (Array.IndexOf(Constants.REJECTIONS, evalString.ToLower())!=-1) {
                return true;
            }
            return false;
        }

        public static void OpenPicture() {
            string relativeFilePath = "Images\\GuessWhoCharacters.jpg";
            string fullImagePath = "";

            try {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                string projectRootDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;

                fullImagePath = Path.Combine(projectRootDirectory, relativeFilePath);
            }
            catch (Exception e) {
                Console.WriteLine("An error occured: " + e.Message);
            }

            if (File.Exists(fullImagePath)) {
                try {
                    Process.Start(new ProcessStartInfo(fullImagePath) {UseShellExecute=true});
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            else {
                if (fullImagePath.Length>0) Console.WriteLine("File not found: "+fullImagePath);
                else Console.WriteLine("Error locating image file");
            }
        }
    }
}