using Applicaion.Demo.Models;
using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using Our.Umbraco.OpenKeyValue.Core.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.WebApi;

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
			int i = 3;

			ViewBag.SetValue = _service.SetValue($"key_{i}", $"value");
			ViewBag.SetValueUpdated = _service.SetValue($"key_{i}", $"value_set_updated");

			ViewBag.Updated = _service.UpdateValue($"key_{i}", $"value_updated");

			ViewBag.SetValue2 = _service.SetValue($"key_{i}_1000", $"value_1000");
			
			_service.Delete($"key_{i}_1000");

			ViewBag.GetValue = _service.GetValue($"key_{i}");


			return base.Index(model);
		}

	}
}
