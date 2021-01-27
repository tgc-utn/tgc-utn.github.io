/*!
    * Start Bootstrap - Grayscale v6.0.3 (https://startbootstrap.com/theme/grayscale)
    * Copyright 2013-2020 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-grayscale/blob/master/LICENSE)
    */
(function ($) {
"use strict"; // Start of use strict

	$.getJSON("resources/calendar.json", function(json) 
	{
		$('#calendar-container').empty();
		var index = 0;
		for(; index < json.dates.length; index++)
		{
		    parseDate(json.dates[index], index + 1);
		    
	    }
	});
	 



})(jQuery); // End of use strict


parseDate = function(date, id)
{
    var isHoliday = date.holiday != undefined ? ' class="table-secondary" ' : '';
    
	var dateElement = $('<tr' + isHoliday + '><th scope="row">' + id + '</th><td>' + date.date + '</tr>');
    $('#calendar-container').append(dateElement);

    var eventContainer = $('<td></td>');
    dateElement.append(eventContainer);
    
	for(var event in date.events)
		parseEvent(date.events[event], eventContainer);
		
};

parseEvent = function(event, dateElement)
{
    var badge = getBadgeFor(event.type);
    var badgeElement = $('<span class="badge ' + badge + ' mr-2">' + event.type + '</span> ');
    
    if(event.link != undefined)
        var eventElement = $('<a href="' + event.link + '" target="_blank">' + event.name + '</a>');
    else
        var eventElement = $('<p>' + event.name + '</p>');
    
    eventElement.prepend(badgeElement);
	dateElement.append(eventElement);
};

getBadgeFor = function(type)
{
    type = type.toLowerCase();
    type = type.replace(/\s/g, '');
    if(type === "clase")
        return "badge-primary";
    else if(type === "tp")
        return "badge-warning";
    else if(type === "parcial")
        return "badge-success"
    else if(type === "nohayclases")
        return "badge-dark";
    else
        return "badge-secondary";
}