namespace bookDemo.Data 
{ 

    using bookDemo.Models;


    public static class ApplicationContex
    {

        public static List<Book> Books { get; set; }
        static  ApplicationContex()
        {
        Books = new List<Book>()
        {
            new Book() { Id = 1 , Title= "AhiEvran", Price=200},
            new Book() { Id = 2, Title= "Lal", Price =203},
            new Book() { Id = 3,Title="Aşkın gözyaşları" ,Price=203}
        };

        }
    }
}
