using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etudiant.Classes
   {
    // Extension class for ISynchronizeInvoke interface
    public static class ISynchronizeInvokeExtensions
    {
        // Method to safely append or modify controls from a different thread
        public static void Append<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            // Check if invoking from a different thread is required
            if (@this.InvokeRequired)
            {
                // If so, invoke the action on the correct thread
                @this.Invoke(action, new object[] { @this });
            }
            else
            {
                 // If the action can be directly executed on the current thread, execute it
                action(@this);
            }
        }
    }
}
