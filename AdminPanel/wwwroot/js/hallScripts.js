function pad(n, width) {
    n = n + "";
    return n.length >= width ? n : new Array(width - n.length + 1).join("0") + n;
}

function addHighlighting() {
    $(function () {
        var isMouseDown = false,
            isHighlighted;
        $("#hallTable td")
            .mousedown(function () {
                isMouseDown = true;
                $(this).toggleClass("highlighted");
                isHighlighted = $(this).hasClass("highlighted");
                return false; // prevent text selection
            })
            .mouseover(function () {
                if (isMouseDown) {
                    $(this).toggleClass("highlighted", isHighlighted);
                }
            })
            .bind("selectstart",
                function () {
                    return false;
                });

        $(document)
            .mouseup(function () {
                isMouseDown = false;
            });
    });
}

function updateTable() {
    row = document.getElementById("rows").value;
    columns = document.getElementById("columns").value;

    var divTable = document.getElementById("divTable");

    var table = document.createElement("table");
    table.id = "hallTable";
    table.style.width = "100%";
    table.style.height = "100%";
    for (var i = 0; i < row; i++) {
        var tr = document.createElement("tr");
        for (var j = 0; j < columns; j++) {
            var td = document.createElement("td");
            td.id = i + "_" + j;
            //td.appendChild(document.createTextNode(pad(i + 1, 2) + "-" + pad(j + 1, 2)));
            tr.appendChild(td);
        }

        table.appendChild(tr);
    }

    divTable.innerHTML = "";
    divTable.appendChild(table);
    addHighlighting();
}

function tableSelectAll() {
    for (var i = 0; i < row; i++) {
        for (var j = 0; j < columns; j++) {
            var td = document.getElementById(i + "_" + j);
            td.classList.add("highlighted");
        }
    }
}

function tableDeselectAll() {
    for (var i = 0; i < row; i++) {
        for (var j = 0; j < columns; j++) {
            var td = document.getElementById(i + "_" + j);
            td.classList.remove("highlighted");
        }
    }
}

function hallTableToArray() {
    var tableArray = [];
    for (var i = 0; i < row; i++) {
        var trArray = [];
        for (var j = 0; j < columns; j++) {
            var td = document.getElementById(i + "_" + j);
            trArray.push(td.classList.contains("highlighted"));
        }

        tableArray.push(trArray);
    }

    return tableArray;
}