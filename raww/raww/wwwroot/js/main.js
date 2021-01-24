//Require bruges til at specificere hvilke libs der skal hentes, og hvor de kan findes (Paths), sørge for de loader
require.config({
    baseUrl: "js",
    paths: {
        //Mvvm - Connects til data-binds
        knockout: "lib/knockout/knockout-latest",
        //For at kunne indlæse HTML filer
        text: "lib/require-text/text.min",
        //Vores egen dataservice lib
        dataservice: "services/dataService",
        //Vores Messaging system, subscriber etc. 
        postman: "services/postman",
        jquery: "lib/jquery/jquery.min",
        //Styling tema, opsætning af grid, udseende etc. 
        bootstrap: "lib/twitter-bootstrap/js/bootstrap.bundle.min"
    },
    shim: {
        //Bs bruger jquery da nogle funktioner kræver det. 
        bootstrap: ['jquery']
    }
});

//Registere komponent, gir den et navn og specificere en path til viewModel og template for det specifikke komponent. 
require(['knockout', 'text'], (ko) => {
    ko.components.register("searchhistory", {
        viewModel: { require: "components/searchhistory/searchhistory" },
        template: { require: "text!components/searchhistory/searchhistory.html" }
    });

    ko.components.register("ratinghistory", {
        viewModel: { require: "components/ratinghistory/ratinghistory" },
        template: { require: "text!components/ratinghistory/ratinghistory.html" }
    });

    ko.components.register("bookmark", {
        viewModel: { require: "components/bookmark/bookmark" },
        template: { require: "text!components/bookmark/bookmark.html" }
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
    ko.components.register("signin", {
        viewModel: { require: "components/signin/signin" },
        template: { require: "text!components/signin/signin.html" }
    });
    ko.components.register("profile", {
        viewModel: { require: "components/profile/profile" },
        template: { require: "text!components/profile/profile.html" }
    });

});

//Alle data-binds vi har i det forskellige filer bliver applied her og bindes til viewModel.
require(['knockout', 'viewModel', 'bootstrap',], function (ko, vm) {
    ko.applyBindings(vm);
});