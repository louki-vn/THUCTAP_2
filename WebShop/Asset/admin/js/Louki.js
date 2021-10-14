function EditPro(product_id) {
    var id = document.getElementById(String("id " + product_id)).innerText;
    var name = document.getElementById(String("name " + product_id)).innerText;
    var category = document.getElementById(String("category " + product_id)).innerText;
    var brand = document.getElementById(String("brand " + product_id)).innerText;
    var size = document.getElementById(String("size " + product_id)).innerText;
    var price = document.getElementById(String("price " + product_id)).innerText;
    var content = document.getElementById(String("content " + product_id)).innerText;
    var sale = document.getElementById(String("sale " + product_id)).innerText;
    $("#editproduct").find('input[name="product_id"]').val(id);
    $("#editproduct").find('input[name="name"]').val(name);
    $("#editproduct").find('input[name="category"]').val(category);
    $("#editproduct").find('input[name="brand_2"]').val(brand);
    $("#editproduct").find('input[name="size_2"]').val(size);
    $("#editproduct").find('input[name="price_2"]').val(price);
    $("#editproduct").find('input[name="content_2"]').val(content);
    $("#editproduct").find('input[name="sale"]').val(sale);
};

function Delproduct(product_id) {
    var id = document.getElementById(String("id " + product_id)).innerText;
    var name = document.getElementById(String("name " + product_id)).innerText;
    $("#deleteproduct").find('input[name="delete_name"]').val(name);
    $("#deleteproduct").find('input[name="delete_id"]').val(id);
};

function DeleteProduct() {
    var id = $("#deleteproduct").find('input[name="delete_id"]').val();
    console.log(id);
    $.ajax({
        type: "GET",
        data: { product_id: id },
        url: "/ProductAdmin/DeleteProduct",
        success: function (e) {

        }
    });
    window.location.reload();
}

function EditProduct() {
    var name = $("#editproduct").find('input[name="name"]').val();
    var id = $("#editproduct").find('input[name="product_id"]').val();
    var sale = $("#editproduct").find('input[name="sale"]').val();
    var size = $("#editproduct").find('input[name="size_2"]').val();
    var price = $("#editproduct").find('input[name="price_2"]').val();
    var content = $("#editproduct").find('input[name="content_2"]').val();

    $.ajax({
        type: "GET",
        data: {
            product_id: id,
            name: name,
            size: size,
            price: price,
            content: content,
            sale: sale
        },
        url: "/ProductAdmin/EditProduct",
        success: function (e) {

        }
    });
    window.location.reload();
}

function DelMem(category_id) {
    var id = document.getElementById(String("id " + category_id)).innerText;
    var name = document.getElementById(String("name " + category_id)).innerText;
    $("#deletemember").find('input[name="delete_id"]').val(id);
    $("#deletemember").find('input[name="delete_name"]').val(name);
};

function DeleteMember() {
    var id = $("#deletemember").find('input[name="delete_id"]').val();
    console.log(id)
    $.ajax({
        type: "POST",
        data: { delete_id: id },
        url: "/UserData/DeleteMember",
        success: function (e) {

        }
    });
    window.location.reload();
}

function report(tran_id) {
    var id = document.getElementById(String("id " + tran_id)).innerText;
    var mem = document.getElementById(String("mem_id " + tran_id)).innerText;
    var product = document.getElementById(String("product_id " + tran_id)).innerText;
    var amount = document.getElementById(String("amount " + tran_id)).innerText;
    var qty = document.getElementById(String("qty " + tran_id)).innerText;

    $("#addreport").find('input[name="tran_id"]').val(id);
    $("#addreport").find('input[name="mem_id"]').val(mem);
    $("#addreport").find('input[name="product_id"]').val(product);
    $("#addreport").find('input[name="amount"]').val(amount);
    $("#addreport").find('input[name="qty"]').val(qty);
};

function DelOrder(tran_id) {
    var id = document.getElementById(String("id " + tran_id)).innerText;
    var name = document.getElementById(String("name " + tran_id)).innerText;
    console.log(name)
    console.log(id)
    $("#deleteorder").find('input[name="delete_name"]').val(name);
    $("#deleteorder").find('input[name="delete_id"]').val(id);
};

function Change(tran_id) {
    var id = document.getElementById(String("id " + tran_id)).innerText;
    $("#editorder").find('input[name="change_tran_id"]').val(id);
};

function ChangeStatus() {
    var id = $("#editorder").find('input[name="change_tran_id"]').val();
    $.ajax({
        type: "GET",
        data: { tran_id: id },
        url: "/Order/EditOrder",
        success: function (e) {

        }
    });
    window.location.reload();
}

function addreport() {
    var id = $("#addreport").find('input[name="tran_id"]').val();
    var mem_id = $("#addreport").find('input[name="mem_id"]').val();
    var product_id = $("#addreport").find('input[name="product_id"]').val();
    var amount = $("#addreport").find('input[name="amount"]').val();
    var qty = $("#addreport").find('input[name="qty"]').val();
    var employee_id = $("#addreport").find('input[name="employee_id"]').val();
    var date = $("#addreport").find('input[name="date"]').val();

    $.ajax({
        type: "GET",
        data: {
            tran_id: id,
            mem_id: mem_id,
            product_id: product_id,
            amount: amount,
            qty: qty,
            employee_id: employee_id,
            date: date
        },
        url: "/Order/AddReport",
        success: function (e) {

        }
    });
    window.location.reload();
}

function DeleteOrder() {
    var id = $("#deletecategory").find('input[name="delete_id"]').val();
    $.ajax({
        type: "GET",
        data: { tran_id: id },
        url: "/Category/DeleteCategory",
        success: function (e) {

        }
    });
    window.location.reload();
}

function EditCategory() {
    var name = $("#editcategory").find('input[name="name"]').val();
    var id = $("#editcategory").find('input[name="category_id"]').val();
    console.log(name)
    console.log(id)
    $.ajax({
        type: "GET",
        data: { tran_id: id },
        url: "/Category/EditCategory",
        success: function (e) {

        }
    });
    window.location.reload();
}

function edit(category_id) {
    var id = document.getElementById(String("id " + category_id)).innerText;
    var name = document.getElementById(String("name " + category_id)).innerText;
    var group = document.getElementById(String("group " + category_id)).innerText;
    $("#editcategory").find('input[name="category_id"]').val(id);
    $("#editcategory").find('input[name="name"]').val(name);
    $("#editcategory").find('input[name="group_id"]').val(group);
};

function Delcategory(category_id) {
    var id = document.getElementById(String("id " + category_id)).innerText;
    var name = document.getElementById(String("name " + category_id)).innerText;
    console.log(name)
    console.log(id)
    $("#deletecategory").find('input[name="delete_name"]').val(name);
    $("#deletecategory").find('input[name="delete_id"]').val(id);
};

function DeleteCategory() {
    var id = $("#deletecategory").find('input[name="delete_id"]').val();
    $.ajax({
        type: "GET",
        data: { delete_id: id },
        url: "/Category/DeleteCategory",
        success: function (e) {

        }
    });
    window.location.reload();
}

function EditCategory() {
    var name = $("#editcategory").find('input[name="name"]').val();
    var id = $("#editcategory").find('input[name="category_id"]').val();
    console.log(name)
    console.log(id)
    $.ajax({
        type: "GET",
        data: { category_id: id, category_name: name },
        url: "/Category/EditCategory",
        success: function (e) {

        }
    });
    window.location.reload();
}