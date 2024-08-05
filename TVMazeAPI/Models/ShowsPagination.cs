using System;
namespace TVMazeAPI.Models
{
	public class ShowsPagination
	{	
		public List<Show>? Shows { get; set; }

		public int TotalPage { get; set; }

		public int CurrentPage { get; set; }

		public int? PreviousPage { get; set; }

		public int? NextPage { get; set; }

	}
}

