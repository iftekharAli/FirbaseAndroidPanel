var app = angular.module('myApp', []);
app.controller('cntrl',
    function ($scope, $http) {
       // var raw = "../api/";
       var raw = "https://wap.shabox.mobi/fbandroid/api/";
        $scope.catshow = false;
        $scope.subcatshow = false;
        $scope.ListOfContentShow = false;
        $scope.subdateshow = false;
        var datat = [];
        var tt = '';
        //fireportal
        $('#datetimepicker1').datetimepicker().on('dp.change', function (ev) {
            // $scope.stardate = 'asaas';
            tt = $("#qq").val();
          //  alert(tt);
        });


        $http.get(raw + 'DropDownAndList/FirebasePortalNames').then(function (response) {
            console.log(response.data);
            if (response.data.length > 0) {
                $scope.fireportal = response.data;
              

            }

        });
        $scope.update = function () {
            $scope.catshow = false;
            $scope.subcatshow = false;
            $scope.subdateshow = false;
            var data = {
                CatCode: $scope.pName,
                SubCatCode: ""
            }
            console.log($scope.data);
         //   alert('i m 1');
            $http.post(raw + 'DropDownAndList/CatAndSubCat', data).then(function (response) {
                console.log(response.data);
                if (response.data.length > 0) {
                    $scope.CatList = response.data;
                    $scope.catshow = true;

                } else if ($scope.pName === 'stk') {
                    $scope.subdateshow = true;
                }
                
            });
        }
        $scope.updateSubCat = function () {
           
         //   $scope.subcatshow = false;
            var data = {
                CatCode: $scope.pName,
                SubCatCode: $scope.catId
            }
            console.log(data);
    
         //   alert('i m 2');

            $http.post(raw + 'DropDownAndList/CatAndSubCat', data).then(function (response) {
                console.log('length' + response.data.length);
                if (response.data.length === 0) {
                    $scope.subcatshow = false;
                    $scope.subdateshow = true;
                } else {
                    $scope.subcatshow = true;
                    $scope.subdateshow = false;

                }
               
                if (response.data.length > 0) {
                    if ($scope.catId != null) {
                        $scope.SubCatList = response.data;
                        $scope.subcatshow = true;
                        $scope.subdateshow = true;

                    } else {
                        $scope.subcatshow = false;
                        $scope.subdateshow = true;
                    }
                 
                }

            });
        }
        $scope.toggleSelection = function toggleSelection(event,code,img) {
           
          //  alert(event.target.checked);
          //  alert(code);
          //  alert(img);
            if (event.target.checked) {
                var datats = {
                    ContentCode: code,
                    ImageUrl: img,
                    Title: $scope.pushTxtTitle,
                    Body: $scope.pushTxt,
                    ServiceName: $scope.fireporalId,
                    ProcessTime: tt

                };
                datat.push(datats);

                //for (var i = 1; i <= 20; i++) {
                //    datat.push(datats);
                //};
                console.log(datat);
            } else {
                datat = [];

            }
          
           
        };
        $scope.Save = function() {
            //alert($scope.fireporalId);
            //alert($scope.pushTxt);//sendTime1
            var a = $("#qq").val();
            //alert(a);
         //   alert($scope.dda);
            var pushtext = $scope.pushTxt;
            var pushtextTitle = $scope.pushTxtTitle;
        //    alert('title'+pushtextTitle);
       //     alert('text'+pushtext);
            if ($scope.fireporalId === 'undefined' || pushtext === '' || pushtext === 'undefined' || a === 'undefined' || a === '' || pushtextTitle === '' || pushtextTitle === 'undefined' ) {
                alert('Some Parameter is/are missing');
            } else {
                if ($scope.ListOfContentShow) {

                    var datainserpust = {
                        Title: $scope.pushTxtTitle,
                        Body: $scope.pushTxt
                    }
           //         alert(datainserpust);
          //          alert('ddd00');
          //          console.log(datat);
                    $scope.final = datat[0];
                    console.log($scope.final);
                    $scope.final.Title = $scope.pushTxtTitle;
                    $scope.final.Body = $scope.pushTxt;
                    $scope.final.ProcessTime = tt;
                    $scope.final.ServiceName = $scope.fireporalId;
                    console.log($scope.final);
                    $http.post(raw + 'TokenManage/SavePushMessage', $scope.final ).then(function(response) {
                        //console.log(response.data);
                        //if (response.data.length > 0) {
                        //    $scope.CatList = response.data;
                        //    $scope.catshow = true;

                        //}
                      //  console.log(response.data.result);
                        if (response.data.result === 'Failed') {
                            window.location.href = "https://wap.shabox.mobi/fbandroid/";
                        } else {
                            window.location.href = "https://wap.shabox.mobi/fbandroid/ReportModify";

                        }
                            
                    });


                } else {
                    alert('nai');
                }



            }
        }

        $scope.Submit = function () {
            var catCodeFinal = '';
            if ($scope.subcatshow) {
                catCodeFinal = $scope.subCatId;
            } else {
                catCodeFinal = $scope.catId;
            }
            var dataList = {
                PortalCode: $scope.pName,
                CatCode: catCodeFinal
            };
            console.log(dataList);


            if ($scope.pName === 'stk') {


                $http.post(raw + 'DropDownAndList/ContentList', dataList).then(function(response) {

                    if (response.data.length > 0) {
                        $scope.ListOfContentShow = true;
                        $scope.subdateshow = true;
                        $scope.noData = false;
                        $scope.listOfContents = response.data;
                    }
                    else {
                        $scope.ListOfContentShow = false;
                        $scope.noData = true;
                    }
                });
            }

            else {


                $http.post(raw + 'DropDownAndList/ContentList', dataList).then(function (response) {
                    if (response.data.length > 0) {
                        $scope.ListOfContentShow = true;
                        $scope.subdateshow = true;
                        $scope.noData = false;
                        $scope.listOfContents = response.data;
                    } else {
                        $scope.ListOfContentShow = false;
                        $scope.noData = true;
                    }

                    console.log(response.data);

                });
            }

        

        }
    });