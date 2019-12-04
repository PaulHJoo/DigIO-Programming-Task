using System.Collections.Generic;

namespace DigIO_Programming_Task_API.Services
{
    public class LogActivityValidator
    {
        public static List<string> Validate(string activityLine, int lineNumber)
        {
            var errors = new List<string>();
            ValidateLogActivity(activityLine, errors, lineNumber);

            return errors;
        }

        private static void ValidateLogActivity(string activityLine, List<string> errors, int lineNumber)
        {
            var splitActivityLine = GetSplitActivityLine(activityLine);

            if (splitActivityLine.Count < 5)
            {
                errors.Add($"Log on line {lineNumber} is incorrectly formatted.");
                return;
            }

            if (!IsLogFormatValid(splitActivityLine, errors, lineNumber))
            {
                return;
            }

            var ipUserAndDateChunk = splitActivityLine[0];
            if (!IsIpAddressValid(ipUserAndDateChunk, errors, lineNumber))
            {
                return;
            }

            var requestChunk = splitActivityLine[1];
            if (!ValidateRequest(requestChunk, errors, lineNumber))
            {
                return;
            }
        }

        private static List<string> GetSplitActivityLine(string activityLine)
        {
            var splitActivityLine = activityLine
                .Trim()
                .Split("\"");

            var nonEmptySplits = new List<string>();
            foreach (var line in splitActivityLine)
            {
                var trimmedLine = line.Trim();
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    nonEmptySplits.Add(trimmedLine);
                }
            }

            return nonEmptySplits;
        }

        private static bool IsLogFormatValid(List<string> splitActivityLine, List<string> errors, int lineNumber)
        {
            var ipToDateChunk = splitActivityLine[0];
            var splitIpToDateChunk = ipToDateChunk.Split(' ');
            if (splitIpToDateChunk.Length != 5)
            {
                errors.Add($"Log on line {lineNumber} is incorrectly formatted.");
                return false;
            }

            var httpChunk = splitActivityLine[1];
            var splitHttpChunk = httpChunk.Split(' ');
            if (splitHttpChunk.Length != 3)
            {
                errors.Add($"Log on line {lineNumber} is incorrectly formatted.");
                return false;
            }

            return true;
        }

        private static bool IsIpAddressValid(string ipUserAndDateChunk, List<string> errors, int lineNumber)
        {
            var splitIpToDateChunk = ipUserAndDateChunk.Split(' ');
            var ipAddress = splitIpToDateChunk[0];
            var splitIpAddress = ipAddress.Split('.');
            if (splitIpAddress.Length != 4)
            {
                errors.Add($"IP Address on line number {lineNumber} is incorrectly formatted.");
                return false;
            }

            foreach (var identifier in splitIpAddress)
            {
                if (!int.TryParse(identifier, out int value))
                {
                    errors.Add($"IP Address on line number {lineNumber} contains non-numeric characters.");
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateRequest(string requestChunk, List<string> errors, int lineNumber)
        {
            var splitRequestChunk = requestChunk.Split(' ');
            var url = splitRequestChunk[1].ToLower();

            if (url.StartsWith("/") || url.StartsWith("h"))
            {
                return true;
            }

            errors.Add($"URL on line number {lineNumber} is incorrectly formatted.");
            return false;
        }
    }
}
