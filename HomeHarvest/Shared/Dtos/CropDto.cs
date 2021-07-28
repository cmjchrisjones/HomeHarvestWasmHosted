﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeHarvest.Shared.Dtos
{
	public class CropDto
	{
	
		[JsonInclude]
		public int Id { get; set; }

		/// <summary>
		/// Year in which the crop was planted in
		/// </summary>
		[Required(ErrorMessage = "You must enter a year for the crop being sowed")]
		[JsonInclude]
		public int Year { get; set; }

		[JsonInclude]
		public string Location { get; set; }

		
		public string LocationYear  => $"{Location}, {Year}";

		/// <summary>
		/// Image name of the plot for the current crop
		/// </summary>
		[JsonInclude]
		public string PlotImage { get; set; }


		/// <summary>
		/// Collection of plants that were planted in the crop year
		/// </summary>
		[JsonInclude]
		public virtual ICollection<SowDto> Sowed { get; set; }
	}
}
