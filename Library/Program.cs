using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Pages { get; set; }
    public string Genre { get; set; }
    public string Publisher { get; set; }
    public string ISBN { get; set; }

    public Book(string title, string author, int pages, string genre, string publisher, string isbn)
    {
        Title = title;
        Author = author;
        Pages = pages;
        Genre = genre;
        Publisher = publisher;
        if (ValidateISBN(isbn))
            ISBN = isbn;
        else
            throw new ArgumentException("Invalid ISBN");
    }

    private bool ValidateISBN(string isbn)
    {
        // Regular expression for ISBN validation
        string pattern = @"^\d{3}-\d{1}-\d{2}-\d{5}-\d{1}$";
        return Regex.IsMatch(isbn, pattern);
    }

    public void WriteToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine($"Title: {Title}");
            writer.WriteLine($"Author: {Author}");
            writer.WriteLine($"Pages: {Pages}");
            writer.WriteLine($"Genre: {Genre}");
            writer.WriteLine($"Publisher: {Publisher}");
            writer.WriteLine($"ISBN: {ISBN}");
        }
    }

    public static Book ReadFromFile(string fileName)
    {
        using (StreamReader reader = new StreamReader(fileName))
        {
            string title = reader.ReadLine().Split(": ")[1];
            string author = reader.ReadLine().Split(": ")[1];
            int pages = int.Parse(reader.ReadLine().Split(": ")[1]);
            string genre = reader.ReadLine().Split(": ")[1];
            string publisher = reader.ReadLine().Split(": ")[1];
            string isbn = reader.ReadLine().Split(": ")[1];

            return new Book(title, author, pages, genre, publisher, isbn);
        }
    }
}

class Student
{
    public string Name { get; set; }
    public int Grade { get; set; }

    public Student(string name, int grade)
    {
        Name = name;
        Grade = grade;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a Book from user input
        Book book;
        while (true)
        {
            try
            {
                Console.WriteLine("Enter Book Information:");
                Console.Write("Title: ");
                string title = Console.ReadLine();

                Console.Write("Author: ");
                string author = Console.ReadLine();

                Console.Write("Pages: ");
                int pages = int.Parse(Console.ReadLine());

                Console.Write("Genre: ");
                string genre = Console.ReadLine();

                Console.Write("Publisher: ");
                string publisher = Console.ReadLine();

                Console.Write("ISBN: ");
                string isbn = Console.ReadLine();

                book = new Book(title, author, pages, genre, publisher, isbn);
                break; // Break the loop if book is successfully created
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a valid number for pages.");
            }
        }

        // Writing book information to a file
        book.WriteToFile("book.txt");
        Console.WriteLine("Book information has been written to book.txt");

        // Reading book information from the file
        Book readBook = Book.ReadFromFile("book.txt");

        // Displaying book information
        Console.WriteLine("\nBook Info:");
        Console.WriteLine($"Title: {readBook.Title}");
        Console.WriteLine($"Author: {readBook.Author}");
        Console.WriteLine($"Pages: {readBook.Pages}");
        Console.WriteLine($"Genre: {readBook.Genre}");
        Console.WriteLine($"Publisher: {readBook.Publisher}");
        Console.WriteLine($"ISBN: {readBook.ISBN}");

        // Extract email addresses from a text string
        string text = "Contact us at info@example.com or support@example.org for assistance.";
        List<string> emails = ExtractEmails(text);
        Console.WriteLine("\nExtracted Email Addresses:");
        foreach (string email in emails)
        {
            Console.WriteLine(email);
        }

        // Create a generic dictionary for student data
        Dictionary<string, Student> studentData = new Dictionary<string, Student>();
        studentData["A001"] = new Student("Alice", 85);
        studentData["B002"] = new Student("Bob", 92);

        // Display student data
        Console.WriteLine("\nStudent Data:");
        foreach (var entry in studentData)
        {
            Console.WriteLine($"Student ID: {entry.Key}, Name: {entry.Value.Name}, Grade: {entry.Value.Grade}");
        }
    }

    static List<string> ExtractEmails(string text)
    {
        List<string> emails = new List<string>();
        string pattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b";
        MatchCollection matches = Regex.Matches(text, pattern);
        foreach (Match match in matches)
        {
            emails.Add(match.Value);
        }
        return emails;
    }
}
