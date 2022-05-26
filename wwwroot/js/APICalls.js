
var producHeaderString = "<div class='col-md-4 mt-1 mt-sm-0 card-container mb-2' >";
producHeaderString += "<div class='HomeCard card text-center product p-1 pt-1 border-1 h-100 rounded-0' onclick='openProductMeals(\"*PARENTPRODUCTID*\")'>";
producHeaderString += "<img class='img-fluid d-block mx-auto' src='/ProductPics/#IMAGEFILE#' alt='#PRODUCTNAME#'>";
producHeaderString += "<div class='card-body p-2 py-0 h-xs-440p'><h5 class='card-title font-weight-semi-bold mb-3 w-xl-220p mx-auto'>#PRODUCTNAME#</h5>";
producHeaderString += "<p><em>#PRODUCTDETAILS#</em></p><p class='price'><i>From R#PRODUCTMINPRICE#</i></p></div>";
producHeaderString += "<p class='btn w-100 px-4 mx-auto'><input type='button' onclick='openProductMeals(\"*PARENTPRODUCTID*\")' class='btn btn-dark btn-lg w-100' name='add-button' value='Explore'></p>";
producHeaderString += "</div></div>";


var ProductMealsString = "<div class='col col-12 col-sm-12 col-md-12 col-lg-6 text-center'><div class=''><button type='button' onclick='backToCategory()' class='btn btn-dark btn-sm'><i class='fa fa-backward'></i>Back</button></div>";
ProductMealsString += "<img src='/ProductPics/#IMAGEFILE#' class='img-fluid' alt='#MEALPRODUCTNAME# - McDonald's'>";
ProductMealsString += "</div><div class='col col-12 col-sm-12 col-md-12 col-lg-6 product-entry'>";
ProductMealsString += "<h1>#MEALPRODUCTNAME#</h1>";
ProductMealsString += "<div class='h5'>#MAELCALORIES#kCal</div><span class='description'><p>#MEALDESCRIPTION#</p></span>";
ProductMealsString += "#SizeBtnR#";
ProductMealsString += "#SizeBtnM#";
ProductMealsString += "#SizeBtnL#";
ProductMealsString += "<label class='ms-4'>R<span id='ProductPrice'>0</span></label>";
ProductMealsString += "<div class='row mt-3'>";
ProductMealsString += "<div class='col-sm-6 align-center border-top border-bottom pt-1'>";
ProductMealsString += "<button type='button' class='btn btn-sm btn-primary' onclick='updateQuantity(-1)'>-</button>";
ProductMealsString += "<p style='width:35px;' class='h5 d-inline-block align-bottom text-center' id='QuantityMeter'>1</p>";
ProductMealsString += "<button type='button' class='btn btn-sm btn-primary' onclick='updateQuantity(1)'>+</button>";
ProductMealsString += "<button id='Large' type='button' class='btn btn-sm btn-success ms-3' onclick='AddToOrder()'>Add to basket</button>";
ProductMealsString += "</div>";
ProductMealsString += "<div class='col-sm-6 align-center border-top border-bottom'><label class='h3 float-end'>Total: R<span id='QuantityTotal'>0</span></label>";
ProductMealsString += "</div></div></div>";

function backToCategory() {
    $("#MealDetails").hide();
    $("#CategoryHeaders").show();
}

