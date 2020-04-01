< !DOCTYPE html >

    <html lang="en">
        <head>
            <!-- These scripts and styles should be on cdns and cached, but replace with local lookups if desired. -->
        <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
            <script src="https://momentjs.com/downloads/moment.min.js"></script>
            <link rel="stylesheet" href="https://www.w3.org/StyleSheets/Core/Modernist" type="text/css">

                <!--
        <script src="js/jquery-3.4.1.min.js"></script>
                <script src="js/moment.min.js"></script>
                <link rel="stylesheet" href="css/w3_core_Modernist.css" type="text/css">
                    -->
            
        <script type="text/javascript">
                        var legistar_client_name = 'oklahomacounty';
            var events_url = `https://webapi.legistar.com/v1/${legistar_client_name}/events`;
            
            var GetEventData = function () {
                // We want to only request meetings from this morning at midnight until X days from now.
                // Use moment to get date math and formatting correct
​
                var today = moment();
                        var today_string = today.format("YYYY-MM-DD");
        
                        var today_plus_days = moment(today).add(7, 'days'); // Defaulting to a week
                        var today_plus_days_string = today_plus_days.format("YYYY-MM-DD");
        
                        // These are ODATA arguments; more info on http://webapi.legistar.com/Home/Examples
                var events_url_parameters = {
                            '$top': '10', // Displays will likely have a vertical limit to avoid scrolling - adjust here to limit data
                    '$filter': `EventDate ge datetime'${today_string}' and EventDate lt datetime'${today_plus_days_string}'`,
                        '$orderby': 'EventDate asc',
                    };
    
                    console.log('Fetching upcoming events...');
    
                GetJsonpData(events_url, events_url_parameters, function (data) {
                    var num_events = data.length;
                       
                    console.log(`Found ${num_events} upcoming events.`);
    
                        if(num_events > 0)
                    {
                        var events = [];

                        $.each(data, function (i, event) {
                            var event_body = event['EventBodyName'];
                        var event_location = event['EventLocation'];

                        // Legistar WebAPI sends the date and the time as two separate fields
                        // Making one datetime out of them for display and sorting
                        var event_date = moment(event['EventDate']);
                        var event_time = event['EventTime']

                        var time_parts = event_time.split(/[ :]+/);
                        var hour = parseInt(time_parts[0]);
                        var hour_24 = time_parts[2].toUpperCase() === 'PM' ? hour + 12 : hour;
                        var hour_24_string = hour_24 >= 10 ? hour_24 : '0' + hour_24;
                            var time_24_string = `${hour_24_string}:${time_parts[1]}:00`;
                            var event_datetime = moment(`${event_date.format('YYYY-MM-DD')} ${time_24_string}`);

                        // Make an event data object to encapsulate both raw and calculated fields
                            var event = {
                            event_body: event_body,
                        event_location: event_location,
                        event_date: event_date.format('LL'),
                        event_time: event_time,
                        event_datetime: event_datetime.format('LLLL'),
                        event_datetime_sortable: event_datetime.format()
                    };

                    events.push(event);
                });

                // Sort the objects by the sortable datetime (so 1 pm comes after 11 am)
                events.sort(CompareObjectsByKey('event_datetime_sortable'));

                // Use a plain, unordered list with a class for styling later
                        var event_list = $('<ul></ul>').addClass('event_list');

                        $.each(events, function (i, event) {
                            var event_list_item = $('<li></li>').addClass('event');

                        event_list_item.addClass(i % 2 == 0 ? 'even' : 'odd');

                        // Make an html object per propery with classes so they can be styled later
                            var event_body_div = $('<div></div>').addClass('event_property').addClass('EventBodyName');
                        event_body_div.text(event.event_body);
                        event_list_item.append(event_body_div);

                            var event_datetime_div = $('<div></div>').addClass('event_property').addClass('EventDateTime');
                        event_datetime_div.text(event.event_datetime);
                        event_list_item.append(event_datetime_div);

                            var event_location_div = $('<div></div>').addClass('event_property').addClass('EventLocation');
                        event_location_div.text(event.event_location);
                        event_list_item.append(event_location_div);

                        event_list.append(event_list_item);
                    });

                    $('#content').empty();
                    $('#content').append(event_list);
                }
                else
                    {
                            $('#content').html('<span class="content-message error">No upcoming events.</span>');
                    }
                })
            };

            // jQuery ajax call to get jsonp data asyncronously and pass it to a callback function
            var GetJsonpData = function (api_url, parameters, callback) {
                            $.ajax({
                                url: api_url,
                                dataType: "jsonp",
                                data: parameters
                            })
                                .done(function(data) {
                                    callback(data);
                                })
                                .fail(function(jqxhr, textStatus, error) {
                                    var error_messages = [textStatus, error];
                                    if (typeof jqxhr.responseText != 'undefined') {
                                        error_messages.push(jqxhr.responseText);
                                    }
                                    console.log("Request Failed: \n" + error_messages.join("\n"));
                                });
                };
    
                // Pass this method to sort an array of objects by one of their values
            var CompareObjectsByKey = function (key) {
                return function sort(a, b) {
                    if (!a.hasOwnProperty(key)) { return 0; }
                    if (!b.hasOwnProperty(key)) { return 0; }
                    if (a[key] > b[key]) { return  1; }
                    if (a[key] < b[key]) { return -1; }
                        return 0;
                    };
                };
    
                // Get the data once on load and then refresh it on a timer
            $(document).ready(function() {GetEventData(); } );
            setInterval(function() {GetEventData(); }, 60 * 60 * 1000); // Refresh the data once per hour
        </script>

                    <style>
                        .EventBodyName {font - size: 1.2em}
            .EventDateTime, .EventLocation {padding - left: 1em;}
            ul {
                            list - style - type: none;
                        padding-left: 7%;
                    }
            li:not(:last-child) {
                            margin - bottom: 1em;
                    }
            #container {
                            display: block;
                        margin-left: auto;
                        margin-right: auto;
                        width: 444px;
                    }
            .content-message {
                            display: block;
                        padding-left: 3em;
                    }
        </style>
    </head>

                <body>
                    <div id="container">
                        <img id="logo" height="144" width="444" src="https://www.oklahomacounty.org/ImageRepository/Document?documentID=117" alt="Oklahoma County Logo">
                            <h2>Upcoming Meetings</h2>
                            <div id="content"><span class="content-message info">Loading meeting data...</span></div>
        </div>
    </body>
</html>