using BasicAPI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static BasicAPI.Controllers.BookController;

namespace BasicAPI.Controllers
{
    public class BookController : ApiController
    {
        dbContext dbContext = new dbContext();

        // GET api/values
        //public dictionary<string, string> get()
        //{
        //    dictionary<string, string> booklist = new dictionary<string, string>();
        //    datatable bookdt = dbcontext.getbooks();
        //    if (bookdt != null)
        //    {
        //        foreach (datarow dr in bookdt.rows)
        //        {
        //            booklist.add(dr["name"].tostring(), dr["author"].tostring());
        //        }
        //    }
        //    return booklist;
        //}
        public IHttpActionResult get()
        {
            List<Book> booklist = new List<Book>();
            DataTable bookdt = dbContext.getBooks();
            if (bookdt != null && bookdt.Rows.Count > 0)
            {
                foreach (DataRow dr in bookdt.Rows)
                {
                    Book book = new Book();
                    book.Id = (Int32)dr["Id"];
                    book.Name = dr["Name"].ToString();
                    book.Author = dr["Author"].ToString();
                    booklist.Add(book);
                }
                return Ok(booklist);
            }
            else
            {
                return BadRequest("No Book available at this time");
            }
          
        }
        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            Dictionary<string, string> book = new Dictionary<string, string>();
            DataTable bookDT = dbContext.getBookById(id);
            if (bookDT != null && bookDT.Rows.Count > 0)
            {
                DataRow dr = bookDT.Rows[0];
                book.Add(dr["Name"].ToString(), dr["Author"].ToString());
                return Ok(book);
            }
            else
            {
                return BadRequest("No Book available on this Id");
            }
        }

        // POST api/values
        public IHttpActionResult Post([FromBody] Book book)
        {
            int affactedRow = dbContext.postBook(book);
            if (affactedRow != 0)
            {
                return Ok("Book detail saved !!!");
            }
            else
            {
                return BadRequest("An error occurred while save the book detail.");
            }
        }

        // PUT api/values/5
        public IHttpActionResult Put([FromBody] Book book)
        {
            int affactedRow = dbContext.updateBook(book);
            if (affactedRow != 0)
            {
                return Ok("Book detail updated !!!");
            }
            else
            {
                return BadRequest("An error occurred while save the book detail.");
            }
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            int affactedRow = dbContext.deleteBook(id);
            if (affactedRow != 0)
            {
                return Ok("Book detail deleted !!!");
            }
            else
            {
                return BadRequest("An error occurred while save the book detail.");
            }
        }
    }

    public class Book
    {
        public int Id;
        public string Name;
        public string Author;
    }
}