var LastProductCategoryID = "";
var LastCategoryProductsStartIndex = 0;
function openCategoryProducts(ItemID, StartIndex) {
    if (ItemID != "") {
        LastProductCategoryID = ItemID;
        LastCategoryProductsStartIndex = 0;
    }
    if (StartIndex == 1000 && LastCategoryProductsStartIndex > 0) {
        LastCategoryProductsStartIndex = LastCategoryProductsStartIndex - 3;
    }
    else if (StartIndex == 2000) {
        LastCategoryProductsStartIndex = LastCategoryProductsStartIndex + 3;
    }
    else {
        if (StartIndex < 999) {
            LastCategoryProductsStartIndex = StartIndex;
        }
    }

    //highlight page Number
    $(".page-item").removeClass("active");
    var pagename = ".pgeNr" + ((LastCategoryProductsStartIndex / 3) + 1);
    $(pagename).addClass("active");

    $("#homescreen").hide();
    $("#CategoryHeadersContent").html("");
    $("#checkoutScreen").hide();
    $("#MealDetails").hide();
    $("#CategoryHeaders").show();
    $("#OrderSuccess").hide();
    $("#OrderInProgress").show();

    $(".nav-item").removeClass("bg-warning");
    var newNav = "#nav_" + LastProductCategoryID;
    $(newNav).addClass("bg-warning");


    var ApiStr = '/api/Product_API_/GetPerCategory?CategoryID=' + LastProductCategoryID + "&startIndex=" + LastCategoryProductsStartIndex;
    $.getJSON(ApiStr, function (json) {
        for (var key in json) {
            var value = json[key];
            if (typeof value == 'object') {
                var addProductString = producHeaderString.replace("#IMAGEFILE#", value.smallImageURL);
                addProductString = addProductString.replace("#PRODUCTNAME#", value.name);
                addProductString = addProductString.replace("#PRODUCTNAME#", value.name);
                addProductString = addProductString.replace("#PRODUCTDETAILS#", value.description);
                addProductString = addProductString.replace("#PRODUCTMINPRICE#", value.price);
                addProductString = addProductString.replace("*PARENTPRODUCTID*", value.id);
                addProductString = addProductString.replace("*PARENTPRODUCTID*", value.id);
                $("#CategoryHeadersContent").append(addProductString);
            }
        }
    });
}



function OpenCheckOut() {
    $("#homescreen").hide();
    $("#CategoryHeaders").hide();
    $("#MealDetails").hide();
    $("#checkoutScreen").show();
}


function openProductMeals(ItemID) {
    $("#homescreen").hide();
    $("#CategoryHeaders").hide();
    $("#checkoutScreen").hide();
    $("#MealDetails").show();
    var SizeR = false; var PriceR = 0; var SizeM = false; var PriceM = 0; var SizeL = false; var PriceL = 0;
    var ProductR = ""; var ProductM = ""; var ProductL = "";
    var apiStr = "/api/Product_API_/GetMealsPerProduct/" + ItemID;
    var addProductString = ProductMealsString;

    $.getJSON(apiStr, function (json) {
        for (var key in json) {
            var value = json[key];
            if (typeof value == 'object') {
                addProductString = addProductString.replace("#IMAGEFILE#", value.mealImageURL);
                addProductString = addProductString.replace("#MEALPRODUCTNAME#", value.name);
                addProductString = addProductString.replace("#MEALPRODUCTNAME#", value.name);
                addProductString = addProductString.replace("#MEALDESCRIPTION#", value.description);
                addProductString = addProductString.replace("#MAELCALORIES#", "12345");
                if (value.size == "R") { SizeR = true; PriceR = value.price; ProductR = value.id; };
                if (value.size == "M") { SizeM = true; PriceM = value.price; ProductM = value.id; };
                if (value.size == "L") { SizeL = true; PriceL = value.price; ProductL = value.id; };

            }
        }
        if (SizeR) {
            addProductString = addProductString.replace("#SizeBtnR#", "<button id='btnsizeR'  type='button' class='btn btn-outline-secondary btnSetSize' onclick='UpdateProductSize(1," + PriceR + ",\"" + ProductR + "\")'>Regular</button>");
        }
        else {
            addProductString = addProductString.replace("#SizeBtnR#", "");
        }
        if (SizeM) {
            addProductString = addProductString.replace("#SizeBtnM#", "<button id='btnsizeM' type='button' class='btn btn-outline-secondary btnSetSize ms-2' onclick='UpdateProductSize(2," + PriceM + ",\"" + ProductM + "\")'>Medium</button>");
        }
        else {
            addProductString = addProductString.replace("#SizeBtnM#", "");
        }
        if (SizeL) {
            addProductString = addProductString.replace("#SizeBtnL#", "<button id='btnsizeL'  type='button' class='btn btn-outline-secondary btnSetSize ms-2' onclick='UpdateProductSize(3," + PriceL + ",\"" + ProductL + "\")'>Large</button>");
        }
        else {
            addProductString = addProductString.replace("#SizeBtnL#", "");
        }




        $("#MealDetailsContent").html(addProductString);
    });
}

var ProductPrice = 0;
var QuantityMtr = 1;
var TotalPrice = ProductPrice * QuantityMtr;
var SelectedProductID = "";

