

let getSearchHistory = function(callback) {
    fetch("api/searchhistory")}
        .then(function(response) {
            return response.json();
        })
        .then(function(data) {
            callback(data);
        });

    getSearchHistory(function(data) {
        console.log(data);
    });
}