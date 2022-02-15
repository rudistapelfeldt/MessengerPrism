using System;
using FFImageLoading.Helpers;
using Prism.Logging;
using static System.Diagnostics.Debug;

namespace MessengerPrism.Services
{
    public class DebugLogger : IMiniLogger
    {
        public void Debug(string message) =>
            WriteLine($"Debug: {message}");

        public void Error(string errorMessage) =>
            WriteLine($"Error: {errorMessage}");

        public void Error(string errorMessage, Exception ex) =>
            WriteLine($"Error: {errorMessage}\n{ex.GetType().Name}: {ex}");

    }
}