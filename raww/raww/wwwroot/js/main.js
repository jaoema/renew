
require.config({
    baseUrl: "js",
    paths : {
        knockout: "lib/knockout/knockout-latest",
        text: "lib/require-text/text.min",
        dataservice: "services/dataService",
    }
});

require(['knockout', 'text'], (ko) => {
        ko.components.register("searchhistory", {
            viewModel: { require: "components/searchhistory/searchhistory" },
            template: { require: "text!components/searchhistory/searchhistory.html"}
            });
});

require(['knockout', 'viewModel'], function (ko, vm) {
    ko.applyBindings(vm);
});