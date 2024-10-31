<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxTest.aspx.cs" Inherits="deTestWebForm0509.ajaxTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-3.7.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel_ajaxBodys" runat="server"></asp:Panel>
       
        <asp:DropDownList ID="DropDownList_apiSwitch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_apiSwitch_OnSelectedIndexChanged"></asp:DropDownList>
        <input  type="text" id="targetID" value="14" />
        <input  type="button" value="Fetch" onclick="startFetch()" />

        <div id="result">
        </div>
    </form>
    <script>
        const apiSwitch = document.querySelector('#DropDownList_apiSwitch');
        const targetID = document.querySelector('#targetID');



        function startFetch() {
            var parameterBody = "";
            document.querySelector('#Panel_ajaxBodys').querySelectorAll('input').forEach(function (d, i) {
                parameterBody += `"${d.id}" : "${d.value}",`
            });
            console.log(parameterBody);
            //const data = [{ id: targetID.value }];
            /*
            fetch("https://pokeapi.co/api/v2/" + apiSwitch[apiSwitch.selectedIndex].text + '/' + targetID.value,
                {
                    method: "GET", // or 'PUT'
                    //body: JSON.stringify(data), // data can be `string` or {object}!
                    headers: new Headers({
                        "Content-Type": "application/json",
                    })
                })
                .then(function (response) {
                    return response.json();
                })
                .then(function (myJson) {
                    console.log(myJson);
                    //$('#result').html(
                    //    JSON.stringify(myJson)

                    //);
                    const treeContainer = document.getElementById("result");
                    treeContainer.innerHTML = '';
                    treeContainer.appendChild(createTree(myJson));
                })
                .catch((error) => console.error("Error:", error));
*/
        }


        function createTree(data) {
            const ul = document.createElement("ul");
            for (let key in data) {
                const li = document.createElement("li");
                li.textContent = key;

                // 如果此key包含子節點，則遞迴呼叫
                if (typeof data[key] === "object") {
                    li.appendChild(createTree(data[key]));
                } else {
                    li.textContent += `: ${data[key]}`;
                }
                ul.appendChild(li);
            }
            return ul;
        }
    </script>
    <script>
        // My JSON Data 動態遞迴treeView
        const locations = [
            {
                id: 1,
                name: "San Francisco Bay Area",
                parentId: null
            },
            {
                id: 2,
                name: "San Jose",
                parentId: 3
            },
            {
                id: 3,
                name: "South Bay",
                parentId: 1
            },
            {
                id: 4,
                name: "San Francisco",
                parentId: 5
            },
            {
                id: 5,
                name: "Manhattan",
                parentId: 2
            },
        ];
        function createTreeView(location) {
            var tree = [],
                object = {},
                parent,
                child;

            for (var i = 0; i < location.length; i++) {
                parent = location[i];

                object[parent.id] = parent;
                object[parent.id]["children"] = [];
            }

            for (var id in object) {
                if (object.hasOwnProperty(id)) {
                    child = object[id];
                    // i made some changes here incase some element is missing so you dont get error and just append th tree insetad 
                    if (child.parentId && object[child["parentId"]]) {
                        object[child["parentId"]]["children"].push(child);
                    } else {
                        tree.push(child);
                    }
                }
            }
            return tree;
        }


        // here is how you build your UL treeview recursively
        function CreateUlTreeView(items, parent) {
            var ul = document.createElement("ul");
            if (parent)
                parent.appendChild(ul);
            items.forEach(function (x) {
                var li = document.createElement("li");
                var text = document.createElement("span");
                text.innerHTML = x.name;
                li.appendChild(text);
                if (x.children && x.children.length > 0)
                    CreateUlTreeView(x.children, li);
                ul.append(li);
            });
            return ul;
        }
        //var root = createTreeView(locations);

        //CreateUlTreeView(root, document.getElementById("result"))
    </script>
</body>
</html>
