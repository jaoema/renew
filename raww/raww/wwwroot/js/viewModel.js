define(['knockout', 'postman'], function (ko, postman) {
    //private part

    //components with a title and a filename
    let nameSearchComp = { titleName: "Find Person", fileName: "namesearch" }
    let searchHistoryComp = { titleName: "Search History", fileName: "searchhistory" }
    let titleSearchComp = { titleName: "Find Title", fileName: "titlesearch" }
    let searchterm = ko.observable("");



    let currentComponent = ko.observable(nameSearchComp.fileName);
    let menuElements = [nameSearchComp, titleSearchComp, searchHistoryComp];
    let selectedComponent = ko.observable('namesearch');

    let changeContent = element => {
        currentComponent(element.fileName.toLowerCase());

    }

    let isActive = element => {
        return element.fileName.toLowerCase() === currentComponent() ? "active" : "";
    }

    let clickSearch = function() {
        //currentComponent(simpleSearchComp.fileName.toLowerCase());
        changeContent(titleSearchComp);
        setTimeout(() => {
            postman.publish('changeSearchFromNav', false);

            postman.publish('changeSearchterm', searchterm());
            searchterm("");
            postman.publish('changeSearchFromNav', true);
        }, 100);
        
        
    }

    let enableSearch = ko.computed(() => searchterm() !== "");


    //public part
    return {
        currentComponent,
        menuElements,
        selectedComponent,
        changeContent,
        isActive,
        searchterm,
        clickSearch,
        enableSearch
    };
});