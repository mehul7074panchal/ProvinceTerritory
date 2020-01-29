using PT.Model.CommonModelLib;
using System;
using System.Text;

namespace PT.Model
{
	[ModelAttribute("tblState", true)]
	public class tblState : IModel
	{
		[ModelPropertyAttribute("StateID", "identity-select-delete-update")]
		public int? StateID { get; set; }

		[ModelPropertyAttribute("Name", "insert-select-update")]
		public string Name { get; set; }

		[ModelPropertyAttribute("CreatedBy", "insert-select-update")]
		public int? CreatedBy { get; set; }

		[ModelPropertyAttribute("CreatedOnDate", "insert-select-update")]
		public DateTime? CreatedOnDate { get; set; }

		[ModelPropertyAttribute("UpdatedBy", "insert-select-update")]
		public int? UpdatedBy { get; set; }

		[ModelPropertyAttribute("UpdatedOnDate", "insert-select-update")]
		public DateTime? UpdatedOnDate { get; set; }

		[ModelPropertyAttribute("IsDeleted", "insert-select-update")]
		public bool? IsDeleted { get; set; }

		[ModelPropertyAttribute("CountryID", "insert-select-update")]
		public int? CountryID { get; set; }

	}
}
