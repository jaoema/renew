
require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/knockout-latest",
        text: "lib/require-text/text.min",
        dataservice: "services/dataService",
        postman: "services/postman",
        jquery: "lib/jquery/jquery.min",
        bootstrap: "lib/twitter-bootstrap/js/bootstrap.bundle.min"
    },
    shim: {
        bootstrap: ['jquery']
    }
});

require(['knockout', 'text'], (ko) => {
    ko.components.register("searchhistory", {
        viewModel: { require: "components/searchhistory/searchhistory" },
        template: { require: "text!components/searchhistory/searchhistory.html" }
    });

    ko.components.register("ratinghistory", {
        viewModel: { require: "components/ratinghistory/ratinghistory" },
        template: { require: "text!components/ratinghistory/ratinghistory.html" }
    });

    ko.components.register("namesearch", {
        viewModel: { require: "components/namesearch/namesearch" },
        template: { require: "text!components/namesearch/namesearch.html" }
    });

    ko.components.register("persondetails", {
        viewModel: { require: "components/persondetails/persondetails" },
        template: { require: "text!components/persondetails/persondetails.html" }
    });

    ko.components.register("titlesearch", {
        viewModel: { require: "components/titlesearch/titlesearch" },
        template: { require: "text!components/titlesearch/titlesearch.html" }
    });

    ko.components.register("titledetails", {
        viewModel: { require: "components/titledetails/titledetails" },
        template: { require: "text!components/titledetails/titledetails.html" }
    });

});

require(['knockout', 'viewModel', 'bootstrap',], function (ko, vm) {
    ko.applyBindings(vm);
});