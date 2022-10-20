using Microsoft.VisualBasic;
using VideOde.Core;
using VideOde.Data.Providers;

namespace VideOde.Terminal
{
    public class ClipsArea
    {
        public static void Handle(IClipService clipService, string? verb, string? arg1)
        {
            if (verb == "list")
            {
                IEnumerable<string> printableRows = new List<string>();

                if (arg1 == null)
                {
                    TabularData<Clip> table = GetData(clipService, -1);
                    printableRows = table.GetPrintableRows(separator: "|");
                }
                else if (int.TryParse(arg1, out int requestedCount))
                {
                    if (requestedCount > 0)
                    {
                        TabularData<Clip> table = GetData(clipService, requestedCount);
                        printableRows = table.GetPrintableRows();
                    }
                    else
                    {
                        Console.WriteLine("Cannot get negative number or no clips!");
                    }
                }
                else
                {
                    Console.WriteLine("Number argument invalid!");
                }

                foreach (string row in printableRows)
                {
                    Console.WriteLine(row);
                }
            }
            else if (verb == "get")
            {
                if (int.TryParse(arg1, out int requestedNumber))
                {
                    if (requestedNumber <= 0)
                    {
                        Console.WriteLine("Cannot get negative index clip!");
                        return;
                    }

                    TabularData<Clip> data = GetData(clipService, requestedNumber);

                    IEnumerable<string> rowResult = data.GetPrintableRow(requestedNumber - 1);

                    foreach (string row in rowResult)
                    {
                        Console.WriteLine(row);
                    }
                }
                else
                {
                    Console.WriteLine("Must provide a number argument to \"get\"!");
                }
            }
            else if (verb == "count")
            {
                Console.WriteLine($"There are {clipService.GetClipCount()} clips in the database");
            }
            else
            {
                Console.WriteLine("Command not recognized!");
                return;
            }
        }

        private static TabularData<Clip> GetData(IClipService clipService, int digitResult)
        {
            const string defaultTableValue = "(no data)";

            IEnumerable<Clip> clips;

            if (digitResult <= 0)
            {
                clips = clipService.GetAllClips();
            }
            else
            {
                clips = clipService.GetClips(digitResult);
            }

            IEnumerable<IEnumerable<string>> cols = new List<IEnumerable<string>>()
            {
                clips.Select(clip => clip.Title),
                clips.Select(clip => clip?.Description ?? defaultTableValue),
                clips.Select(clip => clip.StartDate?.ToString() ?? defaultTableValue),
                clips.Select(clip => clip.EndDate?.ToString() ?? defaultTableValue),
                clips.Select(clip => clip.Length.ToString())
            };

            IEnumerable<string> headings = new List<string>()
            {
                nameof(Clip.Title),
                nameof(Clip.Description),
                nameof(Clip.StartDate),
                nameof(Clip.EndDate),
                nameof(Clip.Length)
            };

            var table = new TabularData<Clip>(clips, cols, headings);
            return table;
        }

    }
}
