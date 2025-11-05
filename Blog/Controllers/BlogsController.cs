using Blog.Data;
using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IImageService _imageService;

        public BlogsController(BlogDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: api/Blogs (Public - no auth required)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBlogs()
        {
            var blogs = await _context
                .Blogs
                .OrderByDescending(b => b.CreatedAt)
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.ImageUrl1,
                    b.CreatedAt,
                    b.UpdatedAt,
                })
                .ToListAsync();

            return Ok(blogs);
        }

        // GET: api/Blogs/5 (Public - no auth required)
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog.Models.Blog>> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        // POST: api/Blogs (Public - no auth required)
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Blog.Models.Blog>> PostBlog([FromForm] BlogCreateDto blogDto)
        {
            var blog = new Blog.Models.Blog
            {
                Title = blogDto.Title,
                ContentText1 = blogDto.ContentText1,
                ContentText2 = blogDto.ContentText2,
                ContentText3 = blogDto.ContentText3,
                ContentText4 = blogDto.ContentText4,
                ContentText5 = blogDto.ContentText5,
                IsPublished = blogDto.IsPublished,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            if (blogDto.ImageFile1 != null && blogDto.ImageFile1.Length > 0)
            {
                try
                {
                    var fileName = await _imageService.SaveImageAsync(blogDto.ImageFile1);
                    blog.ImageFileName1 = fileName;
                    blog.ImageUrl1 = _imageService.GetImageUrl(fileName);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            if (blogDto.ImageFile2 != null && blogDto.ImageFile2.Length > 0)
            {
                try
                {
                    var fileName = await _imageService.SaveImageAsync(blogDto.ImageFile2);
                    blog.ImageFileName2 = fileName;
                    blog.ImageUrl2 = _imageService.GetImageUrl(fileName);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
        }

        // PUT: api/Blogs/5 (Public - no auth required)
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PutBlog(int id, [FromForm] BlogUpdateDto blogDto)
        {
            if (id != blogDto.Id)
            {
                return BadRequest();
            }

            var existingBlog = await _context.Blogs.FindAsync(id);
            if (existingBlog == null)
            {
                return NotFound();
            }

            existingBlog.Title = blogDto.Title;
            existingBlog.ContentText1 = blogDto.ContentText1;
            existingBlog.ContentText2 = blogDto.ContentText2;
            existingBlog.ContentText3 = blogDto.ContentText3;
            existingBlog.ContentText4 = blogDto.ContentText4;
            existingBlog.ContentText5 = blogDto.ContentText5;
            existingBlog.IsPublished = blogDto.IsPublished;
            existingBlog.UpdatedAt = DateTime.UtcNow;

            if (blogDto.RemoveImage1 && !string.IsNullOrEmpty(existingBlog.ImageFileName1))
            {
                _imageService.DeleteImage(existingBlog.ImageFileName1);
                existingBlog.ImageFileName1 = null;
                existingBlog.ImageUrl1 = null;
            }

            if (blogDto.ImageFile1 != null && blogDto.ImageFile1.Length > 0)
            {
                if (!string.IsNullOrEmpty(existingBlog.ImageFileName1))
                {
                    _imageService.DeleteImage(existingBlog.ImageFileName1);
                }

                try
                {
                    var fileName = await _imageService.SaveImageAsync(blogDto.ImageFile1);
                    existingBlog.ImageFileName1 = fileName;
                    existingBlog.ImageUrl1 = _imageService.GetImageUrl(fileName);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            if (blogDto.RemoveImage2 && !string.IsNullOrEmpty(existingBlog.ImageFileName2))
            {
                _imageService.DeleteImage(existingBlog.ImageFileName2);
                existingBlog.ImageFileName2 = null;
                existingBlog.ImageUrl2 = null;
            }

            if (blogDto.ImageFile2 != null && blogDto.ImageFile2.Length > 0)
            {
                if (!string.IsNullOrEmpty(existingBlog.ImageFileName2))
                {
                    _imageService.DeleteImage(existingBlog.ImageFileName2);
                }

                try
                {
                    var fileName = await _imageService.SaveImageAsync(blogDto.ImageFile2);
                    existingBlog.ImageFileName2 = fileName;
                    existingBlog.ImageUrl2 = _imageService.GetImageUrl(fileName);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Blogs/5 (Public - no auth required)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(blog.ImageFileName1))
            {
                _imageService.DeleteImage(blog.ImageFileName1);
            }

            if (!string.IsNullOrEmpty(blog.ImageFileName2))
            {
                _imageService.DeleteImage(blog.ImageFileName2);
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
