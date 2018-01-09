using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinOrg
{
    class InputLanguageHelper
    {
        static InputLanguage _arabicInput;
        static InputLanguage _englishInput;


        public InputLanguageHelper()
        {
            _arabicInput = GetInputLanguageByName("arabic");
            _englishInput = GetInputLanguageByName("english");
        }

        public void SetKeyboardLayout(InputLanguage layout)
        {
            InputLanguage.CurrentInputLanguage = layout;
        }


        public static InputLanguage GetInputLanguageByName(string inputName)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName.ToLower().StartsWith(inputName))
                    return lang;
            }
            return null;
        }

        public static void LoadArabicKeyboardLayout(string lang)
        {
            InputLanguage.CurrentInputLanguage = GetInputLanguageByName("arabic");
            return;
            if (_arabicInput != null)
                InputLanguage.CurrentInputLanguage = _arabicInput;
            else
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
        }

        public static void LoadEnglishKeyboardLayout(string lang)
        {
            InputLanguage.CurrentInputLanguage = GetInputLanguageByName(lang);
            return;
            if (_englishInput != null)
                InputLanguage.CurrentInputLanguage = _englishInput;
            else
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
        }

    }
}
