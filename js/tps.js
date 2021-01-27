/*!
    * Start Bootstrap - Grayscale v6.0.3 (https://startbootstrap.com/theme/grayscale)
    * Copyright 2013-2020 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-grayscale/blob/master/LICENSE)
    */
    

(function ($) {
"use strict"; // Start of use strict

	$.getJSON("resources/tps.json", function(json) 
	{
		$('#tps-container').empty();
		
		for(var year in json.years)
		    parseYear(json.years[year], year);
		    
        
        $('.collapse').on('show.bs.collapse', function () 
        {
            var collapseID = $(this).attr('collapse-id');
            showYear(json.years[collapseID], $(this).find('.card-body'));
        });
        
        
        $('.collapse').on('hide.bs.collapse', function () 
        {
            $(this).find('.card-body').empty();    
        });
	});
	 
    
    
    
    


})(jQuery); // End of use strict


parseYear = function(year, id)
{
    var collapseName = 'collapsed' + id;
	var cardElement = $(
      '<div class="card">' +
	    '<div class="card-header" id="card-heading-' + id + '">' +
	        '<h5 class="mb-0">' +
                '<button class="btn btn-link collapsed" data-toggle="collapse" data-target="#' + collapseName + '" aria-expanded="true" aria-controls="' + collapseName + '">' +
                    year.name +
                '</button>' + 
            '</h5>' +
	    '</div>' +
	  '</div>');
	  
	var cardContent = $(
	    '<div id="' + collapseName + '" class="collapse" aria-labelledby="card-heading-' + id + '" data-parent="#tps-container" collapse-id="' + id + '">' + 
          
        '</div>');
    
    var cardBody = $('<div class="card-body"></div>');
        
    cardContent.append(cardBody);
    cardElement.append(cardContent);      
	$('#tps-container').append(cardElement);
};

showYear = function(year, parentElement)
{   
    var count = year.tps.length;
	var index = 1;
	for(; index < count; index++)
		parseTP(year.tps[index - 1], parentElement);

	parseTP(year.tps[count - 1], parentElement, false);
};

parseTP = function(tp, parentElement, bar = true)
{
    var container = $('<div class="ml-4 mb-4"></div>');
    var row = $('<div class="row"></div>');
    var heading = $('<h3 class="mb-3">' + tp.name + '-' + tp.type + '</h3>');
    
    row.append(heading);
    container.append(row);
    
    var contentRow = $('<div class="row"></div>');
    for(var media in tp.media)
        contentRow.append(parseMedia(tp.media[media]));
    
    container.append(contentRow);
    
    parentElement.append(container);
    
	if(bar)
		parentElement.append('<hr>');
};

parseMedia = function(media)
{
    var keys = Object.keys(media);
    if(keys[0] === "video")  
        return $('<iframe width="425" height="355" class="col-6" src="' + media.video + '" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>');
    else if(keys[0] == 'img')
        return $('<img class="col-6" src="' + media.img +'"/>');
};
