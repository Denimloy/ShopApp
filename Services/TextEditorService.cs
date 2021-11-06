using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Services
{
    public class TextEditorService
    {
        public StringBuilder CapitalizeEachWord(string someString)
        {
            string[] stringArray = someString.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            StringBuilder editedString = new StringBuilder(someString.Length + 1);

            foreach (var item in stringArray)
            {
                editedString.Append(item.Substring(0, 1).ToUpper() + item.Substring(1, item.Length - 1) + " ");
            }
            return editedString.Remove(editedString.Length - 1, 1);
        }

    }
}
