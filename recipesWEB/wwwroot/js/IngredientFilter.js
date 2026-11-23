async function loadIngredients() {
    let search = document.getElementById("search").value;
    let sortOrder = document.getElementById("sortOrder").value;

    let params = new URLSearchParams({
        search: search,
        sortOrder: sortOrder
    });

    let response = await fetch(
        "/Ingredient/GetFilteredIngredients?" + params
    );

    let ingredients = await response.json();

    let tbody = document.getElementById("ingredientsTable");
    tbody.innerHTML = "";

    ingredients.forEach(function (ingredient) {
        let row = document.createElement("tr");

        [ingredient.name, ingredient.recipesCount].forEach(function (value) {
            let cell = document.createElement("td");
            cell.textContent = value ?? "";
            row.appendChild(cell);
        });

        let actions = document.createElement("td");

        ["Edit", "Details", "Delete"].forEach(function (action, index) {
            if (index > 0) {
                actions.appendChild(document.createTextNode(" | "));
            }

            let link = document.createElement("a");
            link.href = "/Ingredient/" + action + "/" + ingredient.id;
            link.textContent = action;
            actions.appendChild(link);
        });

        row.appendChild(actions);
        tbody.appendChild(row);
    });
}

document.getElementById("search")
    .addEventListener("input", loadIngredients);

document.getElementById("sortOrder")
    .addEventListener("change", loadIngredients);

document.getElementById("clearFilters")
    .addEventListener("click", function () {
        document.getElementById("search").value = "";
        document.getElementById("sortOrder").value = "name_asc";

        loadIngredients();
    });