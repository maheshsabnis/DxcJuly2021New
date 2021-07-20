using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Tuples
{
	public class Projects
	{
		public int TaskId { get; set; }
		public string TaskName { get; set; }
		public string AsignedTo { get; set; }
		public bool Status { get; set; }
	}

	public class ProjectTasks : List<Projects>
	{
		public ProjectTasks()
		{
			Add(new Projects() 
			{
			    TaskId=1, TaskName="Write User Stories", AsignedTo = "Mahesh", Status=true
			});
			Add(new Projects()
			{
				TaskId = 2,
				TaskName = "Verify User Stories",
				AsignedTo = "Manish",
				Status = false
			});
			Add(new Projects()
			{
				TaskId = 3,
				TaskName = "Define Test Cases",
				AsignedTo = "Vikram",
				Status = true
			});
			Add(new Projects()
			{
				TaskId = 4,
				TaskName = "Define Testing ENvironment",
				AsignedTo = "Suprotim",
				Status = true
			});
			Add(new Projects()
			{
				TaskId = 6,
				TaskName = "Define Development ENvironment",
				AsignedTo = "Subodh",
				Status = false
			});
		}
	}
}
