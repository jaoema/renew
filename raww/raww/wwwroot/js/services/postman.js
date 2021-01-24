define([], () => {
    //Array med subs
    let subscribers = [];
    //udefineret fra start
    let lastEvent = undefined;

    //publisher et event med data
    let publish = (event, data) => {
        //sortere i listen af subs, til kun at callback til de subs der lytter til x-event
        subscribers.filter(x => x.event === event)
            .forEach(x => x.callback(data));

        //Når der blir published et event med data, bliver det event sat til det sidste nye event
        lastEvent = { event, data };
    }
    //Subscriber med et event, og hvilken information fra eventet
    let subscribe = (event, callback) => {
        let subscriber = { event, callback };
        subscribers.push(subscriber);
        //Hvis last event passer med last subscribed event, laves der et callback med data. 
        if (lastEvent && event === lastEvent.event) {
            callback(lastEvent.data);
        };
    }


    return {
        publish,
        subscribe
    }
});