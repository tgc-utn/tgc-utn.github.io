/*!
    * Start Bootstrap - Grayscale v6.0.3 (https://startbootstrap.com/theme/grayscale)
    * Copyright 2013-2020 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-grayscale/blob/master/LICENSE)
    */
(function ($) {
"use strict"; // Start of use strict

	$.getJSON("resources/resources.json", function(json) 
	{
		$('#resources-container').empty();
		for(var category in json.categories)
			  parseCategory(json.categories[category]);
	});
	 



})(jQuery); // End of use strict


parseCategory = function(category)
{
	  var categoryElement = $('<div class="resources-section mb-5"><h3 class="mb-4">' + category.name + '</h3></div>');
	  $('#resources-container').append(categoryElement);

	  var count = category.links.length;
	  var index = 1;
	  for(; index < count; index++)
		parseLink(category.links[index - 1], categoryElement, index);

	parseLink(category.links[count - 1], categoryElement, index, false);
};

parseLink = function(link, category, id, bar = true)
{
	 
	var createdLink = $('<div class="resource"><h6><a href="' + link.link + '" target="_blank"> [' + id.toString() + '] ' + link.name + '</a></h6></div>');

	category.append(createdLink);

	if(link.description != undefined)
		createdLink.append('<small>' + link.description + '</small>');
			
	var hasAuthor = link.author != undefined;
	var hasTags = link.tags != undefined;
	if(hasAuthor || hasTags)
	{
		var info = $('<p class="lead"></p>');
		createdLink.append(info);
		if(hasAuthor)
		{
			info.append('<i class="fa fa-user inline-icon"></i> por <a href="' + link.author.link + '" target="_blank">' + link.author.name + '</a>');
			if(hasTags)
				info.append('|');
		}
		if(hasTags)
		{
			info.append('<i class="fa fa-tags inline-icon"></i> Tags: ');
			for(var tagIndex in link.tags)               
				info.append('<a href=""><span class="badge badge-info mr-1">' + link.tags[tagIndex] + '</span></a>');
		}
	}		
	if(bar)
		category.append('<hr>');
};