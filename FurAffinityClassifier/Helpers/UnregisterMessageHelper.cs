using System;
using CommunityToolkit.Mvvm.Messaging;

namespace FurAffinityClassifier.Helpers
{
    /// <summary>
    /// Message購読解除用Helper
    /// </summary>
    public class UnregisterMessageHelper
    {
        /// <summary>
        /// Messageの購読を解除する
        /// </summary>
        /// <param name="sender">呼び出し元</param>
        /// <param name="e">イベントパラメーター</param>
        public void Unregister(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unregister 1");
            WeakReferenceMessenger.Default.UnregisterAll(sender);
            System.Diagnostics.Debug.WriteLine("Unregister 2");
        }
    }
}