function updateQuantity(qNr) {
    QuantityMtr += qNr;
    TotalPrice = ProductPrice * QuantityMtr;
    TotalPrice = (Math.round(TotalPrice * 100) / 100).toFixed(2)
    $("#QuantityMeter").html(QuantityMtr);
    $("#QuantityTotal").html(TotalPrice);
    $("#ProductPrice").html(ProductPrice);
}

function UpdateProductSize(SizeType, Price, ProductID) {
    SelectedProductID = ProductID;
    $(".btnSetSize").removeClass("btn-outline-secondary");
    $(".btnSetSize").removeClass("btn-success");
    if (SizeType == 1) { $("#btnsizeR").addClass("btn-success"); $("#btnsizeM").addClass("btn-outline-secondary"); $("#btnsizeL").addClass("btn-outline-secondary"); }
    if (SizeType == 2) { $("#btnsizeR").addClass("btn-outline-secondary"); $("#btnsizeM").addClass("btn-success"); $("#btnsizeL").addClass("btn-outline-secondary"); }
    if (SizeType == 3) { $("#btnsizeR").addClass("btn-outline-secondary"); $("#btnsizeM").addClass("btn-outline-secondary"); $("#btnsizeL").addClass("btn-success"); }
    ProductPrice = Price;
    $("#QuantityMeter").html(QuantityMtr);
    TotalPrice = ProductPrice * QuantityMtr;
    TotalPrice = (Math.round(TotalPrice * 100) / 100).toFixed(2)
    $("#QuantityTotal").html(TotalPrice);
    $("#ProductPrice").html(ProductPrice);
}

function CreateOrder() {
    var currentUserID = $("#Order_CurrentUser").val();
    var currentOrderID = $("#Order_CurrentOrder").val();
    var apiStr1 = '/api/Order_API_/NewOrder?UserID=' + currentUserID;

    $.getJSON(apiStr1, function (json) {
        currentOrderID = json.id;
        $("#Order_CurrentOrder").val(currentOrderID);
        var apiStr2 = '/api/Order_API_/AddOrderProduct?OrderID=' + currentOrderID + "&MealProductID=" + SelectedProductID + "&Quantity=" + QuantityMtr
        $.getJSON(apiStr2, function (json2) {
            retrieveOrder();
        });
    });


}

function AddToOrder() {
    var currentUser = $("#Order_CurrentUser").val();
    var currentOrderID = $("#Order_CurrentOrder").val();

    if (currentOrderID == "00000000-0000-0000-0000-000000000000") {
        currentOrderID = CreateOrder();
    }
    else {
        var apiStr = '/api/Order_API_/AddOrderProduct?OrderID=' + currentOrderID + "&MealProductID=" + SelectedProductID + "&Quantity=" + QuantityMtr
        $.getJSON(apiStr, function (json) {
            retrieveOrder();
        });
    }


}


function retrieveOrder() {
    var currentUserID = $("#Order_CurrentUser").val();
    var currentOrderID = $("#Order_CurrentOrder").val();
    var ItemCounter = 0;
    var OrderContentsPop = "";
    var OrderContents = "";

    var apiStr1 = '/api/Order_API_/GetOrder?OrderId=' + currentOrderID;
    $.getJSON(apiStr1, function (json) {
        $("#CurrentTaxPrice").html("R " + json.taxPrice);
        $("#PopOrderFullAmount").html("R " + json.taxPrice);

        $("#CurrentTaxPrice_C").html("R " + json.taxPrice);
        $("#PopOrderFullAmount_C").html("R " + json.taxPrice);

        var taxval = json.taxPrice - json.orderTotal;
        $("#PopOrderTaxAmount").html("R " + ((Math.round(taxval * 100) / 100).toFixed(2)));
        $("#PopOrderTaxAmount_C").html("R " + ((Math.round(taxval * 100) / 100).toFixed(2)));
        for (var i = 0; i < json.orderList.length; i++) {
            var objItem = json.orderList[i];
            ItemCounter += objItem.quantity;

            OrderContentsPop += "<tr><td>" + objItem.productName + "</td><td>" + objItem.size + "</td><td class='text-center'>";
            OrderContentsPop += "<input onchange='updateOrderProductQuatity(this,\"" + objItem.id + "\")' type='number' min='0' max='20' step='1' value='" + objItem.quantity + "' class='form-control form-control-sm' onKeyDown='return false' style='min-height:25px;max-height:25px;'></td>";
            OrderContentsPop += "<td class='text-end'>R " + objItem.totalPrice + "</td></tr>";

            OrderContents += "<tr><td>" + objItem.productName + "</td><td>" + objItem.size + "</td><td class='text-end'>R " + objItem.productPrice + "</td><td class='text-center'>";
            OrderContents += "<input onchange='updateOrderProductQuatity(this,\"" + objItem.id + "\")' type='number' min='0' max='20' step='1' value='" + objItem.quantity + "' class='form-control form-control-sm' onKeyDown='return false' style='min-height:25px;max-height:25px;'></td>";
            OrderContents += "<td class='text-end'>R " + objItem.totalPrice + "</td></tr>";
        }
        $("#CurrentItemQuantity").html(ItemCounter);
        $("#orderPopContent").html(OrderContentsPop);
        $("#orderContent").html(OrderContents);
    });
}


