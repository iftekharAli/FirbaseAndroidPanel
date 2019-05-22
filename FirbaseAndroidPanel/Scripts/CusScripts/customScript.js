
$(document).ready(function () {

    $("#datetimepickerFromDate").datetimepicker({ format: 'YYYY-MM-DD' });
    $("#datetimepickerEndDate").datetimepicker({ format: 'YYYY-MM-DD' });

    //var dateString = 'Mon Jan 12 00:00:00 GMT 2015';
    //dateString = new Date(dateString).toUTCString();
    //dateString = dateString.split(' ').slice(0, 4).join(' ');
    //console.log(dateString);

});

function searchData() {

    var fromDate = $("#fdate").val();
    var toDate = $("#edate").val();

    $.ajax({
        type: "GET",
        url: "http://202.164.213.242/fbandroid/SongsterReport/GetResult?fromDate=" + fromDate + "&toDate=" + toDate,
        success: function(res) {
            if (res.length != 0) {
			$("#dataTable").empty();
                $.each(res,
                    function (k, v) {

                        console.log(v);
                        var dateString = v.TimeStamp["TimeStamp"];
                        //dateString = new Date(dateString).toTimeString();
                        //dateString = dateString.split(' ').slice(0, 4).join(' ');
                        //console.log(dateString);


                        var dates = dateString.split(" ")[0];
                        console.log(dates);

                        var date = "<td>" + dates +"</td>";
                        var send = "<td>" + v.Send +"</td>";
                        var click = "<td>" + v.TimeStamp["Count"] +"</td>";

                        $("#dataTable").append("<tr>"+date+ send + click+"</tr>");

                    });
            }

        }
});


}