using Our.Umbraco.OpenKeyValue.Core.Services;
using System;
using System.Web.Mvc;
using Umbraco.Web.Models;

namespace Application.Demo.Controllers
{
	public class HomeController : Umbraco.Web.Mvc.RenderMvcController
	{
		private IOpenKeyValueService _service;

		public HomeController(IOpenKeyValueService service)
		{
			_service = service;
		}

		public override ActionResult Index(ContentModel model)
		{
			var key = $"{DateTime.UtcNow.Ticks}_key";
			var value = "value";

			// insert
			var item = _service.Set(key, value);

			// exists
			bool exists = _service.Exists(key);

			// get 
			var retrievedItem = _service.Get(key);

			// update
			var updatedItem = _service.Set(key, $"{value}_updated");

			// delete
			_service.Delete(key);

			// get deleted item
			var deletedItem = _service.Get(key);

			ViewBag.SetItem = item;
			ViewBag.Exists = exists;
			ViewBag.RetrievedItem = retrievedItem;
			ViewBag.UpdatedItem = updatedItem;
			ViewBag.DeletedItem = deletedItem;

			return base.Index(model);
		}
	}
}
