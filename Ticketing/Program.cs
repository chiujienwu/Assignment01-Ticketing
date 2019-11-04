using System;
using System.IO;


namespace Ticketing
{
    class Program
    {
        enum ticketStatus
        {
            Open = 0,
            Closed = 1
        } // test - github commits

        enum ticketPriority
        {
            Low = 0,
            Medium = 1,
            High = 2
        }

        /*
        * file name = tickets.csv saved at runtime to C:\Users\JerryChiu\source\repos\Ticketing\Ticketing\bin\Debug
        * TicketID, Summary, Status, Priority, Submitter, Assigned, Watching
        * 1,This is a bug ticket,Open,High,Drew Kjell, Jane Doe,Drew Kjell| John Smith | Bill Jones
        * 
        * future proof:  should introduce ArrayList ticketList = new ArrayList() to container collection of tickets
        */       

        static void Main(string[] args)
        {
            // ticketID should switch to more dynamic assignment - see other notes below
            int ticketID;
            // maxID to track the last ticketID value Int16
            int maxID = 0;
            int ticketCount = 0;
            string ticketSummary;
            /*String ticketStatus;
             *String ticketPriority;
             *set as enums
             */
            string submittedBy;
            string assginedTo;
            string watching;

            // this assumes the existance of a file but does not automatically check for it
            // instead from the main menu, it prompts user
            string file = "tickets.csv";

            // start of main menu
            string choice;

            do
            {
                // ask user menu input
                Console.WriteLine("1) Read data file named : " + file);
                Console.WriteLine("2) Add record.");
                Console.WriteLine("3) Save data file and exit program.");
                Console.WriteLine("Enter any other key to exit.");
                // input response
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    if (File.Exists(file))
                    {
                        // read data from file
                        StreamReader sr = new StreamReader(file);
                        while (!sr.EndOfStream)
                        {
                            //start to read data file
                            string line = sr.ReadLine();
                            // convert string to array
                            string[] arr = line.Split('|');

                            // display to console array data from file
                            // array index: 0-TicketID, 1-Summary, 2-Status, 3-Priority, 4-Submitter, 5-Assigned, 6-Watching
                            Console.WriteLine("TicketID: {0}, Summary: {1}, Status: {2}, Priority: {3}, Submitter: {4}, Assigned: {5}, Watching: {6},", 
                                arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);

                            int readID = Convert.ToInt32(arr[0]);

                            if (readID > maxID) { maxID = readID; }

                            ticketCount = ticketCount++;
                        }
                        sr.Close();
                    }
                    else
                    {
                        Console.WriteLine("File does not exist");
                        Console.WriteLine("Creating an empty file");
                        StreamWriter sw = new StreamWriter(file);
                        ticketCount = 0;
                        sw.Close();
                    }

                } // end of menu option 1 routine

                else if (choice == "2")

                {
                    // We are appending additional records on the fly to the file
                    // therefore, no need for an array to hold additional records in memory before
                    // writing to file

                    string resp;  // to capture user responses

                    do
                    {
                        // ask a question
                        Console.WriteLine("Enter a ticket (Y/N)?");
                        // input the response
                        resp = Console.ReadLine().ToUpper();
                        // if the response is anything other than "Y", stop asking
                        if (resp != "Y") { break; }

                        // prompt for ticketID
                        ticketID = maxID + 2;
                        Console.WriteLine($"Creating a new ticket under Ticket ID : {ticketID}");

                        // prompt for ticket summary
                        Console.WriteLine("Enter ticket summary: ");
                        // save the ticket summary to variable
                        ticketSummary = Console.ReadLine();

                        // prompt for ticket status
                        Console.WriteLine("Enter ticket status: ");
                        // save the ticket status to variable
                        string ticketStatus = Console.ReadLine();

                        // prompt for ticket priority
                        Console.WriteLine("Enter ticket priority: ");
                        // save the ticket status to variable
                        string ticketPriority = Console.ReadLine();

                        // prompt for submittedBy
                        Console.WriteLine("Enter ticket submitter's full name: ");
                        // save the ticket status to variable
                        submittedBy = Console.ReadLine();

                        // prompt for assginedTo
                        Console.WriteLine("Enter full name ticket is to be assigned: ");
                        // save the ticket status to variable
                        assginedTo = Console.ReadLine();

                        // prompt for watching
                        Console.WriteLine("Enter full name of person watching ticket: ");
                        // save the ticket status to variable
                        watching = Console.ReadLine();

                        /*TicketID, Summary, Status, Priority, Submitter, Assigned, Watching
                         1,This is a bug ticket,Open,High,Drew Kjell, Jane Doe,Drew Kjell| John Smith | Bill Jones*/

                        StreamWriter sw = new StreamWriter(file, append: true);

                        sw.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                            ticketID, ticketSummary, ticketStatus, ticketPriority, submittedBy, assginedTo, watching);

                        sw.Close();

                    } while (resp != "N"); // do while loop for option 2 to continue adding records

                }
            } while (choice == "1" || choice == "2");  // do while loop for main menu; any other option exits

        }

    }
}