function updateOrderProductQuatity(selectObject, IdValue) {
    var value = selectObject.value;
    var apiStr = '/api/Order_API_/Order_UpdateProductQuantity?ItemID=' + IdValue + "&Quantity=" + value;
    $.getJSON(apiStr, function (json) {
        retrieveOrder();
    });
}

function CancelOrder(ItemID) {
    var currentOrderID = $("#Order_CurrentOrder").val();
    var apiStr = '/api/Order_API_/Order_Cancel?OrderID=' + currentOrderID;
    $.getJSON(apiStr, function (json) {
        //retrieveOrder();
        currentOrderID = "00000000-0000-0000-0000-000000000000";
        $("#QuantityTotal").html("0");
        $("#ProductPrice").html("R 0");
        $("#CurrentItemQuantity").html("0");
        $("#orderPopContent").html("");
        $("#CurrentTaxPrice").html("");
        $("#orderPopContent").html("");
        $("#PopOrderTaxAmount").html("");
        $("#PopOrderFullAmount").html("");
        $("#PopOrderTaxAmount_C").html("");
        $("#CurrentTaxPrice_C").html("");
        $("#orderContent").html("");
        $("#Order_CurrentOrder").val("00000000-0000-0000-0000-000000000000")

        $("#homescreen").show();
        $("#CategoryHeaders").hide();
        $("#checkoutScreen").hide();
        $("#MealDetails").hide();

        $(".nav-item").removeClass("bg-warning");
        var newNav = "#nav_00000000-0000-0000-0000-000000000000";
        $(newNav).addClass("bg-warning");
    });
}

function FinaliseOrder(ItemID) {
    var currentOrderID = $("#Order_CurrentOrder").val();
    var apiStr = '/api/Order_API_/Order_Finalise?OrderID=' + currentOrderID;
    $.getJSON(apiStr, function (json) {
        currentOrderID = "00000000-0000-0000-0000-000000000000";
        $("#QuantityTotal").html("0");
        $("#ProductPrice").html("R 0");
        $("#CurrentItemQuantity").html("0");
        $("#PopOrderTaxAmount").html("");
        $("#PopOrderFullAmount").html("");
        $("#PopOrderTaxAmount_C").html("");
        $("#CurrentTaxPrice_C").html("");
        $("#CurrentTaxPrice").html("");
        $("#orderPopContent").html("");
        $("#orderContent").html("");
        $("#Order_CurrentOrder").val("00000000-0000-0000-0000-000000000000")
        $(".nav-item").removeClass("bg-warning");
        var newNav = "#nav_00000000-0000-0000-0000-000000000000";
        $(newNav).addClass("bg-warning");
    });
}


function validateCheckout() {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')
    var errorCount = 0;
    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
                errorCount++;
            }
            form.classList.add('was-validated')
        })
    if (errorCount == 0) {
        //Call Api to save the data and this would also go to pay service
        var currentOrderID = $("#Order_CurrentOrder").val();
        FinaliseOrder(currentOrderID);

        //Clear the Order for now

        //Show that all is done and it is on it's way
        $("#OrderSuccess").show();
        $("#OrderInProgress").hide();
    }
}
