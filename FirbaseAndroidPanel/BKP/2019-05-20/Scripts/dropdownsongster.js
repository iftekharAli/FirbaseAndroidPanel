﻿var app = angular.module('myApp', []);
app.controller('cntrl',
    function ($scope, $http) {
       // var raw = "../api/";
       var raw = "http://202.164.213.242/fbandroid/api/";
        $scope.catshow = false;
        $scope.subcatshow = false;
        $scope.ListOfContentShow = false;
        $scope.subdateshow1 = true;
        var datat = [];
        var tt = '';
        //fireportal
        $('#datetimepicker1').datetimepicker().on('dp.change', function (ev) {
            // $scope.stardate = 'asaas';
            tt = $("#qq").val();
          //  alert(tt);
        });


        //$http.get(raw + 'DropDownAndList/FirebasePortalNames').then(function (response) {
        //    console.log(response.data);
        //    if (response.data.length > 0) {
        //        $scope.fireportal = response.data;
              

        //    }

        //});
        $scope.update = function () {
            //alert('hi');
            $scope.subdateshow1 = true;
            $scope.catshow = false;
            $scope.subcatshow = false;
            $scope.subdateshow = false;
            var data = {
                CatCode: $scope.pName,
                SubCatCode: ""
            };
            console.log($scope.data);

        };
        $scope.updateSubCat = function() {

            ////   $scope.subcatshow = false;
            //var data = {
            //    CatCode: $scope.pName,
            //    SubCatCode: $scope.catId
            //};
            //console.log(data);

            ////   alert('i m 2');

            //$http.post(raw + 'DropDownAndList/CatAndSubCat', data).then(function(response) {
            //    console.log('length' + response.data.length);
            //    if (response.data.length === 0) {
            //        $scope.subcatshow = false;
            //        $scope.subdateshow = true;
            //    } else {
            //        $scope.subcatshow = true;
            //        $scope.subdateshow = false;

            //    }

            //    if (response.data.length > 0) {
            //        if ($scope.catId !== null) {
            //            $scope.SubCatList = response.data;
            //            $scope.subcatshow = true;
            //            $scope.subdateshow = true;

            //        } else {
            //            $scope.subcatshow = false;
            //            $scope.subdateshow = true;
            //        }

            //    }

            //});
        };
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
            //alert($scope.pName);
            if ($scope.pName === 'song') {
                var tt1 = '';

                tt1 = $("#qq").val();
                //alert(tt);
                $scope.finals = datat[0];
                    console.log($scope.final);
                    $scope.finals.Title = $scope.pushTxtTitle;
                    $scope.finals.Body = $scope.pushTxt;
               
                    $scope.finals.ProcessTime = tt1;
                $scope.finals.ServiceName = $scope.pName;
                    console.log($scope.finals);
                    $http.post(raw + 'TokenManage/SavePushMessage', $scope.finals).then(function(response) {

                        if (response.data.result === 'Failed') {
                           // window.location.href = "http://202.164.213.242/fbandroid/";
                        } else {
                          //  window.location.href = "http://202.164.213.242/fbandroid/ReportModify";

                        }

                    });
                
            
          


            }
        };

        $scope.Submit = function () {
            var catCodeFinal = '';
            if ($scope.subcatshow) {
                catCodeFinal = $scope.subCatId;
            } else {
                catCodeFinal = $scope.catId;
            }
            var dataList = {
                PortalCode: $scope.pName,
                CatCode: catCodeFinal,
                Caption: $scope.ucaption,
                Uid: $scope.uid
            };
            console.log(dataList);
            console.log('i am here');
            console.log($scope.pName);

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
            else if ($scope.pName === 'song') {
                console.log('i am here');
                console.log(dataList);
                $http.post(raw + 'DropDownAndList/ContentListSongster', dataList).then(function (response) {

                    if (response.data.length > 0) {
                        $scope.ListOfContentShow = true;
                        $scope.listOfContents = response.data;
                    }
                    else {
                        $scope.ListOfContentShow = false;
                        $scope.noData = true;
                    }
                });
                $scope.ListOfContentShow = true;
                $scope.subdateshow = true;
                $scope.noData = false;
            }

            else if ($scope.pName === 'sb') {


                $http.post(raw + 'DropDownAndList/ContentList', dataList).then(function (response) {

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